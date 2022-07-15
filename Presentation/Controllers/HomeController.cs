using Demo.Core.Domain.DTOs;
using Demo.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var url = "https://localhost:44352/api/student";

            HttpClient client = new HttpClient();

            var res = await client.GetStringAsync(url);
            ViewBag.Students = new StudentDTO();

            var json = JsonConvert.DeserializeObject<ResponseViewModel<IEnumerable<StudentDTO>>>(res);

            return View(json.Data);
        }

        public async Task<IActionResult> LoadData()
        {
            var url = "https://localhost:44352/api/student";

            HttpClient client = new HttpClient();

            var res = await client.GetStringAsync(url);

            var json = JsonConvert.DeserializeObject<ResponseViewModel<IEnumerable<StudentDTO>>>(res);

            return Json(json.Data);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentDTO student)
        {
            var json = JsonConvert.SerializeObject(student);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:44352/api/student";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Get(int id)
        {
            var url = $"https://localhost:44352/api/student/{id}";

            HttpClient client = new HttpClient();

            var res = await client.GetStringAsync(url);

            var json = JsonConvert.DeserializeObject<ResponseViewModel<StudentDTO>>(res);

            return Json(json.Data);
        }
        public async Task<IActionResult> EditStudent(int id)
        {
            var url = $"https://localhost:44352/api/student/{id}";

            HttpClient client = new HttpClient();

            var res = await client.GetStringAsync(url);

            var json = JsonConvert.DeserializeObject<ResponseViewModel<StudentDTO>>(res);

            return Json(json.Data);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentDTO student , int id)
        {
            var json = JsonConvert.SerializeObject(student);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:44352/api/student/Edit/{student}";

            HttpClient client = new HttpClient();

            var response = await client.PutAsync(url , data);

            var result = await response.Content.ReadAsStringAsync();

            return Json(result);
        }
        public async Task<IActionResult> Delete(int id)
        {

            var url = $"https://localhost:44352/api/student/Delete/{id}";

            HttpClient client = new HttpClient();

            var res = await client.DeleteAsync(url);

            return Json(res);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
