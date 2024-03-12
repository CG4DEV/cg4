namespace ITL.Web.HealthChecks.Nodes
{
    public class NodeEnumeration : Node
    {
        public long Value { get; set; }

        public string Name { get; set; }

        public static implicit operator NodeEnumeration(Enum @enum)
        {
            return new NodeEnumeration
            {
                Name = @enum.ToString(),
                Value = GetValue(@enum)
            };
        }

        public override void Visit(INodeVisitor visitor)
        {
            visitor.VisitNodeEnumeration(this);
        }

        private static long GetValue(Enum @enum)
        {
            var t = @enum.GetTypeCode();

            return t switch
            {
                TypeCode.Int16 => (short)(object)@enum,
                TypeCode.UInt16 => (ushort)(object)@enum,
                TypeCode.Int32 => (int)(object)@enum,
                TypeCode.UInt32 => (uint)(object)@enum,
                TypeCode.Int64 => (long)(object)@enum,
                TypeCode.Byte => (long)(object)@enum,
                _ => throw new NotSupportedException($"Enum value: {@enum} isn't supported!"),
            };
        }
    }
}
