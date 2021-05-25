using System;

namespace ExpressionEngineApp
{
    public class ExpressionEngine
    {
        private readonly Expression _expression;
        
        public ExpressionEngine(Expression expression)
        {
            _expression = expression;
        }
        
        public Node AnalyzeExpression()
        {
            var expr = AnalyzeAddSubtract();
            return _expression.Type == CharType.EndOfExpression ? null : expr;
        }

        private Node AnalyzeAddSubtract()
        {
            var left = AnalyzeMultiplyDivide();
            while (true)
            {
                Func<double, double, double> operatorProcess = null;
                if (_expression.Type == CharType.Add)
                {
                    operatorProcess = (a, b) => a + b;
                }
                else if (_expression.Type == CharType.Subtract)
                {
                    operatorProcess = (a, b) => a - b;
                }

                if (operatorProcess == null)
                    return left;

                _expression.Next();
                var right = AnalyzeMultiplyDivide();
                left = new OperationNode(left, right, operatorProcess);
            }
        }

        private Node AnalyzeMultiplyDivide()
        {
            var left = AnalyzeUnary();
            while (true)
            {
                Func<double, double, double> operatorProcess = null;
                if (_expression.Type == CharType.Multiply)
                {
                    operatorProcess = (a, b) => a * b;
                }
                else if (_expression.Type == CharType.Divide)
                {
                    operatorProcess = (a, b) => a / b;
                }

                if (operatorProcess == null)
                    return left;

                _expression.Next();
                var right = AnalyzeUnary();
                left = new OperationNode(left, right, operatorProcess);
            }
        }

        private Node AnalyzeUnary()
        {
            while (true)
            {
                switch (_expression.Type)
                {
                    case CharType.Add:
                        _expression.Next();
                        continue;
                    case CharType.Subtract:
                    {
                        _expression.Next();
                        var right = AnalyzeUnary();
                        return new UnaryNode(right, (a) => -a);
                    }
                    default:
                        return AnalyzeLeaf();
                }
            }
        }
        private Node AnalyzeLeaf()
        {
            switch (_expression.Type)
            {
                case CharType.Number:
                {
                    var node = new NumberNode(_expression.Number);
                    _expression.Next();
                    return node;
                }
                case CharType.OpenParens:
                {
                    _expression.Next();
                    var node = AnalyzeAddSubtract();
                    _expression.Next();
                    return node;
                }
                default:
                    return null;
            }
        }
    }
}