using CI_Platform.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
   
        public interface IUserRepository : IRepository<User>
        {
            void Update(User user);
            void UpdatePassword(User user, string newPassword);
        }
    
}
