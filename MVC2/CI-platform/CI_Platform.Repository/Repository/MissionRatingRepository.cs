using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class MissionRatingRepository:Repository<MissionRating>, IMissionRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public MissionRatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(MissionRating missionRating)
        {
            throw new NotImplementedException();
        }
        public void UpdateRating(MissionRating missionRating, int rating)
        {
            missionRating.Rating = rating;
            missionRating.UpdatedAt = DateTime.Now;
            _context.MissionRatings.Update(missionRating);
        }
    }
}
