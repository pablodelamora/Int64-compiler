/*
Authors:
 - Gad Levy A01017986
 - Jonathan Ginsburg A01021617
 - Pablo de la Mora A01020365
*/

using System;
using System.IO;
using System.Text;

namespace Int64 {

	public class Driver {

		const string VERSION = "0.4";

		//-----------------------------------------------------------
		static readonly string[] ReleaseIncludes = {
			"Lexical analysis",
			"Syntactic analysis",
			"AST construction",
			"Semantic analysis"
		};

		//-----------------------------------------------------------
		void PrintAppHeader() {
			Console.WriteLine("Int64 lexical analyzer " + VERSION);
			Console.WriteLine("GJP.");
		}

		//-----------------------------------------------------------
		void PrintReleaseIncludes() {
			Console.WriteLine("Included in this release:");
			foreach (var phase in ReleaseIncludes) {
				Console.WriteLine("   * " + phase);
			}
		}

		//-----------------------------------------------------------
		void Run(string[] args) {

			PrintAppHeader();
			Console.WriteLine();
			PrintReleaseIncludes();
			Console.WriteLine();

			if (args.Length != 1) {
				Console.Error.WriteLine(
					"Please specify the name of the input file.");
				Environment.Exit(1);
			}

			try {
				var inputPath = args[0];
				var input = File.ReadAllText(inputPath);
				var parser = new Parser(new Scanner(input).Start().GetEnumerator());
				NProgram program = (NProgram)parser.CProgram();
				Console.WriteLine("Syntax OK");

				var semantic = new SemanticAnalyzer(program);
				semantic.Analyze();
				Console.WriteLine("Semantics OK.");
				Console.WriteLine();
				Console.WriteLine("Symbol Table");
				Console.WriteLine("============");
				Console.WriteLine("Functions");
				foreach (FunctionSym entry in semantic.functions) {
					entry.Print();          
				}
				Console.WriteLine("Global Variables");
				foreach (VariableSym entry in semantic.globalVars) {
					entry.Print();          
				}
			} catch (Exception e) {
				if (e is FileNotFoundException 
					|| e is SyntaxError 
					|| e is SemanticError) {
					Console.Error.WriteLine(e.Message);
					Environment.Exit(1);
				}
				throw;
			}
		}

		//-----------------------------------------------------------
		public static void Main(string[] args) {
			new Driver().Run(args);
		}
	}
}
