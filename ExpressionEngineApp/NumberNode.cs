namespace ExpressionEngineApp
{
    internal class NumberNode : Node
    {
        public NumberNode(double number)
        {
            Number = number;
        }

        private double Number { get; }

        public override double Execute()
        {
            return Number;
        }
    }
}