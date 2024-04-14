using RestSharp;

namespace photo.Clients
{
    public class ApiClient
    {
        RestClient client = new RestClient("http://localhost:5098/api");

        public string ConvertFiles(string[] files)
        {
            var request = new RestRequest("/Photo", Method.Post);
            request.AddBody(files);
            var response = client.Execute(request);
            return response.Content;
        }
    }
}
