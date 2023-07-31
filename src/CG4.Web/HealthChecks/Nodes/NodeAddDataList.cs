namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeAddDataList : NodeList
    {
        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeAddDataList(this);
        }
    }
}
