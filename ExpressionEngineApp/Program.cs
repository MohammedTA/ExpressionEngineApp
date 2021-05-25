using System;

namespace ExpressionEngineApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string exp = "((1 + -2)*(70/7))";
            Console.WriteLine(Parse(exp).Execute());
        }
        
        // Static helper to parse a string
        public static Node Parse(string str)
        {
            return Parse(new Expression(str));
        }

        // Static helper to parse from a tokenizer
        public static Node Parse(Expression expression)
        {
            var parser = new ExpressionEngine(expression);
            return parser.AnalyzeExpression();
        }
    }
}