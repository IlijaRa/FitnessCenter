using FitnessCenterMVC.Data;
using FitnessCenterMVC.ModelMapper;
using FitnessCenterMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var all_users = new AllUsersViewModel();
            var coaches = await _context.Coach.ToListAsync();
            var members = await _context.FitnessCenterMember.ToListAsync();
            var administrators = await _context.Administrator.Where(x => x.UserName != User.Identity.Name).ToListAsync();// Administrator gets all administrators except himself

            foreach (var coach in coaches)
            {
                all_users.coaches.Add(CoachMapper.ConvertToCoachViewModel(coach));
            }

            foreach (var member in members)
            {
                all_users.members.Add(FitnessCenterMemberMapper.ConvertToCoachViewModel(member));
            }

            foreach (var administrator in administrators)
            {
                all_users.administrators.Add(AdministratorMapper.ConvertToAdministratorViewModel(administrator));
            }

            return View(all_users);
        }
    }
}
