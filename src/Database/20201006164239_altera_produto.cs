using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;
using Locadora.Domain;

namespace Locadora.Database
{
    [Migration(20201006164239)]
    public class Migration20201006164239 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.ChangeTable("t_movies", t =>
            {
                t.AddDouble("value");
            });
        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_movies", t =>
            {
                t.RemoveColumn("value");
            });
        }
    }

}