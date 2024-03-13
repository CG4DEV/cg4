using System.Text;
using ITL.Web.HealthChecks.Nodes;

namespace ITL.Web.HealthChecks.Visitors
{
    internal class TextNodeVisitor : INodeVisitor
    {
        private readonly StringBuilder _stringBuilder;

        public TextNodeVisitor()
        {
            _stringBuilder = new StringBuilder();
        }

        public void VisitNodeEnumeration(NodeEnumeration nodeEnumeration)
        {
            _stringBuilder.Append(nodeEnumeration.Name);
            _stringBuilder.Append(" (Code: ");
            _stringBuilder.Append(nodeEnumeration.Value);
            _stringBuilder.Append(')');
        }

        public void VisitNodeIntValue(NodeIntValue nodeIntValue)
        {
            _stringBuilder.Append(nodeIntValue.Value);
        }

        public void VisitNodeKey(NodeKey nodeKey)
        {
            _stringBuilder.Append(nodeKey.Value);
        }

        public void VisitNodeKeyValue(NodeKeyValue nodeKeyValue)
        {
            nodeKeyValue.Key.Visit(this);
            _stringBuilder.Append(": ");
            nodeKeyValue.Value.Visit(this);
        }

        public void VisitNodeReport(NodeReport nodeReport)
        {
            _stringBuilder.AppendLine("Health report");
            _stringBuilder.Append("Report status: ");
            nodeReport.CommonStatus.Visit(this);
            _stringBuilder.AppendLine();

            nodeReport.Sections.Visit(this);
        }

        public void VisitNodeSection(NodeSection nodeSection)
        {
            _stringBuilder.AppendLine();
            _stringBuilder.Append("Section ");
            _stringBuilder.Append(nodeSection.Name);
            _stringBuilder.Append(" is ");
            nodeSection.Status.Visit(this);
            _stringBuilder.AppendLine();

            if (!string.IsNullOrEmpty(nodeSection.Description))
            {
                _stringBuilder.AppendLine(nodeSection.Description);
            }

            if (nodeSection.Tags.Any())
            {
                _stringBuilder.AppendLine();
                _stringBuilder.Append("\tTags: ");
                nodeSection.Tags.Visit(this);
            }

            if (nodeSection.AdditionalData.Any())
            {
                _stringBuilder.AppendLine();
                nodeSection.AdditionalData.Visit(this);
            }
        }

        public void VisitNodeStringValue(NodeStringValue nodeStringValue)
        {
            _stringBuilder.Append(nodeStringValue.Value);
        }

        public void VisitNodeSectionsList(NodeSectionsList nodeSectionsList)
        {
            VisitNodeList(nodeSectionsList, Environment.NewLine);
        }

        public void VisitNodeAddDataList(NodeAddDataList nodeAddDataList)
        {
            VisitNodeList(nodeAddDataList, Environment.NewLine);
        }

        public void VisitNodeTagsList(NodeTagsList nodeTagsList)
        {
            VisitNodeList(nodeTagsList);
        }

        public void VisitNodeList(NodeList nodeList, string separator = ", ")
        {
            var isFirst = true;

            foreach (var node in nodeList)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    _stringBuilder.Append(separator);
                }

                node.Visit(this);
            }
        }

        public string Build()
        {
            return _stringBuilder.ToString();
        }

        public void VisitNodeNullValue(NodeNullValue nodeNullValue)
        {
            _stringBuilder.Append("{empty}");
        }
    }
}
