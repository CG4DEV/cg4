using System.Reflection;

namespace CG4.DataAccess.Poco
{
    public struct PropertyMap
    {
        public string Name { get; internal set; }

        public string ColumnName { get; internal set; }

        public bool IsIgnored { get; internal set; }

        public bool AllowEdit { get; internal set; }

        public bool IsIdentity { get; internal set; }

        public bool IsPrymaryKey { get; internal set; }

        public PropertyInfo PropertyInfo { get; set; }
    }
}
