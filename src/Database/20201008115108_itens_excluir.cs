using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201008115108)]
    public class Migration20201008115108 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.ChangeTable("t_itens", t =>
            {
                t.RemoveColumn("value");
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_itens", t =>
            {
                t.AddDecimal("value");
            });
        }
    }

}