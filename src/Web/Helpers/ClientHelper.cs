using Locadora.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Locadora.Web.Helpers
{
    public static class ClientHelper
    {
        public static Subdomain GetSubdomain(this string url)
        {
            if (url.Contains("Admin"))
                return Subdomain.Admin;
            else
                return Subdomain.Clientes;
        }
    }
}