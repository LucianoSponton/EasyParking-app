using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWebApi
{
    public class WebApiDetele // ESTE CUANDO SOLO ENVIAMOS
    {
        private WebApiAccess _webApiAccess;

        public WebApiDetele(WebApiAccess webApiAccess)
        {
            _webApiAccess = webApiAccess;
        }

        public async Task DeleteAsync(string uri)
        {
            try
            {
                int intento = 0;
                do
                {
                    intento += 1;
                    HttpResponseMessage response = await _webApiAccess.HttpClient.DeleteAsync(uri);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return;
                    }
                    else
                    {
                        if (intento >= _webApiAccess.Retry)
                        {
                            string errorcontent = response.Content.ReadAsStringAsync().Result;
                            throw new Exception($"ERROR ... {response.StatusCode.ToString()} - {errorcontent}");

                        }
                    }

                } while (true);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
