namespace ITL.DataAccess.Exceptions
{
    public class ITLSqlException : Exception
    {
        public ITLSqlException(string sql, Exception innerException)
            : base($"An error ocurred while executing SQL: {sql}", innerException)
        {
        }
    }
}
