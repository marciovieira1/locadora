using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Services
{
    public partial interface ITPreferenceService : IEntityService<TPreference>, IService
    {
        void SavePreference(TClient model);
    }
}