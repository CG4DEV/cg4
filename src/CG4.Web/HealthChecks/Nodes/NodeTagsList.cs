namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeTagsList : NodeList
    {
        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeTagsList(this);
        }
    }
}
