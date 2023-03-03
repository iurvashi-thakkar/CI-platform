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
       
        public ICountryRepository Country{ get; }

        public ICityRepository City{ get; }

        public IMissionThemeRepository MissionTheme{ get; }

        public ISkillRepository Skill{ get; }

        public IMissionRepository Mission{ get; }
        void Save();
    }
}
