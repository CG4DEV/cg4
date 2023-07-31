namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeKey : Node
    {
        public string Value { get; set; }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeKey(this);
        }

        public static NodeKey From(string value)
        {
            return new NodeKey
            {
                Value = value,
            };
        }
    }
}
