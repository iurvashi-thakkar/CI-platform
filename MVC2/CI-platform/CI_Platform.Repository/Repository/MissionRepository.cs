using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class MissionRepository:Repository<Mission>,IMissionRepository
    {
        private readonly ApplicationDbContext _context;
        public MissionRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public IEnumerable<Mission> GetMissionCard()
        {
            var missions = _context.Missions.Include(m => m.City).Include(m => m.Theme)
                .Include(m=>m.GoalMissions).Include(m=>m.MissionApplications)
                .Include(m=>m.MissionRatings).Include(m=>m.MissionMedia).Include(m=>m.MissionSkills).
                Include(m=>m.FavouriteMissions);
            return missions;
        }
        public IEnumerable<Mission> GetMissionCardById(long id)
        {
            var missionBYId = _context.Missions.Include(m => m.City).Include(m => m.Theme)
                .Include(m => m.GoalMissions).Include(m => m.MissionApplications)
                .Include(m => m.MissionRatings).Include(m => m.MissionMedia).Include(m => m.MissionSkills).
                Include(m => m.FavouriteMissions).Include(m => m.Comments).Where(m=>m.MissionId==id);
           
            return missionBYId;
        }

        //    public IEnumerable<Mission> GetMissionSortNew()
        //    {
        //        var missionSortNew=_context.Missions.OrderBy<Mission, CreatedAt>
        //    }
    }
}
