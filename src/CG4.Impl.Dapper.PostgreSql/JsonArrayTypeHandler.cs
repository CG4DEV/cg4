using System.Data;
using System.Text.Json.Nodes;
using Dapper;
using Npgsql;
using NpgsqlTypes;

namespace CG4.Impl.Dapper.PostgreSql
{
    /// <summary>
    /// Custom Dapper type handler for mapping between <see cref="JsonArray"/> and PostgreSQL JSONB type.
    /// </summary>
    public class JsonArrayTypeHandler : SqlMapper.TypeHandler<JsonArray>
    {
        /// <summary>
        /// Parses the PostgreSQL JSONB value to a <see cref="JsonArray"/> object.
        /// </summary>
        /// <param name="value">The JSON string value from the database.</param>
        /// <returns>A parsed <see cref="JsonArray"/> or null if the value could not be parsed.</returns>
        public override JsonArray Parse(object value) => value is string json ? JsonNode.Parse(json).AsArray() : null;

        /// <summary>
        /// Sets the parameter value for PostgreSQL JSONB type when sending data to the database.
        /// </summary>
        /// <param name="parameter">The database parameter.</param>
        /// <param name="value">The <see cref="JsonArray"/> value to set.</param>
        public override void SetValue(IDbDataParameter parameter, JsonArray value)
        {            
            parameter.Value = value.ToJsonString();

            ((NpgsqlParameter)parameter).NpgsqlDbType = NpgsqlDbType.Jsonb;
        }
    }
}