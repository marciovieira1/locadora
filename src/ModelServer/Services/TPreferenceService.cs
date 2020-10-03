using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;

namespace Locadora.Services
{
    public partial class TPreferenceService : EntityService<TPreference>, ITPreferenceService
    {
        public void SavePreference(TClient model)
        {
            TPreference.Delete(x => x.Client.Id == model.Id);

            if (model.Categories != null)
            {
                for (int i = 0; i < model.Categories.Length; i++)
                {
                    new TPreference()
                    {
                        Client = model,
                        Category = TCategory.Load(model.Categories[i])
                    }.Save();
                }
            }
        }
    }
}