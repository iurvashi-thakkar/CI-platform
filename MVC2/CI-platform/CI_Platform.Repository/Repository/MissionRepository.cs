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
            var missions = _context.Missions.Include(m => m.City).Include(m => m.Theme);
            return missions;
        }
    //    public IEnumerable<Mission> GetMissionSortNew()
    //    {
    //        var missionSortNew=_context.Missions.OrderBy<Mission, CreatedAt>
    //    }
    }
}
