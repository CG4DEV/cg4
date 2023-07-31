namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeKeyValue : Node
    {
        public NodeKey Key { get; set; }

        public Node Value { get; set; }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeKeyValue(this);
        }

        public static NodeKeyValue From(KeyValuePair<string, object> data)
        {
            var kv = new NodeKeyValue
            {
                Key = NodeKey.From(data.Key),
                Value = data.Value switch
                {
                    string s => NodeStringValue.From(s),
                    int i => NodeIntValue.From(i),
                    null => new NodeNullValue(),
                    _ => NodeStringValue.From(data.Value),
                },
            };

            return kv;
        }
    }
}
