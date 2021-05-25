using System;

namespace ExpressionEngineApp
{
    internal class UnaryNode : Node
    {
        public UnaryNode(Node rhs, Func<double, double> operatorFunc)
        {
            RightNode = rhs;
            OperatorFunc = operatorFunc;
        }

        private Node RightNode { get; }
        private Func<double, double> OperatorFunc { get; }

        public override double Execute()
        {
            var rightValue = RightNode.Execute();
            var result = OperatorFunc(rightValue);
            return result;
        }
    }
}