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


        public HomeLandingPageVM GetLandingPageData(string sort, string email)
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
            


            if (sort == "Newest")
            {
                landingPageVM.Missions = missionsList.OrderByDescending(m => m.StartDate);
            }
            else if (sort == "Oldest")
            {
                landingPageVM.Missions = missionsList.OrderBy(m => m.StartDate);
            }
            else if (sort == "Lowest_seats")
            {
                landingPageVM.Missions = missionsList.OrderBy(m => m.CreatedAt);
            }
            else if (sort == "Highest_seats")
            {
                landingPageVM.Missions = missionsList.OrderBy(m => m.CreatedAt);
            }
            else if (sort == "Favourite")
            {
                landingPageVM.Missions = missionsList.OrderBy(m => m.CreatedAt);
            }
            else if (sort == "Deadline")
            {
                landingPageVM.Missions = missionsList.OrderBy(m => ((m.StartDate) - TimeSpan.FromDays(1)));
            }
            else
            {
                landingPageVM.Missions = missionsList.OrderBy(m => m.Title); ;
            }


            landingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            return landingPageVM;
        }
    }
}
