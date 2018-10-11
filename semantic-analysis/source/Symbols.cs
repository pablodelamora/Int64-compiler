/*
Authors:
 - Gad Levy A01017986
 - Jonathan Ginsburg A01021617
 - Pablo de la Mora A01020365
*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Int64 {

    enum VariableSymKind {PARAMETER, REGULAR};
    enum FunctionSymKind {STANDARD, USER_DEFINED};

    class VariableSym {
        public Token anchorToken;
        public VariableSymKind kind;

        public VariableSym(Token anchorToken, VariableSymKind kind) {
            this.kind = kind;
            this.anchorToken = anchorToken;
        }

        public void Print(int margin = 0) {
            Console.WriteLine(String.Concat(Enumerable.Repeat("\t", margin)) + $"Name: {anchorToken.Lexeme}, with type {kind}.");
        }
    }

    class FunctionSym {
        public Token anchorToken;
        public List<VariableSym> localVars = new List<VariableSym>();
        public FunctionSymKind kind;

        public VariableSym GetLocalVariableSymbolByLexeme(string lexeme) {
            foreach (VariableSym variable in localVars) {
                if (variable.anchorToken.Lexeme == lexeme) return variable;
            }
            return null;
        }

        public void AddLocalVariable(VariableSym newVariable) {
            foreach (VariableSym variable in localVars) {
                if (variable.anchorToken.Lexeme == newVariable.anchorToken.Lexeme) {
                    throw new SemanticError("Name collision", newVariable.anchorToken);
                }
            }
            localVars.Add(newVariable);
        }

        public void Print(int margin = 0) {
            Console.WriteLine(String.Concat(Enumerable.Repeat("\t", margin)) + $"Name: {anchorToken.Lexeme}, with arity {GetArity()} and type {kind}.");
            foreach (VariableSym variable in localVars) {
                variable.Print(1);
            }
        }

        public int GetArity() {
            int arity = 0;
            foreach (VariableSym variable in localVars) {
                if (variable.kind == VariableSymKind.PARAMETER) ++arity;
            }
            return arity;
        }
        
        public FunctionSym(Token anchorToken, FunctionSymKind kind) {
            this.anchorToken = anchorToken;
            this.kind = kind;
        }
    }

}
