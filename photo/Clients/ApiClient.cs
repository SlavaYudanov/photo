using Microsoft.AspNetCore.Mvc;
using photoLibrary;
using RestSharp;

namespace photo.Clients
{
    public class ApiClient
    {
        RestClient client = new RestClient("http://localhost:5098/api");

        public List<string> ConvertFiles(string folderGuid, string[] convertFile, string[] convertType)
        {
            var convertRequest = new ConvertRequest { FolderGuid = folderGuid, ConvertFile = convertFile, ConvertType = convertType };
            var request = new RestRequest("/Photo", Method.Post);
            request.AddBody(convertRequest);
            var response = client.Execute<List<string>>(request);

            return response.Data;
        }
    }   
}
