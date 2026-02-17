using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using RestSharp;
using System.Net;
using System.Text;

namespace Review
{
    class HttpToSound
    {
        public async Task PlayFilePostAsync(string url) {
            /*var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://127.0.0.1:12080/user/urlplay"),
                Content = new MultipartFormDataContent
                {
                    new StringContent("6")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "id",
                            }
                        }
                    },
                    new StringContent("1e16985449ff0e67cc22a11b8b66746d3ac75774")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "token",
                            }
                        }
                    },
                    new StringContent("http://192.168.1.29:12080/mp3/4/photograph.mp3")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "url",
                            }
                        }
                    },
                    new StringContent("f9cae3c4b2358781e580b35445cb82a4")
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "snlist",
                            }
                        }
                    },
                },
            };
            //Console.WriteLine(MyConvert.ByteToString());
            Console.WriteLine(request.Content.Headers.ContentLength);
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }*/
            var options = new RestClientOptions("http://127.0.0.1:12080/user/urlplay")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("http://127.0.0.1:12080/user/urlplay", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.MultipartFormQuoteParameters= true;
            //request.AddHeader("content-type", "multipart/form-data; boundary=---011000010111000001101001");
            request.AddParameter("id", "6");
            request.AddParameter("token", "1e16985449ff0e67cc22a11b8b66746d3ac75774");
            request.AddParameter("url", "http://192.168.1.29:12080/mp3/6/e6b58be8af9532.mp3");
            request.AddParameter("snlist", "f9cae3c4b2358781e580b35445cb82a4");
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }
    }

}

