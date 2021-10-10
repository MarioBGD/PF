using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PF.Mobile.App.DAL.Api
{public class ApiRequest<T>
    {

        public async Task<ApiResult<T>> Invoke(string query, Method method = Method.Get, object body = null)
        {
            ApiResult<T> result = new ApiResult<T>();

            try
            {
                RestClient client = new RestClient(new Uri(Config.ApiConnectionString + query));
                client.Timeout = 30000;

                RestRequest request = new RestRequest(MethodToRestMethod(method));
                request.AddHeader("Authentication", SessionManager.AuthToken ?? "no auth");

                if (body != null)
                {
                    request.AddJsonBody(JsonConvert.SerializeObject(body));
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicy) => { return true; };

                var resp = await client.ExecuteAsync(request);
                
                result.StatusCode = resp.StatusCode;

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                    ApiClient.OnUnauthorized();

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
                Debug.WriteLine(e);
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