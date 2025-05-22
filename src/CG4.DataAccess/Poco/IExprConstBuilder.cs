using System;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Defines an interface for building SQL constant expressions from .NET types.
    /// Used to customize how specific .NET types are converted to SQL literals.
    /// </summary>
    public interface IExprConstBuilder
    {
        /// <summary>
        /// Builds a SQL constant expression for a given .NET type and value.
        /// </summary>
        /// <param name="type">The .NET type to convert.</param>
        /// <param name="value">The value to convert to a SQL constant.</param>
        /// <returns>An ExprConst representing the value in SQL format.</returns>
        ExprConst Build(Type type, object value);
    }
}
