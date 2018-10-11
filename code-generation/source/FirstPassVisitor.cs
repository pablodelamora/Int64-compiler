/*
Authors:
- Gad Levy A01017986
- Jonathan Ginsburg A01021617
- Pablo de la Mora A01020365
*/

using System;
using System.Collections.Generic;

namespace Int64 {

	class FirstPassVisitor {

		private SemanticAnalyzer semanticAnalyzer;

		public FirstPassVisitor(SemanticAnalyzer semanticAnalyzer) {
			this.semanticAnalyzer = semanticAnalyzer;
		}

		public void Visit (NProgram node) {
			Visit((dynamic) node[0]);
			Visit((dynamic) node[1]);
		}

		public void Visit (NVarDefList node) {
			foreach (NVarDef varDef in node) {
				semanticAnalyzer.AddGlobalVariable(new VariableSym(varDef.AnchorToken, VariableSymKind.REGULAR));
			}
		}

		public void Visit (NFunDefList node) {
			foreach (NFunDef funDef in node) {
				FunctionSym newFuncSym = new FunctionSym(funDef.AnchorToken, FunctionSymKind.USER_DEFINED);
				// Add parameters to local scope
				foreach (NParameter param in funDef[0]) {
					VariableSym newParam = new VariableSym(param.AnchorToken, VariableSymKind.PARAMETER);
					newFuncSym.AddLocalVariable(newParam);
				}
				// Add variables to local scope
				foreach (NVarDef variable in funDef[1]) {
					VariableSym newVariable = new VariableSym(variable.AnchorToken, VariableSymKind.REGULAR);
					newFuncSym.AddLocalVariable(newVariable);
				}
				semanticAnalyzer.AddFunction(newFuncSym);
			}
		}
	}
}
