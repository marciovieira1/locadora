using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITUserService : IEntityService<TUser>, IService
    {
        void Edit(TUser model);
        Byte[] HashPassword(String password);
        TUser FindByUsername(String username);
        TUser Authenticate(Login login);
        String GetErrorLogin(Login login);
    }
}