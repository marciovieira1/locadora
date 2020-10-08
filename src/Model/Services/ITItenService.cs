using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITItenService : IEntityService<TIten>, IService
    {
        void SaveMovies(TReservation model);
    }
}