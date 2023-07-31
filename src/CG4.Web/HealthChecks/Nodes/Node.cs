namespace CG4.Web.HealthChecks.Nodes
{
    public abstract class Node
    {
        public abstract void Visit(INodeVisitor visitor);
    }
}
