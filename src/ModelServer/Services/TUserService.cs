using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using NHibernate.Criterion;
using System.Security.Cryptography;

namespace Locadora.Services
{
    public partial class TUserService : EntityService<TUser>, ITUserService
    {
        public void Edit(TUser model)
        {
            var user = TUser.Load(model.Id);
            user.Name = model.Name;
            user.EnumProfileUser = model.EnumProfileUser;
            user.Update();
        }
        public byte[] HashPassword(string password)
        {
            return SHA384.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public TUser FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            return Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", username))
                .SetMaxResults(1)
                .UniqueResult<TUser>();
        }
        public TUser Authenticate(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
                return null;


            var criteria = Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", login.Username));

            var result = criteria.SetMaxResults(1)
                .UniqueResult<TUser>();

            if (result == null || result.Password == null)
                return null;

            var userPassword = result.Password;
            var hashTryPassword = HashPassword(login.Password);
            return userPassword.SequenceEqual(hashTryPassword) ? result : null;
        }

        public String GetErrorLogin(Login login)
        {
            var user = Session.CreateCriteria<TUser>()
                .Add(Restrictions.Eq("Login", login.Username))
                .SetMaxResults(1)
                .UniqueResult<TUser>();

            if (user == null)
            {
                return "Usuário ou Senha não existe!";
            }
            else
            {
                return "Bem-vindo " + login.Username;
            }
        }
    }
}