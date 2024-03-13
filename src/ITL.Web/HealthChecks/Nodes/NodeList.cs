using System.Collections;

namespace ITL.Web.HealthChecks.Nodes
{
    public abstract class NodeList : Node, IEnumerable<Node>
    {
        private readonly List<Node> _nodes;

        public NodeList()
        {
            _nodes = new();
        }

        public NodeList(IEnumerable<Node> nodes)
        {
            _nodes = new(nodes);
        }

        public void Add(Node node)
        {
            _nodes.Add(node);
        }

        public void AddRange(IEnumerable<Node> nodes)
        {
            _nodes.AddRange(nodes);
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _nodes.GetEnumerator();
        }
    }
}
