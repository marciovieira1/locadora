using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Locadora.Domain
{
    public partial class TReservation
    {
        public virtual List<TMovie> Movie { get; set; }
        public virtual int[] Movies { get; set; }
        public virtual int[] Quantities { get; set; }
    }
}
