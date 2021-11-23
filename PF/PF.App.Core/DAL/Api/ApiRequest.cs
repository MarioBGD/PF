using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PF.App.Core.DAL.Contracts;
using PF.App.Core.DAL.Contracts.Models;
using RestSharp;
using Splat;

namespace PF.App.Core.DAL.Api
{
    public class ApiRequest<T>
    {
        //private static readonly string ApiConnectionString = "http://192.168.0.53:44317/api/";
        private static readonly string ApiConnectionString = "http://pfapp.hostingasp.pl/api/";
        
        public async Task<ApiResult<T>> Invoke(string query, string authToken, Method method = Method.Get, object body = null)
        {
            ApiResult<T> result = new ApiResult<T>();
            try
            {
                RestClient client = new RestClient(new Uri(ApiConnectionString + query));
                client.Timeout = 30000;

                RestRequest request = new RestRequest(MethodToRestMethod(method));
                request.AddHeader("Authentication", authToken ?? "no auth");

                if (body != null)
                {
                    request.AddJsonBody(JsonConvert.SerializeObject(body));
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicy) => { return true; };

                var resp = await client.ExecuteAsync(request);
                
                result.StatusCode = resp.StatusCode;

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Locator.Current.GetService<ISessionManager>()?.Unauthorize();
                }

                if (!string.IsNullOrEmpty(resp.Content))
                    try
                    {
                        result.ResultContent = JsonConvert.DeserializeObject<T>(resp.Content);
                    }
                    catch (Exception e)
                    {

                    }

                result.Message = "ok";
            }
            catch (Exception e)
            {
                result.Message = e.ToString();
                //Debug.WriteLine(e);
            }

            return result;
        }


        private RestSharp.Method MethodToRestMethod(Method method)
        {
            
            switch (method)
            {
                case Method.Get:
                    return RestSharp.Method.GET;
                case Method.Post:
                    return RestSharp.Method.POST;
                case Method.Put:
                    return RestSharp.Method.PUT;
                case Method.Delete:
                    return RestSharp.Method.DELETE;
            }

            return RestSharp.Method.GET;
        }
    }

    public enum Method
    { Get, Post, Put, Delete }
}