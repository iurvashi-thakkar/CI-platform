﻿using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CI_platform.Controllers
{
    public class MissionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public MissionController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
            this._logger = logger;
            //_emailService = emailService;
            
        } 

        public IActionResult HomePage(string sort="",long id = 0)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);
            ViewBag.User = user;
            List<Country> countryList = _unitOfWork.Country.GetAll().ToList();
            ViewBag.Countries = countryList;
            if (id != 0)
            {
                List<City> cityList = _unitOfWork.City.GetAll().Where(c => c.CountryId == id).ToList();
                ViewBag.Cities = cityList;
            }
            else
            {
                List<City> cityList = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined").ToList();
                ViewBag.Cities = cityList;
            }
            List<MissionTheme> themeList = _unitOfWork.MissionTheme.GetAll().ToList();
            ViewBag.Themes = themeList;

            List<Skill> skillList = _unitOfWork.Skill.GetAll().ToList();
            ViewBag.Skills = skillList;

            List<Mission> missionsList = _unitOfWork.Mission.GetMissionCard().ToList();
            if (sort == "")
            {
               
                ViewBag.Missions = missionsList;
            }
            else if (sort == "newest")
            {
                ViewBag.Missions = missionsList.OrderByDescending(m => m.StartDate).ToList();
            }
            else if(sort== "oldest")
            {
                ViewBag.Missions = missionsList.OrderBy(m => m.StartDate).ToList();
            }

            return View();
        }
        //[HttpGet]
        //public IActionResult SortByNew()
        //{
        //    List<Mission> missionsList = _unitOfWork.Mission.GetMissionCard().ToList();


        //    List<Mission> missionsList1 = missionsList.OrderBy(u1 => u1.StartDate).ToList();
        //    ViewBag.Missions = missionsList1;
        //    return RedirectToAction("HomePage");
        //}

        //public IActionResult MissionPage()
        //{
        //    var sessionValue = HttpContext.Session.GetString("UserEmail");
        //    if (String.IsNullOrEmpty(sessionValue))
        //    {
        //        TempData["error"] = "Session Expired! Please Login Again!";
        //        return RedirectToAction("Index", "Home");
        //    }
        //    var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);
        //    ViewBag.User = user;
        //    List<Mission> missionsList = _unitOfWork.Mission.GetMissionCard().ToList();
        //    ViewBag.Missions = missionsList;
        //    return View();
        //}

        public IActionResult MissionDetail()
        {
            return View();
        }
    }
}
