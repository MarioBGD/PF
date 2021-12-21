namespace PF.App.Core.DAL.Contracts.Models
{
    public class ServiceResult
    {
        public ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        
        public bool Success { get; }
        public string Message { get; }
    }
}