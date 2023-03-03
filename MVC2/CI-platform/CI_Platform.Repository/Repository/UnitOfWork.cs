using CI_Platform.Entity.DataModels;
using CI_Platform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public IPasswordResetRepository PasswordReset { get; private set; }

        public ICountryRepository Country { get; private set; }
        public ICityRepository City { get; private set; }

        public IMissionThemeRepository MissionTheme { get; private set; }

        public ISkillRepository Skill { get; private set; }

        public IMissionRepository Mission { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            PasswordReset = new PasswordResetRepository(_db);
            Country = new CountryRepository(_db);
            City = new CityRepository(_db);
            MissionTheme = new MissionThemeRepository(_db);
            Skill = new SkillRepository(_db);
            Mission = new MissionRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
