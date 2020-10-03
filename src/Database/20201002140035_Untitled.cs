using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201002140035)]
    public class Migration20201002140035 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.ChangeTable("t_users", t =>
            {
                t.AddString("login");
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_users", t =>
            {
                t.RemoveColumn("login");
            });
        }
    }

}