using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassword(User user, string newPassword)
        {
            user.Password = newPassword;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Update(user);
        }
    }
}
