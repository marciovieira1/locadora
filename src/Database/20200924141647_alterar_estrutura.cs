using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20200924141647)]
    public class Migration20200924141647 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.ChangeTable("t_preferences", t =>
            {
                t.RemoveColumn("enum_type_movie");
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_preferences", t =>
            {
                t.AddInt32("enum_type_movie");
            });
        }
    }

}