using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DutResLeague.Models; // Adjust this namespace according to your project structure

namespace YourNamespace.Controllers
{
    public class ClubManagementController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();

        // GET: ClubManagement
        public ActionResult Index()
        {
            if (Session["Email"] == null || Session["Role"]?.ToString() != "sportAdmin")
            {
                return RedirectToAction("Login", "Account");
            }

            var clubs = _context.Clubs.ToList();
            var coaches = _context.Users.Where(u => u.Role == UserRole.coach).ToList();

            ViewBag.Coaches = new SelectList(coaches, "Id", "Email");
            return View(clubs);
        }

        [HttpPost]
        public ActionResult AddClub(string clubName, int coachId)
        {
            var existingClub = _context.Clubs.Any(c => c.ClubName == clubName);
            if (existingClub)
            {
                TempData["ErrorMessage"] = "A club with this name already exists.";
                return RedirectToAction("Index");
            }

            var coachAssigned = _context.Clubs.Any(c => c.CoachId == coachId);
            if (coachAssigned)
            {
                TempData["ErrorMessage"] = "This coach is already assigned to another club.";
                return RedirectToAction("Index");
            }

            var newClub = new Club { ClubName = clubName, CoachId = coachId };
            _context.Clubs.Add(newClub);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Club added successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddPlayer(string playerName, int clubId)
        {
            var newPlayer = new Player { PlayerName = playerName, ClubId = clubId };
            _context.Players.Add(newPlayer);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Player added successfully.";
            return RedirectToAction("Index");
        }

        public ActionResult DeleteClub(int id)
        {
            var club = _context.Clubs.Find(id);
            if (club != null)
            {
                _context.Clubs.Remove(club);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult ClubDetails(int id)
        {
            var club = _context.Clubs.Find(id);
            if (club == null)
            {
                return HttpNotFound();
            }

            var players = _context.Players.Where(p => p.ClubId == id).ToList();
            ViewBag.Players = players;
            return View(club);
        }
    }
}
