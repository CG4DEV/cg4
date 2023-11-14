using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProjectName.Contracts
{
    public class ErrorDetails
    {
        /// <summary>
        /// Короткое человекопонятное описание. От раза к разу это НЕ ДОЛЖНО изменяться.
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// HTTP код ответа ([RFC7231], Section 6).
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        /// Человекопонятное описание с подробностями проблемы.
        /// </summary>
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// Ошибки валидации.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
