using CI_Platform.Entity.DataModels;
using CI_Platform.Entity.DataModels.ViewModel;
using CI_Platform.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CI_platform.Controllers
{
    public class MissionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IHomeLandingRepository _HomeLandingRepository;
        private readonly IMissionLandingRepository _MissionLandingRepository;
        //private readonly ApplicationDbContext _context;
        public MissionController(ApplicationDbContext _context, ILogger<HomeController> logger, IUnitOfWork unitOfWork, IHomeLandingRepository HomeLandingRepository, IMissionLandingRepository MissionLandingRepository)
        {
            _unitOfWork = unitOfWork;

            this._logger = logger;
            _HomeLandingRepository = HomeLandingRepository;
            _MissionLandingRepository = MissionLandingRepository;
            //_context= _context;
            //_emailService = emailService;

        }
        public IActionResult GetCitiesByCountry(long countryId)
        {
            var cities = _unitOfWork.City.GetAll().Where(c => c.CountryId == countryId).ToList();
            return Json(cities);
        }

        public IActionResult HomePage(string sort = "", int currentPage = 1)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            //var user = _unitOfWork.User.GetFirstOrDefault(u => u.Email == sessionValue);
            //ViewBag.User = user;
            //List<Country> countryList = _unitOfWork.Country.GetAll().ToList();
            //ViewBag.Countries = countryList;
            //if (id != 0)
            //{
            //    List<City> cityList = _unitOfWork.City.GetAll().Where(c => c.CountryId == id).ToList();
            //    //List<Mission> missionsList1 = _unitOfWork.Mission.GetMissionCard().Where(c=>c.CountryId==id).ToList();
            //    ViewBag.Cities = cityList;

            //    List<Mission> missionsList = _unitOfWork.Mission.GetMissionCard().Where(c => c.CountryId == id).ToList();
            //    ViewBag.Missions = missionsList;
            //    if (sort == "")
            //    {

            //        ViewBag.Missions = missionsList;
            //    }
            //    else if (sort == "newest")
            //    {
            //        ViewBag.Missions = missionsList.OrderByDescending(m => m.StartDate).ToList();
            //    }
            //    else if (sort == "oldest")
            //    {
            //        ViewBag.Missions = missionsList.OrderBy(m => m.StartDate).ToList();
            //    }
            //    //ViewBag.Mission = missionsList1;
            //}
            //else
            //{
            //    List<City> cityList = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined").ToList();
            //    ViewBag.Cities = cityList;
            //}
            //List<MissionTheme> themeList = _unitOfWork.MissionTheme.GetAll().ToList();
            //ViewBag.Themes = themeList;

            //List<Skill> skillList = _unitOfWork.Skill.GetAll().ToList();
            //ViewBag.Skills = skillList;

            //if (id == 0)
            //{
            //    List<Mission> missionsList= _unitOfWork.Mission.GetMissionCard().ToList();
            //    if (sort == "")
            //    {

            //        ViewBag.Missions = missionsList;
            //    }
            //    else if (sort == "newest")
            //    {
            //        ViewBag.Missions = missionsList.OrderByDescending(m => m.StartDate).ToList();
            //    }
            //    else if (sort == "oldest")
            //    {
            //        ViewBag.Missions = missionsList.OrderBy(m => m.StartDate).ToList();
            //    }
            //}

            //HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sort, sessionValue,currentPage);
            HomeLandingPageVM landingPageData = _HomeLandingRepository.GetLandingPageData(sessionValue, currentPage);
            return View(landingPageData);


        }
        //public IActionResult AddToFavourites(int missionId)
        //{

        //    ApplicationDbContext _db = new ApplicationDbContext();
        //    var userId = HttpContext.Session.GetString("UserId");
        //    long UserId = Convert.ToInt64(userId);
        //    var alredyFavourite = _db.FavouriteMissions.SingleOrDefault(m => m.MissionId == missionId && m.UserId == UserId);

        //    if (alredyFavourite == null)
        //    {
        //        var newFavourite = new FavouriteMission
        //        {
        //            UserId = UserId,
        //            MissionId = missionId
        //        };

        //        _db.FavouriteMissions.Add(newFavourite);
        //        _db.SaveChanges();
        //    }
        //    else
        //    {
        //        _db.FavouriteMissions.Remove(alredyFavourite);
        //        _db.SaveChanges();
        //    }

        //    return RedirectToAction(nameof(HomePage));
        //}
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
        //[HttpPost]
        //public IActionResult AddToFavorites(int missionId)
        //{
        //    ApplicationDbContext _db = new ApplicationDbContext();
        //    string Id = HttpContext.Session.GetString("UserId");
        //    long userId = long.Parse(Id);

        //    Check if the mission is already in favorites for the user
        //    if (_db.FavoriteMissions.Any(fm => fm.MissionId == missionId && fm.UserId == userId))
        //        {
        //            Mission is already in favorites, return an error message or redirect back to the mission page
        //           var FavoriteMissionId = _context.FavoriteMissions.Where(fm => fm.MissionId == missionId && fm.UserId == userId).FirstOrDefault();
        //            _context.FavoriteMissions.Remove(FavoriteMissionId);
        //            _context.SaveChanges();
        //            return Ok();

        //            return BadRequest("Mission is already in favorites.");
        //        }

        //    Add the mission to favorites for the user

        //   var favoriteMission = new FavoriteMission { MissionId = missionId, UserId = userId };
        //    _context.FavoriteMissions.Add(favoriteMission);
        //    _context.SaveChanges();

        //    return Ok();
        //}



        public IActionResult MissionDetail(long missionId)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }

            HomeLandingPageVM MissionlandingPageData = _MissionLandingRepository.GetMissionPageData(sessionValue,missionId);
            return View(MissionlandingPageData);
        }

        [HttpPost]
        public IActionResult AddRating(long missionId, long userId, int rating)
        {
            var mission_user_rating = _unitOfWork.MissionRating.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_rating == null)
            {
                _unitOfWork.MissionRating.Add(new MissionRating
                {
                    MissionId = missionId,
                    UserId = userId,
                    Rating = rating,

                });
            }
            else
            {
                _unitOfWork.MissionRating.UpdateRating(mission_user_rating, rating);
            }
            _unitOfWork.Save();
            TempData["success"] = "Rating Successfull";
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("MissionDetail","Mission",new {missionId});
        }


        [HttpPost]
        public IActionResult AddComment(long missionId, long userId,string comment_text)
        {
            var sessionValue = HttpContext.Session.GetString("UserEmail");
            if (String.IsNullOrEmpty(sessionValue))
            {
                TempData["error"] = "Session Expired!\nPlease Login Again!";
                return RedirectToAction("Index");
            }
            var mission_user_comment = _unitOfWork.MissionComment.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_comment == null)
            {
                _unitOfWork.MissionComment.Add(new Comment
                {
                    MissionId = missionId,
                    UserId = userId,
                    CommentText= comment_text,
                });
            }
            //else
            //{
            //    _unitOfWork.MissionRating.UpdateC(mission_user_comment, comment_text);
            //}
            _unitOfWork.Save();
            TempData["success"] = "Comment Added!Thank You For Your Comment";

            return RedirectToAction("MissionDetail", "Mission", new { missionId });
        }
        [HttpPost]
        public IActionResult AddToFavorites(long missionId, long userId)
        {
            //string Id = HttpContext.Session.GetString("UserId");
            //long userId = long.Parse(Id);
            //var sessionValue = HttpContext.Session.GetString("UserEmail");

            // Check if the mission is already in favorites for the user
            var mission_user_favourite = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_favourite!=null)
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var FavoriteMissionId = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
                _unitOfWork.FavoriteMission.Remove(FavoriteMissionId);
                _unitOfWork.Save();
                return Ok();
              

                //return BadRequest("Mission is already in favorites.");
            }

            // Add the mission to favorites for the user
            _unitOfWork.FavoriteMission.Add(new FavouriteMission
            {
                MissionId = missionId,
                UserId = userId,
            });
            TempData["success"] = "Added To Favourite Mission";
            _unitOfWork.Save();
            return Ok();
        }

        [HttpPost]
        public IActionResult AddToFavorite(long missionId, long userId)
        {
            //string Id = HttpContext.Session.GetString("UserId");
            //long userId = long.Parse(Id);
            //var sessionValue = HttpContext.Session.GetString("UserEmail");

            // Check if the mission is already in favorites for the user
            var mission_user_favourite = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
            if (mission_user_favourite != null)
            {
                // Mission is already in favorites, return an error message or redirect back to the mission page
                var FavoriteMissionId = _unitOfWork.FavoriteMission.GetFirstOrDefault(u => (u.UserId == userId) && (u.MissionId == missionId));
                _unitOfWork.FavoriteMission.Remove(FavoriteMissionId);
                _unitOfWork.Save();
                return RedirectToAction("MissionDetail", "Mission", new { missionId });


                //return BadRequest("Mission is already in favorites.");
            }

            // Add the mission to favorites for the user
            _unitOfWork.FavoriteMission.Add(new FavouriteMission
            {
                MissionId = missionId,
                UserId = userId,
            });
            TempData["success"] = "Added To Favourite Mission";
            _unitOfWork.Save();
            return RedirectToAction("MissionDetail", "Mission", new { missionId });
        }
    }
}
