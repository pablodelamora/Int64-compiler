MCS=mcs

all: source/Driver.cs source/Parser.cs source/Scanner.cs source/SyntaxError.cs source/Token.cs source/TokenCategory.cs
	$(MCS) -out:int64.exe source/*.cs

TEST_FILES := $(wildcard sample-inputs/*)
test:
	@for dir in $(TEST_FILES); do \
		echo ">> Testing "$$dir; \
		mono int64.exe $$dir; \
	done

clean:
	rm int64.exe
