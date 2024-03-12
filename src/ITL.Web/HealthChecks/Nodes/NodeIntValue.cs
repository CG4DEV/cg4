namespace ITL.Web.HealthChecks.Nodes
{
    public class NodeIntValue : Node
    {
        public int Value { get; set; }

        public static NodeIntValue From(int i)
        {
            return new()
            {
                Value = i,
            };
        }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeIntValue(this);
        }
    }
}
