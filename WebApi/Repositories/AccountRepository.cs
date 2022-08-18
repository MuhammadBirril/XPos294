using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using WebApi.DataModels;

namespace WebApi.Repositories
{
    public class AccountRepository
    {
        private XposDbContext _XPosDbContext = new XposDbContext();

        public AccountViewModel Authentication(UserLogin userLogin)
        {
            AccountViewModel result = new AccountViewModel();
            result = _XPosDbContext.Accounts
                .Where(o => o.UserName == userLogin.UserName && o.Password == userLogin.Password)
                .Select(o => new AccountViewModel
                {
                    //Password JANGAN dimunculkan!
                    Id = o.Id,
                    UserName = o.UserName,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Active = o.Active
                }).FirstOrDefault();
            if (result != null)
            {
                result.Role = userLogin.UserName == "admin" ? new List<string> { "Administrator" } : Authorization(userLogin.UserName);
            }
            return result;
        }

        public List<string> Authorization(string userName)
        {
            List<string> result = new List<string>();
            var list = (from us in _XPosDbContext.Accounts
                        join ur in _XPosDbContext.UserRoles
                        on us.UserName equals ur.UserName into JoinUserRole

                        from usUr in JoinUserRole.DefaultIfEmpty()
                        join gj in _XPosDbContext.GroupJobs
                        on usUr.GroupJobId equals gj.Id into JoinUserRoleGroup

                        from urg in JoinUserRoleGroup.DefaultIfEmpty()
                        join tr in _XPosDbContext.TransRoles
                        on urg.Id equals tr.GroupJobId into JoinURGTrans

                        from urgt in JoinURGTrans.DefaultIfEmpty()
                        where us.UserName == userName
                        select new { urgt.Role }
                        ).ToList();
            //_XPosDbContext.UserRoles
            //.Where(o => o.UserName == userName)
            //.ToList();

            foreach (var item in list)
            {
                result.Add(item.Role);
            }
            return result;
        }
    }
}

