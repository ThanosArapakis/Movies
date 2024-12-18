using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Frontend.Models;
using Movies.Frontend.Models.DataTransferObjects;
using Movies.Frontend.Services.IService;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Movies.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService, ILogger<HomeController> logger)
        {
            _movieService = movieService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<MovieDto> list = new();

            var response = await _movieService.GetAllMoviesAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MovieDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> MovieCreate()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> MovieCreate(MovieDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _movieService.CreateMovieAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> MovieEdit(int productId)
        {
            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                MovieDto model = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieEdit(MovieDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _movieService.UpdateMovieAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> MovieDelete(int productId)
        {
            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                MovieDto model = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MovieDelete(MovieDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _movieService.DeleteMovieAsync<ResponseDto>(model.Id);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
