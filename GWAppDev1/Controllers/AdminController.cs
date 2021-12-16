using GWAppDev1.Models;
using GWAppDev1.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GWAppDev1.ViewModels;

namespace GWAppDev1.Controllers
{
    [Authorize(Roles = Role.Admin)]
    public class AdminController : Controller
    {
            private ApplicationDbContext _context;
            private ApplicationSignInManager _signInManager;
            private ApplicationUserManager _userManager;
            public AdminController()
            {
                _context = new ApplicationDbContext();
            }

            public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
            {
                UserManager = userManager;
                SignInManager = signInManager;
            }
            public ApplicationSignInManager SignInManager
            {
                get
                {
                    return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }
                private set
                {
                    _signInManager = value;
                }
            }

            public ApplicationUserManager UserManager
            {
                get
                {
                    return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                private set
                {
                    _userManager = value;
                }
            }
            public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateStaff(RegisterViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Staff.Email, Email = viewModel.Staff.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var userId = user.Id;
                var newStaff = new Staff()
                {
                    Fullname = viewModel.Staff.Fullname,
                    Email = viewModel.Staff.Email,
                    Age = viewModel.Staff.Age,
                    Address = viewModel.Staff.Address
                };
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Staff);
                    _context.Staffs.Add(newStaff);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }

                AddErrors(result);
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateTrainer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrainer(RegisterViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Trainer.Email, Email = viewModel.Trainer.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var userId = user.Id;
                var newTrainer = new Trainer()
                {
                    UserId = userId,
                    Fullname = viewModel.Trainer.Fullname,
                    Age = viewModel.Trainer.Age,
                    Address = viewModel.Trainer.Address,
                    Specialty = viewModel.Trainer.Specialty
              
                };
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainer);
                    _context.Trainers.Add(newTrainer);
                    _context.SaveChanges();
                    return RedirectToAction("ShowTrainerInfo", "Admin");
                }
            
            AddErrors(result);
        }
            return View(viewModel);
    }

    private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public ActionResult ShowStaffInfo()
        {
            var Staffs = _context.Staffs.ToList();
            return View(Staffs);
            
        }
        public ActionResult ShowTrainerInfo()
        {
            var Trainers = _context.Trainers.ToList();
            return View(Trainers);
        }
        [HttpGet]
        public ActionResult EditTrainerInfo(int id)
        {
            var userId = User.Identity.GetUserId();
            var trainer = _context.Trainers.SingleOrDefault(t => t.Id == id && t.UserId == userId);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            var viewModel = new RegisterViewModels()
            {
                Trainer = trainer 
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTrainerInfo(RegisterViewModels viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Trainer.Email, Email = viewModel.Trainer.Email };
                var result = await UserManager.CreateAsync(user, viewModel.Password);
                var userId = user.Id;
                var newTrainer = new Trainer()
                {
                    UserId = userId,
                    Fullname = viewModel.Trainer.Fullname,
                    Age = viewModel.Trainer.Age,
                    Address = viewModel.Trainer.Address,
                    Specialty = viewModel.Trainer.Specialty

                };
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, Role.Trainer);
                    _context.Trainers.Add(newTrainer);
                    _context.SaveChanges();
                    return RedirectToAction("ShowTrainerInfo", "Admin");
                }

                AddErrors(result);
            }
            return View(viewModel);

        }
        public ActionResult  DeleteTrainer(int id)
        {
            var userId = User.Identity.GetUserId();
            var trainers = _context.Trainers.SingleOrDefault(t => t.Id == id && t.UserId == userId);
            if (trainers == null)
            {
                return HttpNotFound();
            }
            _context.Trainers.Remove(trainers);
            _context.SaveChanges();
            return RedirectToAction("ShowTrainerInfo");
        }

    }
}