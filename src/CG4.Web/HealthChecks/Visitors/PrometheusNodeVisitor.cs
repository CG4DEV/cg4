using System.Text;
using CG4.Web.HealthChecks.Nodes;

namespace CG4.Web.HealthChecks.Visitors
{
    public class PrometheusNodeVisitor : INodeVisitor
    {
        private const string METRIC_NAME = "application_connect_";

        private readonly StringBuilder _stringBuilder;

        public PrometheusNodeVisitor()
        {
            _stringBuilder = new StringBuilder();
        }

        public string Build()
        {
            return _stringBuilder.ToString();
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
            _stringBuilder.Append('"');
            _stringBuilder.Append(nodeIntValue.Value);
            _stringBuilder.Append('"');
        }

        public void VisitNodeKey(NodeKey nodeKey)
        {
            _stringBuilder.Append(nodeKey.Value);
        }

        public void VisitNodeKeyValue(NodeKeyValue nodeKeyValue)
        {
            nodeKeyValue.Key.Visit(this);
            _stringBuilder.Append(':');
            nodeKeyValue.Value.Visit(this);
        }

        public void VisitNodeReport(NodeReport nodeReport)
        {
            _stringBuilder.Append("# HEALTH REPORT ");
            nodeReport.CommonStatus.Visit(this);
            _stringBuilder.AppendLine();
            _stringBuilder.AppendLine();
            nodeReport.Sections.Visit(this);
        }

        public void VisitNodeSection(NodeSection nodeSection)
        {
            if (!string.IsNullOrEmpty(nodeSection.Description))
            {
                _stringBuilder.Append("# HELP ");
                _stringBuilder.AppendLine(nodeSection.Description);
            }

            _stringBuilder.Append("# STATUS ");
            nodeSection.Status.Visit(this);
            _stringBuilder.AppendLine();

            _stringBuilder.Append("# TYPE ");
            _stringBuilder.Append(METRIC_NAME);
            _stringBuilder.Append(nodeSection.Name);
            _stringBuilder.AppendLine(" gauge");

            _stringBuilder.Append(METRIC_NAME);
            _stringBuilder.Append(nodeSection.Name);
            _stringBuilder.Append('{');
            nodeSection.AdditionalData.Visit(this);
            _stringBuilder.Append("} ");
            _stringBuilder.Append(nodeSection.Status.Value);
            _stringBuilder.AppendLine();

        }

        public void VisitNodeStringValue(NodeStringValue nodeStringValue)
        {
            _stringBuilder.Append('"');
            _stringBuilder.Append(nodeStringValue.Value);
            _stringBuilder.Append('"');
        }

        public void VisitNodeSectionsList(NodeSectionsList nodeSectionsList)
        {
            VisitNodeList(nodeSectionsList, Environment.NewLine);
        }

        public void VisitNodeAddDataList(NodeAddDataList nodeAddDataList)
        {
            VisitNodeList(nodeAddDataList);
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

        public void VisitNodeNullValue(NodeNullValue nodeNullValue)
        {
            _stringBuilder.Append('"');
            _stringBuilder.Append('"');
        }
    }
}
