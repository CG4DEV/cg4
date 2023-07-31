namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeSectionsList : NodeList
    {
        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeSectionsList(this);
        }
    }
}
