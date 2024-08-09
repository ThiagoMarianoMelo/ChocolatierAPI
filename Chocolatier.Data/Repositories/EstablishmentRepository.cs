using Chocolatier.Data.Context;
using Chocolatier.Domain.Entities;
using Chocolatier.Domain.Interfaces;
using Chocolatier.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chocolatier.Data.Repositories
{
    public class EstablishmentRepository : BaseRepository<Establishment>, IEstablishmentRepository
    {
        private readonly UserManager<Establishment> UserManager;
        private readonly IAuthEstablishment AuthEstablishment;

        public EstablishmentRepository(ChocolatierContext chocolatierContext, UserManager<Establishment> userManager, IAuthEstablishment authEstablishment) : base(chocolatierContext)
        {
            UserManager = userManager;
            AuthEstablishment = authEstablishment;
        }

        public IQueryable<Establishment> GetQueryableEstablishmentsByFilter(string name, string email)
        {
            var queryCondiction = BuildQueryEstablishmentFilter(name, email);

            return UserManager.Users.AsNoTracking()
                               .Where(queryCondiction)
                               .Select(es => new Establishment() { Id = es.Id, Email = es.Email, UserName = es.UserName, 
                                   EstablishmentType = es.EstablishmentType, Address = es.Address })
                               .OrderBy(usr => usr.UserName);
        }


        private Expression<Func<Establishment, bool>> BuildQueryEstablishmentFilter(string name, string email)
        {

            return est => !est.Id.Equals(AuthEstablishment.Id) &&
                          !est.LockoutEnabled &&
                          (string.IsNullOrWhiteSpace(name) || est.UserName!.Contains(name)) &&
                          (string.IsNullOrWhiteSpace(email) || est.Email!.Contains(email));
        }
    }
}
