namespace PF.App.Core.DAL.Contracts.Models
{
    public class ApiResult<T>
    {
        public System.Net.HttpStatusCode StatusCode { get; set; } = System.Net.HttpStatusCode.BadRequest;

        public bool IsOk => (StatusCode == System.Net.HttpStatusCode.OK);
        public T ResultContent { get; set; }
        //public bool IsResultContent => ResultContent == default(T);

        public string Message { get; set; }
    }
}