﻿namespace CG4.Executor
{
    /// <summary>
    /// Интерфейс создания и исполнения стори.
    /// </summary>
    public interface IStoryExecutor
    {
        /// <summary>
        /// Создает и исполняет стори.
        /// </summary>
        /// <typeparam name="TStoryResult">Тип возвращаемого значения.</typeparam>
        /// <param name="context">Контекст стори.</param>
        /// <returns>Возвращаемое значение.</returns>
        Task<TStoryResult> Execute<TStoryResult>(IResult<TStoryResult> context);

        /// <summary>
        /// Создания и исполнение стори.
        /// </summary>
        /// <param name="context">Контекст стори.</param>
        Task Execute(IResult context);
    }
}
