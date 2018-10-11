/*
Authors:
- Gad Levy A01017986
- Jonathan Ginsburg A01021617
- Pablo de la Mora A01020365
*/

using System;
using System.Collections.Generic;

namespace Int64 {

	class SecondPassVisitor {

		private SemanticAnalyzer semanticAnalyzer;
		private FunctionSym currentFunction;
		private int nestedLoopCount = 0;

		public SecondPassVisitor(SemanticAnalyzer semanticAnalyzer) {
			this.semanticAnalyzer = semanticAnalyzer;
		}

		public void Visit (NProgram node) {
			// Visit((dynamic) node[0]); No need to visit variable definitions on first pass
			Visit((NFunDefList) node[1]);
		}

		public void Visit (NFunDefList node) {
			foreach (NFunDef nFunDef in node) {
				Visit((dynamic) nFunDef);
			}
		}

		public void Visit (NFunDef nFunDef) {
			currentFunction = semanticAnalyzer.GetFunctionSymbolByLexeme(nFunDef.AnchorToken.Lexeme);
			// No need to visit neither parameter list nor variable definition list
			NStmtList stmtList = (NStmtList)nFunDef[2];
			Visit(stmtList);
			currentFunction = null;
		}

		public void Visit (NStmtList nStmtList) {
			foreach (Node stmt in nStmtList) {
				Visit((dynamic) stmt);
			}
		}

		public void Visit (NAssign nAssign) {
			string lexeme = nAssign.AnchorToken.Lexeme;
			if (currentFunction.GetLocalVariableSymbolByLexeme(lexeme) != null || semanticAnalyzer.GetGlobalVariableSymbolByLexeme(lexeme) != null) {
				Visit((dynamic) nAssign[0]);
			}
			else throw new SemanticError("Variable not in scope", nAssign.AnchorToken);
		}

		public void Visit (NFunCall nFunCall) {
			string lexeme = nFunCall.AnchorToken.Lexeme;
			FunctionSym functionSym = semanticAnalyzer.GetFunctionSymbolByLexeme(lexeme);
			NExprList argumentList = (NExprList)nFunCall[0];
			if (functionSym != null && functionSym.GetArity() == argumentList.children.Count) {
				Visit(argumentList);
			}
			else throw new SemanticError("Nonexistent function or signature mismatch", nFunCall.AnchorToken);
		}

		public void Visit (NExprList nExprList) {
			GenericChildVisitor(nExprList);
		}

		public void Visit (NExpr nExpr) {
			Visit((dynamic) nExpr[0]);
			if (nExpr.children.Count >= 2) {
				Visit((dynamic) nExpr[1]);
				Visit((dynamic) nExpr[2]);
			}
		}

		public void Visit (NIfStmt nIfStmt) {
			Visit((dynamic) nIfStmt[0]); // Visit condition
			Visit((dynamic) nIfStmt[1]); // Visit body
			int currentChildIndex = 2;
			while (currentChildIndex < nIfStmt.children.Count) {
				if (nIfStmt[currentChildIndex].GetType() == typeof(NExpr)) { // Assume else if
					Visit((dynamic) nIfStmt[currentChildIndex++]); // Visit else if condition
					Visit((dynamic) nIfStmt[currentChildIndex++]); // Visit else if body
				}
				else { // Assume else
					Visit((dynamic) nIfStmt[currentChildIndex++]); // Visit else body
				}
			}
		}

		public void Visit (NSwitchStmt nSwitchStmt) {
			GenericChildVisitor(nSwitchStmt);
		}

		public void Visit (NCaseList nCaseList) {
			GenericChildVisitor(nCaseList);
		}

		public void Visit (NCase nCase) {
			GenericChildVisitor(nCase);
		}

		public void Visit (NWhileStmt nWhileStmt) {
			++nestedLoopCount;
			GenericChildVisitor(nWhileStmt);
			--nestedLoopCount;
		}

		public void Visit (NDoWhileStmt nDoWhileStmt) {
			++nestedLoopCount;
			GenericChildVisitor(nDoWhileStmt);
			--nestedLoopCount;
		}

		public void Visit (NForStmt nForStmt) {
			++nestedLoopCount;
			Token anchorToken = nForStmt.AnchorToken;
			string lexeme = anchorToken.Lexeme;
			if (currentFunction.GetLocalVariableSymbolByLexeme(lexeme) == null && semanticAnalyzer.GetGlobalVariableSymbolByLexeme(lexeme) == null) throw new SemanticError("Variable not in scope", anchorToken);
			GenericChildVisitor(nForStmt);
			--nestedLoopCount;
		}

		public void Visit (NBreak nBreak) {
			if (nestedLoopCount <= 0) throw new SemanticError("Break statement not allowed outside a loop", nBreak.AnchorToken);
		}

		public void Visit (NContinue nContinue) {
			if (nestedLoopCount <= 0) throw new SemanticError("Continue statement not allowed outside a loop", nContinue.AnchorToken);
		}

		public void Visit (NReturn nReturn) {
			GenericChildVisitor(nReturn);
		}

