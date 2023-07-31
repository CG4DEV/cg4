using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CG4.Web.HealthChecks.Nodes
{
    public class NodeReport : Node
    {
        public NodeEnumeration CommonStatus { get; set; }

        public NodeSectionsList Sections { get; } = new();

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeReport(this);
        }

        public static NodeReport From(HealthReport healthReport)
        {
            var report = new NodeReport
            {
                CommonStatus = healthReport.Status
            };

            report.Sections.AddRange(healthReport.Entries.Select(x => NodeSection.From(x)));

            return report;
        }
    }
}
