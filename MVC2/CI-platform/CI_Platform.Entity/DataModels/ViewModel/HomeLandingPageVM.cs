using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Entity.DataModels.ViewModel
{
   public class HomeLandingPageVM
    {
        public IEnumerable<Mission> Missions { get; set; }

        public IEnumerable<City> Cities { get; set; }

        public IEnumerable<Country> Countries { get; set; }   

        public IEnumerable<MissionTheme> Themes { get; set; }

        //public IEnumerable<MissionApplication> MissionApplications { get; set; }

        public IEnumerable<User> UserList { get; set; } 
        public IEnumerable<Skill> Skills { get; set; }

        //public IEnumerable<MissionSkills> MissionSkills { get; set; }
        //public string SelectedCountry { get; set; }
        public User LoggedUser { get; set; }

        public Mission AppliedMission { get; set; }


        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string sort { get; set; }

    }
}
