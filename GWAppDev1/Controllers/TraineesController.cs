﻿using GWAppDev1.Models;
using GWAppDev1.Utils;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GWAppDev1.Controllers
{
    [Authorize(Roles = Role.Trainee)]
    public class TraineesController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public TraineesController()
        {
            _context = new ApplicationDbContext();
        }

        public TraineesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        // GET: Trainee
        public ActionResult Index()
        {
            var trainee = _context.Trainees.ToList();
            return View(trainee);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var trainee = _context.Trainees.SingleOrDefault(t => t.Id == id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }
    }
}