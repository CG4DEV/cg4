namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeNullValue : Node
    {
        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeNullValue(this);
        }
    }
}
