using Simple.Entities;
using Locadora.Domain;
using Simple.Services;
using Locadora.Services;
using System;

namespace Locadora.Domain
{
    public partial class TPreference
    {
        public static void SavePreference(TClient model) 
        {
			Service.SavePreference(model);
		}

    }
}