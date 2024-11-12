using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.EntityFrameworkCore;
using Domain.Models.User;

namespace InfraStructure.Validator
{
    public class UserValidator : IUserValidator<TenderUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TenderUser> manager, TenderUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var errors = new List<IdentityError>();

            var existingUsers = await manager.Users
                .Where(u => (u.UserName == user.UserName || u.Email == user.Email || u.PhoneNumber == user.PhoneNumber) && u.Id != user.Id)
                .ToListAsync();

            foreach (var existingUser in existingUsers)
            {
                if (existingUser.UserName == user.UserName)
                {
                    errors.Add(new IdentityError
                    {
                        Code = "DuplicateUserName",
                        Description = "نام کاربری تکراری است",
                    });
                }

                if (existingUser.Email == user.Email)
                {
                    errors.Add(new IdentityError
                    {
                        Code = "DuplicateEmail",
                        Description = "ایمیل مورد نظر تکراری است"
                    });
                }

                if (existingUser.PhoneNumber == user.PhoneNumber)
                {
                    errors.Add(new IdentityError
                    {
                        Code = "DuplicatePhoneNumber",
                        Description = "شماره تماس تکراری است"
                    });
                }
            }

            if (errors.Count > 0)
            {
                return IdentityResult.Failed(errors.ToArray());
            }

            return IdentityResult.Success;
        }
    }
}
