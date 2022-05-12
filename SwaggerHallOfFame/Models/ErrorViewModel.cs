namespace SwaggerHallOfFame.Models
{
    /// <summary>  
    ///  Класс для вывода ошибок.  
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>  
        ///  Идентификатор ошибки.  
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>  
        ///  True/false, проверка на ошибку.  
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}