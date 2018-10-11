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
        public VariableSymKind kind;
        public Token anchorToken;
        public string lexeme;

        public VariableSym(Token anchorToken, VariableSymKind kind) {
            this.kind = kind;
            this.anchorToken = anchorToken;
            this.lexeme = anchorToken.Lexeme;
        }

        public VariableSym(string newLexeme, VariableSymKind newKind) {
            this.lexeme = newLexeme;
            this.kind = newKind;
        }

        public void Print(int margin = 0) {
            Console.WriteLine(String.Concat(Enumerable.Repeat("\t", margin)) + $"Name: {lexeme}, with type {kind}.");
        }
    }

    class FunctionSym {
        public Token anchorToken;
        public List<VariableSym> localVars = new List<VariableSym>();
        public FunctionSymKind kind;

        public VariableSym GetLocalVariableSymbolByLexeme(string lexeme) {
            foreach (VariableSym variable in localVars) {
                if (variable.lexeme == lexeme) return variable;
            }
            return null;
        }

        public void AddLocalVariable(VariableSym newVariable) {
            foreach (VariableSym variable in localVars) {
                if (variable.lexeme == newVariable.lexeme) {
                    if (newVariable.anchorToken != null) {
                        throw new SemanticError("Name collision", newVariable.anchorToken);
                    }
                    else {
                        throw new Exception("Error of name collision with a variable symbol that has no anchor token, which probably means that it was created for aid during code generation. This should not happen. Try checking if in your code is an identifier (variable or function parameter) with lexeme: " + newVariable.lexeme + ".");
                    }
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
