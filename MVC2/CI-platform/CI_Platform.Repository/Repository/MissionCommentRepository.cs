using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class MissionCommentRepository:Repository<Comment>,IMissionCommentRepository
    {
        private readonly ApplicationDbContext _context;
        public MissionCommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
