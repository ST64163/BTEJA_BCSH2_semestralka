using InterpreterSK;

string sourceFile = "..\\..\\..\\_Documents\\Priklad 3.txt";
string sourceCode = File.ReadAllText(sourceFile);
Interpreter interpreter = new();
interpreter.WriteEvent += (object sender, string message) => { Console.Write(message); };
interpreter.ReadLineEvent += (object sender) => { return Console.ReadLine() ?? ""; };
interpreter.Interpret(sourceCode);