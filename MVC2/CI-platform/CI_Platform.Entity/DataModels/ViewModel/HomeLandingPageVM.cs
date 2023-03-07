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

        public IEnumerable<Skill> Skills { get; set; }
        //public string SelectedCountry { get; set; }
        public User LoggedUser { get; set; }

    }
}
