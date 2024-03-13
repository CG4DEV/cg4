namespace ITL.Web.HealthChecks.Nodes
{
    public class NodeStringValue : Node
    {
        public string Value { get; set; }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeStringValue(this);
        }

        public static NodeStringValue From(string value)
        {
            return new NodeStringValue
            {
                Value = value,
            };
        }

        public static NodeStringValue From(object value)
        {
            return new NodeStringValue
            {
                Value = value.ToString(),
            };
        }
    }
}
