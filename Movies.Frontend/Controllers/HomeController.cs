using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Movies.Frontend.Models;
using Movies.Frontend.Models.DataTransferObjects;
using Movies.Frontend.Models.Enums;
using Movies.Frontend.Services.IService;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace Movies.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IWebHostEnvironment _environment;

        public HomeController(IMovieService movieService, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _logger = logger;
            _environment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //Initializing the movie list that will be shown in the home page
            List<MovieDto> list = new();

            var response = await _movieService.GetAllMoviesAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MovieDto>>(Convert.ToString(response.Result)); //converting the json response body to a MovieDto list
            }
            else
            {
                TempData["error"] = response?.ErrorMessage; //Toastr will show an error window with the error message (as configured in the Shared/_Notifications file)
            }
            return View(list);
        }

        public async Task<IActionResult> MovieDetails(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(Id.Value);
            if (response != null && response.IsSuccess)
            {
                MovieDto model = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Result));

                return View(model);
            }
            else
            {
                TempData["error"] = response?.ErrorMessage;
            }
            return NotFound();
        }

        public async Task<IActionResult> MovieCreate()
        {
            //giving the Enum values to this select list item so it can be displayed in the create form
            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                }).ToList();

            ViewBag.Genres = genres;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> MovieCreate(MovieDto model)
        {
            if (ModelState.IsValid)
            {

                if (model.UploadedImages != null && model.UploadedImages.Any())
                {
                    var imagePaths = new List<string>();

                    foreach (var image in model.UploadedImages)
                    {
                        if (image.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName); //building the path for the image to be saved
                            var filePath = Path.Combine(uploadsFolder, fileName);

                            Directory.CreateDirectory(uploadsFolder);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream); //image saved
                            }

                            imagePaths.Add("/uploads/" + fileName);
                        }
                    }
                    model.ImagePaths = imagePaths;
                }

                var response = await _movieService.CreateMovieAsync<ResponseDto>(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.ErrorMessage;
                }
                
            }

            //giving the viebag the genre values in case the user is gonna fill the form again
            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>()
               .Select(g => new SelectListItem
               {
                   Value = ((int)g).ToString(),
                   Text = g.ToString()
               }).ToList();

            ViewBag.Genres = genres;

            return View(model);

        }

        public async Task<IActionResult> MovieEdit(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(Id.Value);
            if (response != null && response.IsSuccess)
            {
                MovieDto model = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Result));

                var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>()
                .Select(g => new SelectListItem
                {
                    Value = ((int)g).ToString(),
                    Text = g.ToString()
                }).ToList();

                ViewBag.Genres = genres;
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
                if (model.UploadedImages != null && model.UploadedImages.Any())
                {
                    var imagePaths = new List<string>();

                    foreach (var image in model.UploadedImages)
                    {
                        if (image.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                            var filePath = Path.Combine(uploadsFolder, fileName);

                            Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await image.CopyToAsync(fileStream);
                            }

                            imagePaths.Add("/uploads/" + fileName);
                        }
                    }
                    model.ImagePaths = imagePaths;
                }

                var response = await _movieService.UpdateMovieAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {                        
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response?.ErrorMessage;
                }
            }

            var genres = Enum.GetValues(typeof(Genre)).Cast<Genre>()
               .Select(g => new SelectListItem
               {
                   Value = ((int)g).ToString(),
                   Text = g.ToString()
               }).ToList();

            ViewBag.Genres = genres;

            return View(model);
        }

        public async Task<IActionResult> MovieDelete(int? Id)
        {
            var response = await _movieService.GetMovieByIdAsync<ResponseDto>(Id.Value); //retrieving the specific movie that is going to be deleted
            if (response != null && response.IsSuccess)
            {
                MovieDto model = JsonConvert.DeserializeObject<MovieDto>(Convert.ToString(response.Result)); 
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
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
                else
                {
                    TempData["error"] = response?.ErrorMessage;
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
