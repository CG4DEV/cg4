using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG4.Impl.Dapper
{
    //public class DapperLoggerInterceptor : SqlMapper.IInterceptor
    //{
    //    private readonly ILogger<DapperLoggerInterceptor> _logger;

    //    public DapperLoggerInterceptor(ILogger<DapperLoggerInterceptor> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public void BeforeExecute(IDbCommand command)
    //    {
    //        var parameters = string.Join(", ",
    //            command.Parameters.Cast<IDbDataParameter>()
    //                .Select(p => $"{p.ParameterName}={p.Value}"));

    //        _logger.LogInformation("""
    //        Dapper Query:
    //        SQL: {Sql}
    //        Parameters: {Parameters}
    //        """,
    //            command.CommandText,
    //            parameters);

    //        command.Properties["Stopwatch"] = Stopwatch.StartNew();
    //    }

    //    public void AfterExecute(IDbCommand command)
    //    {
    //        if (command.Properties["Stopwatch"] is Stopwatch sw)
    //        {
    //            sw.Stop();
    //            _logger.LogDebug("Query executed in {ElapsedMs} ms", sw.ElapsedMilliseconds);
    //        }
    //    }
    //}
}
