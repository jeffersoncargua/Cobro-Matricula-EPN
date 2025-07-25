﻿using Cobro_Matricula_EPN.Context;
using CobroMatriculaEPN.SharedDatabaseSetup;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CobroMatriculaEPN.IntegratedTest
{
    public class SharedDatabaseFixture : IDisposable
    {

        private static readonly object _lock = new object();

        private bool _databaseInitialized;

        private readonly string dbName = "IntegratedTest.db";

        public SharedDatabaseFixture()
        {

            Connection = new SqliteConnection($"Filename={dbName}");
            Seed();
            Connection.Open();
        }

        public DbConnection Connection { get; }

        public ApplicationDbContext CreateContext(DbTransaction? transaction=null)
        {
            var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using(var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();
                        DatabaseSetup.SeedData(context);
                    }

                    _databaseInitialized = true;
                }
            }
        }
        public void Dispose() => Connection.Dispose();

    }
}
