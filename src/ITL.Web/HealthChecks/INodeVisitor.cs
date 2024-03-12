using ITL.Web.HealthChecks.Nodes;

namespace ITL.Web.HealthChecks
{
    public interface INodeVisitor
    {
        void VisitNodeEnumeration(NodeEnumeration nodeEnumeration);

        void VisitNodeIntValue(NodeIntValue nodeIntValue);

        void VisitNodeKey(NodeKey nodeKey);

        void VisitNodeKeyValue(NodeKeyValue nodeKeyValue);

        void VisitNodeReport(NodeReport nodeReport);

        void VisitNodeSection(NodeSection nodeSection);

        void VisitNodeStringValue(NodeStringValue nodeStringValue);

        void VisitNodeSectionsList(NodeSectionsList nodeSectionsList);

        void VisitNodeAddDataList(NodeAddDataList nodeAddDataList);

        void VisitNodeTagsList(NodeTagsList nodeTagsList);

        string Build();

        void VisitNodeNullValue(NodeNullValue nodeNullValue);
    }
}
