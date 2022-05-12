namespace SwaggerHallOfFame.Models
{
    /// <summary>  
    ///  ����� ��� ������ ������.  
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>  
        ///  ������������� ������.  
        /// </summary>
        public string? RequestId { get; set; }
        /// <summary>  
        ///  True/false, �������� �� ������.  
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}