using IDE.Interpreter.AST.Statements;
using IDE.Interpreter.LexicalAnalysis;
using IDE.Interpreter.SemanticAnalysis;
using IDE.Interpreter.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace BTEJA_BCSH2_semestralka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickInterpret(object sender, RoutedEventArgs e)
        {
            PrintLine("");
            string sourceCode = textBox_editor.Text;
            try
            {
                Lexer lexer = new();
                Print("Lexer - ");
                lexer.LoadProgram(sourceCode, out List<Token> tokens);
                PrintLine("OK");
                /*
                tokens.ForEach(token =>
                    {
                        string tokenString = token.ToString();
                        string valueString = "";
                        if (token is IdentifierToken || token is IntToken || token is DoubleToken || token is BoolToken || token is StringToken)
                            valueString = " - " + token.Value.ToString();
                        if (token is ReservedToken || token is OperatorToken)
                            valueString = " - " + Token.TokenTypeToString.GetValueOrDefault(token.TokenType);
                        PrintLine(tokenString + valueString);
                    });
                */
                Parser parser = new();
                Print("Parser - ");
                parser.Parse(tokens, out List<Statement> statements);
                PrintLine("OK");
            }
            catch (System.Exception exception)
            {
                PrintLine(exception.Message);
            }
        }

        private void Print(string sentence) => textBox_console.Text += sentence;
        private void PrintLine(string sentence) => textBox_console.Text += $"{sentence}\n";

        private void ClickLoad(object sender, RoutedEventArgs e)
        {
            Print("\nLoad ...\n");
        }

        private void ClickSave(object sender, RoutedEventArgs e)
        {
            Print("\nSave ...\n");
        }
    }
}
