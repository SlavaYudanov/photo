using Microsoft.AspNetCore.Mvc;
using Aspose.Words;
using Aspose.Words.Saving;
using System.Data;
using photoLibrary;
using Aspose.Words.Fields;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace photoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private static SaveFormat GetFormat(string format)
        {
            switch(format)
            {
                case "jpg":
                    return SaveFormat.Jpeg;

                case "gif":
                    return SaveFormat.Gif;

                case "png":
                    return SaveFormat.Png;

                case "tiff":
                    return SaveFormat.Tiff;

                case "bmp":
                    return SaveFormat.Bmp;

                case "svg":
                    return SaveFormat.Svg;

                case "emf":
                    return SaveFormat.Emf;

                default:
                    return SaveFormat.Jpeg;
            }
        }

        // POST api/<PhotoController>
        [HttpPost]
        public IActionResult Post([FromBody] ConvertRequest convertRequest)
        {
            var files = new List<string>();

            try
            {
                var doc = new Document();
                var builder = new DocumentBuilder(doc);

                for (int i = 0; i < convertRequest.ConvertFile.Length; i++)
                {
                    var filePath = $"C:/uploads/{convertRequest.FolderGuid}/{convertRequest.ConvertFile[i]}";
                    var subs = convertRequest.ConvertFile[i].Split('.');
                    var newFile = string.Join(".", subs.Take(new System.Range(0, subs.Length - 1))) ?? "undefined.";
                    newFile = $"C:/uploads/{convertRequest.FolderGuid}/{newFile}.{convertRequest.ConvertType[i]}";
                    var shape = builder.InsertImage(filePath);
                    shape.GetShapeRenderer().Save(newFile, new ImageSaveOptions(GetFormat(convertRequest.ConvertType[i])));
                    System.IO.File.Delete(filePath);

                    files.Add(newFile);
                }
            }
            catch(Exception)
            {
                return NotFound();
            }

            return Ok(files);
        }
    }
}
