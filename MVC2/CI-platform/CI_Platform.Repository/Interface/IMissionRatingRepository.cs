using CI_Platform.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
   public interface IMissionRatingRepository:IRepository<MissionRating>
    {
        void Update(MissionRating missionRating);
        void UpdateRating(MissionRating missionRating, int rating);
    }
}
