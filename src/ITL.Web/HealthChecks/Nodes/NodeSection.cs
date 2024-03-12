using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ITL.Web.HealthChecks.Nodes
{
    public class NodeSection : Node
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public NodeAddDataList AdditionalData { get; } = new();

        public NodeTagsList Tags { get; } = new();

        public NodeEnumeration Status { get; set; }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeSection(this);
        }

        public static NodeSection From(KeyValuePair<string, HealthReportEntry> reportEntry)
        {
            var section = new NodeSection
            {
                Name = reportEntry.Key,
                Description = reportEntry.Value.Description,
                Status = reportEntry.Value.Status,
            };

            section.Tags.AddRange(reportEntry.Value.Tags.Select(x => NodeStringValue.From(x)));

            section.AdditionalData.AddRange(reportEntry.Value.Data.Select(x => NodeKeyValue.From(x)));

            return section;
        }
    }
}
