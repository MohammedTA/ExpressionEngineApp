using System;

namespace ExpressionEngineApp
{ 
    internal class OperationNode : Node
    {
        public OperationNode(Node leftNode, Node rightNode, Func<double, double, double> op)
        {
            LeftNode = leftNode;
            RightNode = rightNode;
            OperatorFunc = op;
        }

        private Node LeftNode { get; }
        private Node RightNode { get; }
        private Func<double, double, double> OperatorFunc { get; }

        public override double Execute()
        {
            var leftValue = LeftNode.Execute();
            var rightValue = RightNode.Execute();

            return OperatorFunc(leftValue, rightValue);
        }
    }
}