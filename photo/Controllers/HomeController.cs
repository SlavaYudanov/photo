using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using photo.Clients;
using photo.Data;
using photo.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
namespace photo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Convert()
        {
            var view = new ConvertViewModel();
            view.FileNames = new List<string> ((string[])TempData["files"]);
            return View(view);
        }
        public IActionResult ConvertStart(ConvertViewModel convert)
        {
            var client = new ApiClient();
            var response = client.ConvertFiles(convert.FileNames.ToArray());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFileCollection uploadedFiles)
        {

            var guid = Guid.NewGuid();
            //добавить создание гуида для авторизованных по их id 

            //это работает неправильно :(  есть варик сделать userName + куки
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                // Получаем GUID из куки
                //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                guid = new Guid(userId);
                Console.WriteLine("User.Identity.Name " + guid);
            }
            else if (Request.Cookies.ContainsKey("uid"))
            {
                guid = new Guid(Request.Cookies["uid"]!);

            }
            else
            {
                Response.Cookies.Append("uid", guid.ToString());
            }

            if (uploadedFiles != null)
            {

                var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads/{guid}";
                // создаем папку для хранения файлов
                Directory.CreateDirectory(uploadPath);

                foreach (var file in uploadedFiles)
                {
                    // путь к папке uploads
                    string fullPath = $"{uploadPath}/{file.FileName}";

                    // сохраняем файл в папку uploads
                    using var fileStream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(fileStream);
                }
                TempData["files"] = uploadedFiles.Select(x => x.FileName ).ToList();
                return RedirectToAction("Convert") ;
            }
            return RedirectToAction("Index");
        }

        
    }
}