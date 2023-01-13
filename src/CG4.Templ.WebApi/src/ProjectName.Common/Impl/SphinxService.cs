using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ProjectName.Common.Impl
{
    public class SphinxService : ISphinxService
    {
        private const int FAST_SEARCH_LIMIT = 10_000_000;

        private const int COMMAND_TIMEOUT = 300;
        
        private static readonly Dictionary<char, string[]> _escapeOperators = new[]
            {
                "\\", "(", ")", "|", "-", "!", "@", "~", "\"", "`", "$", "/", "^", "=", "<", "*", ",", "+", "&", "%", "#", 
                ".", ";", ":", "<", ">", "?", "_", "{", "}", "[", "]", "NEAR", "MAYBE", "SENTENCE", "PARAGRAPH", "ZONE",
                "ZONESPAN",
            }
            .GroupBy(x => char.ToLowerInvariant(x[0]))
            .ToDictionary(x => x.Key, x => x.ToArray());

        private static readonly string _escapeOperatorsChars =
            new(_escapeOperators.Keys.Union(_escapeOperators.Keys.Select(char.ToUpperInvariant)).ToArray());

        protected readonly ISphinxConnectionFactory _factory;

        public SphinxService(ISphinxConnectionFactory factory)
        {
            _factory = factory;
        }

        public string EscapeForExtendedSearch(string input)
        {
            var builder = new StringBuilder();
            var span = input.AsSpan();

            while (span.Length > 0)
            {
                var index = span.IndexOfAny(_escapeOperatorsChars);

                if (index < 0)
                {
                    builder.Append(span);
                    return builder.ToString();
                }

                builder.Append(span[..index]);

                var prevChar = index == 0 ? default(char?) : span[index - 1];
                span = span[index..];
                var operators = _escapeOperators[char.ToLowerInvariant(span[0])];

                foreach (var op in operators)
                {
                    if (span.StartsWith(op))
                    {
                        var isSpecialSymbol = prevChar is null or ' ' or '('
                                              && (span.Length == op.Length || span[op.Length] is ' ' or ':' or '/');

                        if (op.Length == 1 || isSpecialSymbol)
                        {
                            builder.Append('\\');
                        }

                        builder.Append(op);
                        span = span[op.Length..];
                        index = -1;
                        break;
                    }
                }

                if (index >= 0)
                {
                    builder.Append(span[0]);
                    span = span[1..];
                }
            }

            return builder.ToString();
        }

        public async Task<IEnumerable<TEntity>> SearchAsync<TEntity>(string sql, object param = null)
        {
            using var connection = await _factory.CreateAsync();
            return await connection.QueryAsync<TEntity>(sql, param, commandTimeout: COMMAND_TIMEOUT);
        }

        public async Task<TEntity> SearchByIdAsync<TEntity>(string sql, object param = null)
        {
            using var connection = await _factory.CreateAsync();
            return await connection.QuerySingleOrDefaultAsync<TEntity>(sql, param, commandTimeout: COMMAND_TIMEOUT);
        }
    }
}