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
    public class MissionLandingRepository:IMissionLandingRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public MissionLandingRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public HomeLandingPageVM GetMissionPageData(string email,long missionId)
        {
            HomeLandingPageVM landingPageVM = new();

            landingPageVM.LoggedUser = _unitOfWork.User.GetFirstOrDefault(u => u.Email == email);

            landingPageVM.AppliedMission= _unitOfWork.Mission.GetFirstOrDefault(u=>u.MissionId== missionId);

            //landingPageVM.Countries = _unitOfWork.Country.GetAll();
            landingPageVM.UserList = _unitOfWork.User.GetAll().Where(u => u.Email != email);

            IEnumerable<Mission> missionsList;
            //landingPageVM.Cities = _unitOfWork.City.GetAll();


            //landingPageVM.Cities = _unitOfWork.City.GetAll().Where(c => c.Name != "Undefined");
            missionsList = _unitOfWork.Mission.GetMissionCardById(missionId);

            landingPageVM.Missions = missionsList;



            //int totalrecords = missionsList.Count();
            //int pageSize = 9;
            //missionsList = missionsList.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            //int totalPages = (int)Math.Ceiling(totalrecords / (double)pageSize);

            //landingPageVM.Missions = missionsList;
            //landingPageVM.CurrentPage = currentPage;
            //landingPageVM.TotalPages = totalPages;
            ////landingPageVM.sort = sort;
            //landingPageVM.PageSize = pageSize;
            //landingPageVM.Themes = _unitOfWork.MissionTheme.GetAll();
            //landingPageVM.Skills = _unitOfWork.Skill.GetAll();
            return landingPageVM;
        }
    }
}
