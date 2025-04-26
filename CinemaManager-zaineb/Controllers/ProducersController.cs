using CinemaManager_zaineb.Models.Cinema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaManager_zaineb.Controllers
{
    public class ProducersController : Controller
    {
        CinemaDbContext _context;
        public ProducersController(CinemaDbContext context)
        {
            _context = context;
        }

        //Partie B
        public IActionResult MoviesAndTheirProds_UsingModel()
        {
            var moviesAndProducers = from m in _context.Movies
                                     join p in _context.Tables on m.ProducerId equals p.Id
                                     select new ProdMovie
                                     {
                                         mTitle = m.Title,
                                         mGenre = m.Genre,
                                         pName = p.Name,
                                         pNat = p.Nationality
                                     };

            return View(moviesAndProducers.ToList());
        }



        // Action ProdsAndTheirMovies pour afficher les producteurs et leurs films
        public async Task<IActionResult> ProdsAndTheirMovies()
        {
            var producersWithMovies = await _context.Tables
                .Include(p => p.Movies) // Inclure les films associés à chaque producteur
                .ToListAsync();

            return View(producersWithMovies);
        }



        // GET: ProducersController
        public ActionResult Index()
        {
            return View(_context.Tables.ToList());
        }

        // GET: ProducersController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Retourne une erreur 404 si l'ID est null
            }

            var producer = _context.Tables.Find(id); // Recherche du producteur par ID
            if (producer == null)
            {
                return NotFound(); // Retourne une erreur 404 si le producteur n'existe pas
            }

            return View(producer); // Envoie le producteur à la vue Details
        }


        // GET: ProducersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProducersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Name,Nationality,Email")] Table producer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(producer);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(producer);
            }
            catch
            {
                return View(producer);
            }
        }


        // GET: ProducersController/Edit/5
        public ActionResult Edit(int id)
        {
            var producer = _context.Tables.Find(id); // Recherche du producteur par ID
            if (producer == null)
            {
                return NotFound(); // Renvoie une erreur 404 si le producteur n'existe pas
            }
            return View(producer); // Passe le producteur à la vue
        }


        
        // POST: ProducersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,Nationality,Email")] Table producer)
        {
            if (id != producer.Id)
            {
                return NotFound(); // Vérifie que l'ID de l'URL correspond à l'ID du modèle
            }

            try
            {
                if (ModelState.IsValid) // Valide les données du formulaire
                {
                    _context.Update(producer); // Met à jour le producteur en base de données
                    _context.SaveChanges(); // Sauvegarde les modifications
                    return RedirectToAction(nameof(Index)); // Redirige vers la liste des producteurs
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur lors de la mise à jour : " + ex.Message);
            }

            return View(producer); // Renvoie à la vue en cas d'erreur
        }


        // GET: ProducersController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Renvoie une erreur 404 si l'ID est null
            }

            var producer = _context.Tables.Find(id); // Recherche du producteur par ID
            if (producer == null)
            {
                return NotFound(); // Renvoie une erreur 404 si le producteur n'existe pas
            }

            return View(producer); // Passe le producteur à la vue de confirmation
        }


        // POST: ProducersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var producer = _context.Tables.Find(id); // Recherche du producteur par ID
                if (producer != null)
                {
                    _context.Tables.Remove(producer); // Supprime le producteur
                    _context.SaveChanges(); // Enregistre la suppression en base de données
                }
                return RedirectToAction(nameof(Index)); // Redirige vers la liste des producteurs
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erreur lors de la suppression : " + ex.Message);
                return View();
            }
        }

    }
}
