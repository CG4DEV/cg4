namespace ITL.Web.HealthChecks.Nodes
{
    public abstract class Node
    {
        public abstract void Visit(INodeVisitor visitor);
    }
}
