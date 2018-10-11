MCS=mcs

all: source/Driver.cs source/FirstPassVisitor.cs source/int64lib.cs source/Node.cs source/Parser.cs source/Scanner.cs source/SecondPassVisitor.cs source/SemanticAnalyzer.cs source/SemanticError.cs source/SpecificNodes.cs source/Symbols.cs source/SyntaxError.cs source/Token.cs source/TokenCategory.cs
	$(MCS) -out:int64.exe source/*.cs
	$(MCS) -out:int64lib.dll -t:library source/int64lib.cs

TEST_FILES := $(wildcard sample-inputs/*)
test:
	@for dir in $(TEST_FILES); do \
		echo ">> Testing "$$dir; \
		mono int64.exe $$dir out.il && ilasm out.il && mono out.exe; \
	done

clean:
	-rm int64.exe
	-rm int64lib.dll
	-rm out.il
	-rm out.exe