using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moveis.Models;
using movies.Viewmodel;

namespace moveis.Controllers
{
    public class MovieController : Controller
    {

        private readonly AppDbContext _context;
        private new List<string> _allawextetion = new List<string> { ".jpg", ".png" };

        long maxsize = 10485760;

        public MovieController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.movies.ToListAsync();

            return View(model);

        }

        public async Task<IActionResult> Create()
        {
            var Viewmodel = new Moviefromviewmodel
            {
                Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync()
            };
            return View("movieform", Viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Moviefromviewmodel model)
        {

            if (!ModelState.IsValid)
            {
                model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                return View("movieform", model);
                

            }
            var file = Request.Form.Files;
            if (!file.Any())
            {
                model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "must add poster");
                return View("movieform", model);
            }

            var poster = file.FirstOrDefault();

            if (!_allawextetion.Contains(Path.GetExtension(poster.FileName.ToLower())))
            {
                model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "poster must end jpg or png ");
                return View("movieform", model);
            }

            if (poster.Length > 104857600)
            {
                model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                ModelState.AddModelError("Poster", "poster must lass than 50 mega ");
                return View("movieform", model);
            }

            using var datastream = new MemoryStream();

            await poster.CopyToAsync(datastream);

            var movie = new Movie
            {
                Titel = model.Titel,
                GanaraId = model.GanaraId,
                Year = model.Year,
                Rate = model.Rate,
                Storeline = model.Storeline,
                Poster = datastream.ToArray(),
            };
            _context.movies.Add(movie);
            _context.SaveChanges();




            return RedirectToAction(nameof(Index));
        }





        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("badrequist");
            }
            var movie = _context.movies.Find(id);
            if (movie == null)
            {
                return NotFound("notfound");

            }




            var viewmodel = new Moviefromviewmodel
            {
                Id = movie.Id,

                Titel = movie.Titel,
                GanaraId = movie.GanaraId,
                Year = movie.Year,
                Rate = movie.Rate,
                Storeline = movie.Storeline,
                Poster = movie.Poster,
                Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync()


            };



            return View("movieform", viewmodel);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Moviefromviewmodel model)
        {

            if (!ModelState.IsValid)
            {
                model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                return View("movieform", model);
            }

            var movie = await _context.movies.FindAsync(model.Id);

            if (movie == null)
            {
                return NotFound();
            }




            var file = Request.Form.Files;

            movie.Poster = model.Poster;

            if (file.Any())
            {
                var poster = file.FirstOrDefault();
                using var datastream = new MemoryStream();

                await poster.CopyToAsync(datastream);

                model.Poster = datastream.ToArray();

                if (!_allawextetion.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "poster must end jpg or png ");
                    return View("movieform", model);
                }

                if (poster.Length > maxsize)
                {
                    model.Genares = await _context.Ganaras.OrderBy(e => e.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "poster must lass than 10 mega ");
                    return View("movieform", model);
                }

                movie.Poster = model.Poster;

            }




            movie.Titel = model.Titel;
            movie.GanaraId = model.GanaraId;
            movie.Year = model.Year;
            movie.Rate = model.Rate;
            movie.Storeline = model.Storeline;
            movie.Poster = model.Poster;



            _context.SaveChanges();




            return RedirectToAction(nameof(Index));
        }





        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.movies.Include(m => m.Ganara).SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var movie = await _context.movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _context.movies.Remove(movie);
            _context.SaveChanges();

           
            return RedirectToAction(nameof(Index));
        }


    }



















































}


