/*
Authors:
 - Gad Levy A01017986
 - Jonathan Ginsburg A01021617
 - Pablo de la Mora A01020365
*/

using System;

namespace Int64 {

    class SemanticError: Exception {

        public SemanticError(string message):
            base(String.Format(
                "Semantic Error: {0}.",
                message
            )) {

        }
        public SemanticError(string message, Token token):
            base(String.Format(
                "Semantic Error: {0} \n" +
                "at row {1}, column {2}, with lexeme: {3}.",
                message,
                token.Row,
                token.Column,
                token.Lexeme
            )) {
        }
    }
}
