using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IUnitOfWork
    {
        public IUserRepository User{ get; }
        public IPasswordResetRepository PasswordReset{ get; }
       

        void Save();
    }
}
