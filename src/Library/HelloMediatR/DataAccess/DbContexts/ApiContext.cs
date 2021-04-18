using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hello.MediatR.Domain.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hello.MediatR.Domain.DataAccess.DbContexts
{
    public class ApiContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public ApiContext(DbContextOptions options) : base(options)
        {
            LoadFakeUsers();
        }

        public void LoadFakeUsers()
        {
            {
                var user = new UserEntity() { Id = "fake-id-1", Name = "Admin1" };
                Users.Add(user);
            }

            {
                var user = new UserEntity() { Id = "fake-id-2", Name = "Admin2" };
                Users.Add(user);
            }
        }

        public async Task<UserEntity> GetUserByName(string name)
        {
            await Task.CompletedTask;
            return Users.Local.FirstOrDefault(o => o.Name == name);
        }

        public async Task<List<UserEntity>> GetUsers()
        {
            await Task.CompletedTask;
            return Users.Local.ToList();
        }
    }
}
