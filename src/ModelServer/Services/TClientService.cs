
using System.Text;
using Simple.Entities;
using Locadora.Domain;
using System.Security.Cryptography;
using System.Linq;
using NHibernate.Criterion;
using System;

namespace Locadora.Services
{
    public partial class TClientService : EntityService<TClient>, ITClientService
    {
        public byte[] HashPassword(string password)
        {
            return SHA384.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public void Edit(TClient model)
        {
            var client = TClient.Load(model.Id);
            client.Name = model.Name;
            client.Email = model.Email;
            client.Telephone = model.Telephone;
            client.Login = model.Login;
            client.EnumProfileClient = model.EnumProfileClient;
            client.Update();
        }
        public bool CheckPassword(TClient client, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;
            var hashTryPassword = HashPassword(password);
            return client.Password.SequenceEqual(hashTryPassword);
        }

 
        public TClient FindByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;

            return Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", username))
                .SetMaxResults(1)
                .UniqueResult<TClient>();
        }
        public TClient Authenticate(Login login)
        {
            if (string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
                return null;


            var criteria = Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", login.Username));

            var result = criteria.SetMaxResults(1)
                .UniqueResult<TClient>();

            if (result == null || result.Password == null)
                return null;

            var userPassword = result.Password;
            var hashTryPassword = HashPassword(login.Password);
            return userPassword.SequenceEqual(hashTryPassword) ? result : null;
        }

        public String GetErrorLogin(Login login)
        {
            var client = Session.CreateCriteria<TClient>()
                .Add(Restrictions.Eq("Login", login.Username))
                .SetMaxResults(1)
                .UniqueResult<TClient>();

            if (client == null)
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