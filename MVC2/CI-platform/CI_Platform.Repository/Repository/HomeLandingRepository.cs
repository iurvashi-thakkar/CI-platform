using CI_Platform.Entity.DataModels;
using CI_Platform.Entity.DataModels.ViewModel;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class HomeLandingRepository:IHomeLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //public HomeLandingPageVM GetLandingPageData(string sort, string email,int currentPage)
        //{
        //    HomeLandingPageVM landingPageVM = new();

        //    landingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
        //    landingPageVM.Countries = _unitOfWork.Country.GetAll();
        //    //landingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);

        //    IEnumerable<Mission> missionsList;
        //    landingPageVM.Cities = _unitOfWork.City.GetAll();
        //    //missionsList = _unitOfWork.Mission.GetMisssionCard();

        //    //landingPageVM.Cities = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined");
        //    missionsList = _unitOfWork.Mission.GetMissionCard();


        //    if (sort == "Newest")
        //    {
        //        landingPageVM.Missions = missionsList.OrderByDescending(m => m.StartDate);
        //    }
        //    else if (sort == "Oldest")
        //    {
        //        landingPageVM.Missions = missionsList.OrderBy(m => m.StartDate);
        //    }
        //    else if (sort == "Lowest_seats")
        //    {
        //        landingPageVM.Missions = missionsList.OrderBy(m => m.CreatedAt);
        //    }
        //    else if (sort == "Highest_seats")
        //    {
        //        landingPageVM.Missions = missionsList.OrderBy(m => m.CreatedAt);
        //    }
        //    else if (sort == "Favourite")
        //    {
        //        landingPageVM.Missions = missionsList.OrderBy(m => m.CreatedAt);
        //    }
        //    else if (sort == "Deadline")
        //    {
        //        landingPageVM.Missions = missionsList.OrderBy(m => ((m.StartDate) - TimeSpan.FromDays(1)));
        //    }
        //    else
        //    {
        //        landingPageVM.Missions = missionsList.OrderBy(m => m.Title); ;
        //    }

        //    int totalrecords=missionsList.Count();
        //    int pageSize = 4;
        //    missionsList = missionsList.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
        //    int totalPages = (int)Math.Ceiling(totalrecords /(double) pageSize);

        //    landingPageVM.Missions = missionsList;
        //    landingPageVM.CurrentPage = currentPage;
        //    landingPageVM.TotalPages = totalPages;
        //    landingPageVM.sort= sort;
        //    landingPageVM.PageSize = pageSize;
        //    landingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
        //    landingPageVM.Skills = _unitOfWork.Skill.GetAll();
        //    return landingPageVM;
        //}

        //public HomeLandingPageVM GetMissionPageData(string sort, string email)
        //{

        //}

        public HomeLandingPageVM GetLandingPageData(string email, int currentPage)
        {
            HomeLandingPageVM landingPageVM = new();

            landingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);
            landingPageVM.Countries = _unitOfWork.Country.GetAll();
            //landingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);
           
            IEnumerable<Mission> missionsList;
            landingPageVM.Cities = _unitOfWork.City.GetAll();
            //missionsList = _unitOfWork.Mission.GetMisssionCard();

            //landingPageVM.Cities = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined");
            missionsList = _unitOfWork.Mission.GetMissionCard();


        
            
            int totalrecords = missionsList.Count();
            int pageSize = 9;
            missionsList = missionsList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            int totalPages = (int)Math.Ceiling(totalrecords / (double)pageSize);

            landingPageVM.Missions = missionsList;
            landingPageVM.CurrentPage = currentPage;
            landingPageVM.TotalPages = totalPages;
            //landingPageVM.sort = sort;
            landingPageVM.PageSize = pageSize;
            landingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            return landingPageVM;
        }
    }
}
