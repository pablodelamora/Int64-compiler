MCS=mcs

all: source/Driver.cs source/Scanner.cs source/Token.cs source/TokenCategory.cs
	$(MCS) -out:analyzer.exe source/*.cs

test:
	mono analyzer.exe sample_inputs/test.int64 | less

clean:
	rm analyzer.exe
