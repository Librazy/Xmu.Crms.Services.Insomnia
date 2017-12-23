using System;
using System.Linq;
using Xmu.Crms.Shared.Exceptions;
using Xmu.Crms.Shared.Models;
using Xmu.Crms.Shared.Service;
using static Xmu.Crms.Services.Insomnia.PasswordUtils;


namespace Xmu.Crms.Services.Insomnia
{
    public class LoginService : ILoginService
    {
        private readonly CrmsContext _db;

        public LoginService(CrmsContext db) => _db = db;

        public UserInfo SignInWeChat(long userId, string code, string state, string successUrl) =>
            throw new NotImplementedException();

        public UserInfo SignInPhone(UserInfo user)
        {
            var userInfo = _db.UserInfo.SingleOrDefault(u => u.Phone == user.Phone) ??
                           throw new UserNotFoundException();
            if (IsExpectedPassword(user.Password, ReadHashString(userInfo.Password)))
            {
                return userInfo;
            }

            throw new PasswordErrorException();
        }

        public UserInfo SignUpPhone(UserInfo user)
        {
            user.Password = HashString(user.Password);
            if (_db.UserInfo.Any(u => u.Phone == user.Phone))
            {
                throw new PhoneAlreadyExistsException();
            }

            var entry = _db.UserInfo.Add(user);
            _db.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteTeacherAccount(long userId) => throw new NotImplementedException();

        public bool DeleteStudentAccount(long userId) => throw new NotImplementedException();
    }
}