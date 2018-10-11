Resources
---------

See resources directory.

Changelog
---------

1) Changed grammar to allow the return statement to not have a return expression in which case the return value of the function would be equal to zero.
2) Check Parser.cs' implementation of public Node ExprUnary(), specifically the else of the most outer if contianed in such implementation. Also check Parser.cs' vicinity of line 281, which changed to return an NReturn node with no children when no expression for the return value was given.
3) Added short circuitry in And and Or expressions, that returns the last evaluated value.

Compiling Process of an Int64 Program
-------------------------------------

1) Compile compiler with: make
2) Compile source int64 to assembly with: mono int64.exe <source> <out>
3) Assemble the intermediate language (.il) file: ilasm <out>
4) Run executable produced in previous step with: mono <executable>

Example:
	make
	mono int64.exe sample-inputs/hello.int64 out.il
	ilasm out.il
	mono out.exe

Reverse Engineering Process for the Intermediate Language
---------------------------------------------------------

This is a very important step in writing the code-production phase of a compiler with target CIL (see: https://en.wikipedia.org/wiki/Common_Intermediate_Language). It consists in compiling a program written in regular C# with the mono compiler (mcs) and seeing the equivalent assembly by disassembling it. Then the concepts that are known in C# can be translated to CIL.

Process:
1) Write a simple C# program with a target feature to learn in CIL.
2) Compile it with: mcs input.cs
3) Disassemble it with: monodis output.exe
4) Learn from the disassembler output

Jonathan Ginsburg,
December 7, 2017.
