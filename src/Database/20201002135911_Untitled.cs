using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201002135911)]
    public class Migration20201002135911 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            //schema.AddTable("books", t =>
            //{
            //    t.AddString("name");
            //    t.AddInt32("year");
            //});
            
        }

        public override void Down(SchemaAction schema)
        {
            //schema.RemoveTable("books");
        }
    }

}