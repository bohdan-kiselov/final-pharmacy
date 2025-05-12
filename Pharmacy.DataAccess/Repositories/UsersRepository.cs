using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Models;
using Pharmacy.DataAccess.Entities;

namespace Pharmacy.DataAccess.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PharmacyDBContext _context;

        public UsersRepository(PharmacyDBContext context)
        {
            _context = context;
        }

        public async Task<bool> FindLogin(string login)
        {
            return await _context.Users.AnyAsync(u => u.Login == login);
        }

        public async Task<bool> FindEmail(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetUser(int userId)
        {
            var entity = await _context.Users
                .FindAsync(userId);

            return entity == null ? null : MapToModel(entity);
        }

        public async Task<User?> GetUserByLogin(string login)
        {
            var entity = await _context.Users
                .FirstOrDefaultAsync(u => u.Login == login);

            return entity == null ? null : MapToModel(entity);
        }

        public async Task<User> Create(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var userEntity = new UserEntity
            {
                Login = user.Login,
                Email = user.Email,
                HashPassword = user.Password,
                PhoneNumber = user.PhoneNumber,
                RoleID = user.RoleID ?? 1
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            user.Id = userEntity.Id;

            return user;
        }

        private User MapToModel(UserEntity entity)
        {
            return new User(entity.Id, entity.Login, entity.Email, entity.HashPassword, entity.PhoneNumber, entity.RoleID);
            
        }

        public async Task<bool> Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingEntity = await _context.Users.FindAsync(user.Id);

            if (existingEntity == null)
                return false;

            existingEntity.Login = user.Login;
            existingEntity.Email = user.Email;
            existingEntity.HashPassword = user.Password;
            existingEntity.PhoneNumber = user.PhoneNumber;
            // IsVerified не оновлюється це окрема логіка підтвердження нової пошти, можливо треба підняти це питання у наступних мітах

            existingEntity.RoleID = user.RoleID ?? existingEntity.RoleID; 

            _context.Users.Update(existingEntity);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

    }
}
