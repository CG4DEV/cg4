namespace ITL.Web.HealthChecks.Nodes
{
    public class NodeNullValue : Node
    {
        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeNullValue(this);
        }
    }
}
