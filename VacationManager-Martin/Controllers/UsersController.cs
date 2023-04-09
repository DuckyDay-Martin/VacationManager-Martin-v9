using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VacationManager_Martin.Data;
using VacationManager_Martin.Data.Entities;
using VacationManager_Martin.Models;

//Checked//

namespace VacationManager_Martin.Controllers
{
    //[Authorize(Roles = Global.RoleConstants.Roles.CEO)]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index(string sortOrder)
        {
            List<User> users = _context.Users.ToList();
            List<UserViewModel> userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel() { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, UserName = user.UserName });
            }


            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "LastName_desc" : "";

            var _users = from user in _context.Users 
                         select user;

            switch (sortOrder)
            {
                case "FirstName_desc":
                    _users = _users.OrderByDescending(s => s.FirstName);
                    break;

                case "LastName_desc":
                    _users = _users.OrderByDescending(s => s.LastName);
                    break;
     
                default:
                    _users = _users.OrderBy(s => s.LastName);
                    break;
            }


            return View(userViewModels);
        }

        // GET: Users/Details/5
        //[Authorize(Roles = Global.RoleConstants.Roles.CEO)]
        public async Task<IActionResult> Details(string id, UserViewModel userViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users.Find(id);

            userViewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TeamId = user.TeamId
            };
            return View(userViewModel);
        }

        //GET: Users/Create
        //[Authorize(Roles = Global.RoleConstants.Roles.CEO)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( UserViewModel userViewModel) 
        {
            if (ModelState.IsValid)
            {

                var user = new User();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.UserName = userViewModel.UserName;             
                user.TeamId= userViewModel.TeamId;

                await _userManager.CreateAsync(user, userViewModel.Password);
                
                await _userManager.AddPasswordAsync(user, userViewModel.Password);

                _context.Users.Add(user);
                
                _context.Update(user);
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            var user = new User();     
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TeamId = user.TeamId
            };

            return View(userViewModel);
        }


        // POST: Users/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserViewModel userViewModel)
        {           
            var user = new User();
            user = _context.Users.Find(id);
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.UserName = userViewModel.UserName;            
            user.TeamId = userViewModel.TeamId;

            if (ModelState.IsValid)
            {               
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = new User();
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TeamId = user.TeamId
            };

            return View(userViewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, UserViewModel userViewModel)
        {
            var user = new User();
            user = _context.Users.Find(id);
            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.UserName = userViewModel.UserName;
            user.TeamId = userViewModel.TeamId;

            if (userViewModel != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }       
    }
}
