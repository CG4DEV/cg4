using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CG4.Impl.Dapper.Poco
{
    public struct ClassMap
    {
        public string SchemaName { get; private set; }

        public string TableName { get; private set; }

        public string ClassName { get; private set; }

        public Type EntityType { get; private set; }

        public PropertyMap[] Properties { get; private set; }

        internal static ClassMap Map(Type type)
        {
            ClassMap map = new()
            {
                // Map Class
                EntityType = type,
                ClassName = type.Name
            };

            foreach (var attr in type.GetCustomAttributes())
            {
                switch (attr)
                {
                    case TableAttribute:
                        map.TableName = ((TableAttribute)attr).Name;
                        break;
                }
            }

            // Map Properties
            var properties = type.GetProperties();

            map.Properties = new PropertyMap[properties.Length];

            for (int i = 0; i < properties.Length; i++)
            {
                PropertyMap pm = new()
                {
                    Name = properties[i].Name,
                    PropertyInfo = properties[i],
                    AllowEdit = true,
                };

                foreach (var item in properties[i].GetCustomAttributes())
                {
                    switch (item)
                    {
                        case ColumnAttribute attr:
                            pm.ColumnName = attr.Name;
                            break;

                        case KeyAttribute:
                            pm.IsPrymaryKey = true;
                            break;

                        case DatabaseGeneratedAttribute attr:
                            pm.IsIdentity = attr.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity;
                            break;

                        case EditableAttribute attr:
                            pm.AllowEdit = attr.AllowEdit;
                            break;
                        
                        case NotMappedAttribute:
                            pm.IsIgnored = true;
                            break;
                    }
                }

                map.Properties[i] = pm;
            }

            return map;
        }

        public PropertyMap GetIdentity()
        {
            return Properties.SingleOrDefault(x => x.IsIdentity);
        }
    }
}
