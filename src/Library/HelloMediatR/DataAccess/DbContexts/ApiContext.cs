using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                var user1 = new UserEntity()
                {
                    Id = "fake-id-1",
                    Name = "Admin1",
                    Salt = new DateTime(2024, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc).Ticks.ToString(),
                };
                user1.HashedPassword = Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes($"{user1.Id}{user1.Salt}{"fake_pwd_1"}")));
                Users.Add(user1);
            }

            {
                var user2 = new UserEntity()
                {
                    Id = "fake-id-2",
                    Name = "Admin2",
                    Salt = new DateTime(2024, 2, 2, 2, 2, 2, 2, DateTimeKind.Utc).Ticks.ToString(),
                };
                user2.HashedPassword = Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes($"{user2.Id}{user2.Salt}{"fake_pwd_2"}")));
                Users.Add(user2);
            }
        }

        public async Task<UserEntity> GetUserByName(string name)
        {
            await Task.CompletedTask;
            return Users.Local.FirstOrDefault(o => o.Name == name);
        }

        public async Task<UserEntity> ValidateUser(string name, string rawPassword)
        {
            if (string.IsNullOrEmpty(rawPassword))
                return default;

            UserEntity user = await GetUserByName(name);
            if (user is null)
                return default;

            return user.HashedPassword == Encoding.UTF8.GetString(SHA256.HashData(Encoding.UTF8.GetBytes($"{user.Id}{user.Salt}{rawPassword}")))
                ? user : null;
        }

        public async Task<List<UserEntity>> GetUsers()
        {
            await Task.CompletedTask;
            return Users.Local.ToList();
        }
    }
}
