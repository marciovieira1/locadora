using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;
using Locadora.Domain;

namespace Locadora.Services
{
    public partial class TItenService : EntityService<TIten>, ITItenService
    {
        public void SaveMovies(TReservation model)
        {
            TIten.Delete(x => x.Reservation.Id == model.Id);

            if (model.Movies != null)
            {
                for (int i = 0; i < model.Movies.Length; i++)
                {
                    var movie = TMovie.Load(model.Movies[i]);
                    new TIten()
                    {
                        Reservation = model,
                        Movie = movie,
                        Value = movie.Value.Value,
                        Quantity = model.Quantities[i]
                    }.Save();
                }
            }
        }
    }
}