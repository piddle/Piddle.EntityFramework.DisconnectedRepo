using Microsoft.VisualStudio.TestTools.UnitTesting;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.NormalContext;
using Piddle.EntityFramework.DisconnectedRepo.TestingArea.Utils;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Piddle.EntityFramework.DisconnectedRepo.TestingArea.Tests
{
    public abstract class DbContextTestsBase
    {
        protected NormalContextTestValues Values { get; set; }
        protected EntityMapper EntityMapper { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            CleanDownSchemaAndRecreate();

            Values = new NormalContextTestValues();
            EntityMapper = new EntityMapper();

            // Insert Bill and children

            using (var context = new NormalDbContext())
            {
                context.Bills.Add(Values.Bill);
                var rowsAffected = context.SaveChanges();

                Values.AssertCreated(rowsAffected);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {

        }

        private static void CleanDownSchemaAndRecreate()
        {
            var script = File.ReadAllText("Migrations\\CleanDownSchemaAndRecreate.sql");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
