using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Domain
{
    public partial class TUser
    {
        public virtual void Edit() 
        {
			Service.Edit(this);
		}

        public static Byte[] HashPassword(String password) 
        {
			return Service.HashPassword(password);
		}

        public static TUser FindByUsername(String username) 
        {
			return Service.FindByUsername(username);
		}

        public static TUser Authenticate(Login login) 
        {
			return Service.Authenticate(login);
		}

        public static String GetErrorLogin(Login login) 
        {
			return Service.GetErrorLogin(login);
		}

    }
}