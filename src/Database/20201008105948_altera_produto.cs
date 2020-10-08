using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201008105948)]
    public class Migration20201008105948 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.ChangeTable("t_movies", t =>
            {
                t.ChangeColumn("value", DbType.Decimal);
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_movies", t =>
            {
                t.ChangeColumn("value", DbType.Double);
            });

        }
    }

}