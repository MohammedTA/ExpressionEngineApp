using System;
using System.Globalization;

namespace ExpressionEngineApp
{
    public class Expression
    {
        public Expression(string str)
        {
            Chars = str.GetEnumerator();
            NextChar();
        }

        private CharEnumerator Chars { get; }
        private char? Char { get; set; }
        public CharType Type { get; private set; }
        public double Number { get; private set; }

        private void NextChar()
        {
            if (Chars.MoveNext())
                Char = Chars.Current;
            else
                Char = null;
        }

        public void Next()
        {
            while (Char.HasValue && char.IsWhiteSpace(Char.Value)) NextChar();

            switch (Char)
            {
                case '+':
                    Type = CharType.Add;
                    NextChar();
                    return;

                case '-':
                    Type = CharType.Subtract;
                    NextChar();
                    return;

                case '*':
                    Type = CharType.Multiply;
                    NextChar();
                    return;

                case '/':
                    Type = CharType.Divide;
                    NextChar();
                    return;

                case '(':
                    Type = CharType.OpenParens;
                    NextChar();
                    return;

                case ')':
                    Type = CharType.CloseParens;
                    NextChar();
                    return;
            }

            if (!Char.HasValue || !char.IsDigit(Char.Value) && Char != '.') return;
            
            var num = "";
            var haveDecimalPoint = false;
            while (Char.HasValue && (char.IsDigit(Char.Value) || !haveDecimalPoint && Char == '.'))
            {
                num += Char;
                haveDecimalPoint = Char == '.';
                NextChar();
            }
            Number = double.Parse(num, CultureInfo.InvariantCulture);
            Type = CharType.Number;
        }
    }
}