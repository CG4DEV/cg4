﻿namespace CG4.DataAccess
{
    public class CG4SqlException : Exception
    {
        public CG4SqlException(string sql, Exception innerException)
            : base($"An error ocurred while executing SQL: {sql}", innerException)
        {
        }
    }
}
