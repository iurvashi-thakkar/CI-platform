using CI_Platform.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IMissionRepository:IRepository<Mission>
    {
        public IEnumerable<Mission> GetMissionCard();
        public IEnumerable<Mission> GetMissionCardById(long id);
        //public IEnumerable<Mission> GetMissionSortNew();
    }
}