		public void Visit (NExprOr nExprOr) {
			GenericChildVisitor(nExprOr);
		}

		public void Visit (NExprAnd nExprAnd) {
			GenericChildVisitor(nExprAnd);
		}

		public void Visit (NExprComp nExprComp) {
			GenericChildVisitor(nExprComp);
		}

		public void Visit (NExprRel nExprRel) {
			GenericChildVisitor(nExprRel);
		}

		public void Visit (NExprBitOr nExprBitOr) {
			GenericChildVisitor(nExprBitOr);
		}

		public void Visit (NExprBitAnd nExprBitAnd) {
			GenericChildVisitor(nExprBitAnd);
		}

		public void Visit (NExprBitShift nExprBitShift) {
			GenericChildVisitor(nExprBitShift);
		}	

		public void Visit (NExprAdd nExprAdd) {
			GenericChildVisitor(nExprAdd);
		}

		public void Visit (NExprMul nExprMul) {
			GenericChildVisitor(nExprMul);
		}

		public void Visit (NExprPow nExprPow) {
			GenericChildVisitor(nExprPow);
		}

		public void Visit (NExprUnary nExprUnary) {
			GenericChildVisitor(nExprUnary);
		}

		public void Visit (NExprPrimary nExprPrimary) {
			if (nExprPrimary.children.Count == 0) {
				Token anchorToken = nExprPrimary.AnchorToken;
				string lexeme = anchorToken.Lexeme;
				if (currentFunction.GetLocalVariableSymbolByLexeme(lexeme) == null && semanticAnalyzer.GetGlobalVariableSymbolByLexeme(lexeme) == null) throw new SemanticError("Variable not in scope", anchorToken);
			}
			GenericChildVisitor(nExprPrimary);
		}

		public void Visit (NArrayList nArrayList) {
			GenericChildVisitor(nArrayList);
		}

		public void GenericChildVisitor(Node node) {
			foreach (Node n in node) {
				Visit((dynamic) n);
			}
		}

		//--------------------------------------------------------------------------------
		// Literal Visitors Follow

		public void Visit (NLitBool nLitBool) {
			// Do nothing
		}

		private string SanitizeLeadingZeros(string number, int prefixLength) {
			string numberWithoutPrefix = number.Substring(prefixLength);
			for (int i = 0; i < numberWithoutPrefix.Length; i++) {
				if (numberWithoutPrefix[i] == '0') {
					if (i == numberWithoutPrefix.Length - 1) numberWithoutPrefix = "0"; // The whole number is a string of zeros, semantically equivalent to a single zero
					continue;
				}
				else {
					numberWithoutPrefix = numberWithoutPrefix.Substring(i);
					break;
				}
			}
			return number.Substring(0, prefixLength) + numberWithoutPrefix;
		}

		public void Visit (NLitInt nLitInt) {
			Token token = nLitInt.AnchorToken;
			string lexeme = token.Lexeme;
			TokenCategory tokenCategory = token.Category;
			bool errorFound = false;
			switch (tokenCategory) {
				case TokenCategory.BASE_2: {
					int maxNumerals = 64;
					int prefixLength = 2;
					string sanitizedLexeme = SanitizeLeadingZeros(lexeme, prefixLength);
					if (sanitizedLexeme.Length > maxNumerals + prefixLength) errorFound = true;
					break;
				}
				case TokenCategory.BASE_8: {
					int maxNumerals = 22;
					int prefixLength = 2;
					int maxMostSignificantNumeral = 1;
					string sanitizedLexeme = SanitizeLeadingZeros(lexeme, prefixLength);
					if (sanitizedLexeme.Length > maxNumerals + prefixLength) errorFound = true;
					if (Convert.ToInt16(lexeme.Substring(2, 1)) > maxMostSignificantNumeral) errorFound = true;
					break;
				}
				case TokenCategory.BASE_10: {
					int maxNumerals = 19;
					int prefixLength = 0;
					string highestDecimalRepresentation = "9223372036854775807";
					string sanitizedLexeme = SanitizeLeadingZeros(lexeme, prefixLength);
					if (sanitizedLexeme.Length > maxNumerals + prefixLength) errorFound = true;
					if (sanitizedLexeme.Length == maxNumerals + prefixLength) {
						for (int i = 0; i < highestDecimalRepresentation.Length; i++) {
							if (Convert.ToInt16(highestDecimalRepresentation[i]) < Convert.ToInt16(sanitizedLexeme[i])) errorFound = true;
						}
					}
					break;
				}
				case TokenCategory.BASE_16: {
					int maxNumerals = 16;
					int prefixLength = 2;
					string sanitizedLexeme = SanitizeLeadingZeros(lexeme, prefixLength);
					if (sanitizedLexeme.Length > maxNumerals + prefixLength) errorFound = true;
					break;
				}
			}
			if (errorFound) throw new SemanticError("Literal too big", token);
		}

		public void Visit (NLitChar nLitChar) {
			// Do nothing
		}

		public void Visit (NLitString nLitString) {
			// Do nothing
		}
	}
}
