Int64 Compiler
--------------

This compiler was done as a semester project for the Compilers Design course at ITESM-CSF during the August-December 2017 term. It was lectured by Prof. Ariel Ortiz. A website for the course can be found at: http://arielortiz.info

Compilation Process
-------------------

The project was done in a modular way that corresponds to the steps of compilation usually found in literature, namely:
	1) Lexical Analysis (Scanner)
		In this phase characters are translated into tokens.
	2) Syntactic Analysis
		In this phase the language grammar is translated into LL(1) type and a recursive descent parser is implemented.
	3) Abstract Syntax Tree (AST) Construction (Parser)
		In this phase the AST is built from the previous syntactic analyzer's parser in a way that language constructs are abstracted in regards to associativity, precedence, etc.
	4) Semantic Analysis
		This phase consists of AST visitors that: (1) build symbol tables for global and local scopes of variables and function definitions and (2) check type consistency (not in our case as our language only has one type), and verify symbol existence and coherence, such as function signature usage.
	5) Code Generation
		This phase culminates in producing a target code (usually assembly) that is equivalent to the initial one. It consists of another AST visitor that returns code partials to produce a total.

Files
-----

Files and directories contained in this repository are arranged in a way that reflects evolution of the compiler. First, code was produced for the lexical analysis phase and hence put in a directory that corresponds with name, then syntax analysis was implemented and so on. The directories of subsequent phases are modified copies of previous phases, along with some bug corrections and minor changes found in each of the README.txt files contained. The final result is, as expected, found in the code-generation directory, which depends on all previous phases. It is considered the most debugged code and the most updated one. All previous are only meant for learning from the evolution process.

All code was based on example source files for the buttercup language studied during the course. Please refer to the course website for further details of the aforementioned language.

Authors
-------

Credit for this compiler goes to Gad Levy and Pablo De La Mora. Definitely, credit should also be given to Professor Ariel Ortiz.

Jonathan Ginsburg,
December 7, 2017.
