using System.ComponentModel;


namespace Locadora.Domain
{
    public enum Subdomain
    {
        [Description("Clientes")]
        Clientes = 0,
        [Description("Administrador")]
        Admin
    }
}
