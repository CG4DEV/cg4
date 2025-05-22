using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Represents a mapping between a .NET class and its corresponding database table.
    /// Contains metadata about the class, table name, schema, and property mappings.
    /// </summary>
    public struct ClassMap
    {
        /// <summary>
        /// Gets the database schema name for the mapped table.
        /// Can be null or empty if schemas are not used.
        /// </summary>
        public string SchemaName { get; private set; }

        /// <summary>
        /// Gets the database table name.
        /// Derived from the class name or specified by TableAttribute.
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// Gets the full name of the mapped .NET class.
        /// </summary>
        public string ClassName { get; private set; }

        /// <summary>
        /// Gets the Type object representing the mapped .NET class.
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// Gets an array of property mappings containing information about class properties
        /// and their corresponding database columns.
        /// </summary>
        public PropertyMap[] Properties { get; private set; }

        /// <summary>
        /// Creates a class mapping for a given .NET type by analyzing its attributes and properties.
        /// </summary>
        /// <param name="type">The .NET type to create mapping for.</param>
        /// <returns>A ClassMap instance containing the mapping information.</returns>
        /// <remarks>
        /// The mapping process considers:
        /// - TableAttribute for custom table names
        /// - Property attributes like Column, Key, DatabaseGenerated
        /// - Property types and nullability
        /// </remarks>
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

        /// <summary>
        /// Gets the property mapping for the ID/primary key property of the class.
        /// </summary>
        /// <returns>The PropertyMap for the ID property, or null if no ID property is found.</returns>
        public PropertyMap? GetIdPropertyMap()
        {
            return Properties.SingleOrDefault(x => x.IsIdentity);
        }

        /// <summary>
        /// Finds a property mapping by property name.
        /// </summary>
        /// <param name="name">The name of the property to find.</param>
        /// <returns>The PropertyMap for the specified property, or null if not found.</returns>
        public PropertyMap? GetPropertyMap(string name)
        {
            return Properties.SingleOrDefault(x => x.Name == name);
        }
    }
}
