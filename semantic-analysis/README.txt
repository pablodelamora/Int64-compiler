Removed sample-inputs/empty.int64 as it is samantically illegal for it does not have a main function.
Removed sample-inputs/associativity.int64 as it had a semantic error, namely: variables used were not declared.
Added source/{FirstPassVisitor,SecondPassVisitor,SemanticAnalyzer,SemanticError,Symbols}.cs