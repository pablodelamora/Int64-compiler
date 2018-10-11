Context Free Grammar in LL(1) form is provided as a JPEG image.
This syntactical-analyzer builds upon the lexical-analyzer done as a previous phase.
source/Scanner.cs was almost taken verbatim. Some corrections were done on defining the Regexs:
	Mainly changing character classes to character sequences, e.g.: [<<] to <<.
	They do not mean the same as the first will match < and the second <<.
source/Scanner.cs was changed to reflect the new inclusions, and output ad-hoc messages.
All other source files were copied verbatim.
New source files added: Parser.cs and SyntaxError.cs
