using CI_Platform.Entity.DataModels.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IMissionLandingRepository
    {
        HomeLandingPageVM GetMissionPageData(string email,long missionId);
    }
}
