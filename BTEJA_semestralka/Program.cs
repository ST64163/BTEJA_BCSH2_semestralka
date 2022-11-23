using InterpreterSK;

string sourceFile = "..\\..\\..\\TestSourceCode.txt";
string sourceCode = File.ReadAllText(sourceFile);
Interpreter interpreter = new();
interpreter.WriteEvent += (object sender, string message) => { Console.WriteLine(message); };
interpreter.ReadEvent += (object sender) => { return Console.ReadLine() ?? ""; };
interpreter.Interpret(sourceCode);