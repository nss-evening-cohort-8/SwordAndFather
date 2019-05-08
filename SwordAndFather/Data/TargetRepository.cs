using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Options;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public interface ITargetRepository
    {
        Target AddTarget(string name, string location, FitnessLevel fitnessLevel, int userId);
        IEnumerable<Target> GetAll();
    }

    public class StubTargetRepository : ITargetRepository
    {
        static readonly List<Target> Targets = new List<Target>();

        public Target AddTarget(string name, string location, FitnessLevel fitnessLevel, int userId)
        {
            var target = new Target() { Name = name};
            Targets.Add(target);
            return target;
        }

        public IEnumerable<Target> GetAll()
        {
            return Targets;
        }
    }

    public class TargetRepository : ITargetRepository
    {
        readonly string _connectionString;

        public TargetRepository(IOptions<DbConfiguration> dbConfig)
        {
            _connectionString = dbConfig.Value.ConnectionString;
        }

        public Target AddTarget(string name, string location, FitnessLevel fitnessLevel, int userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var insertQuery = @"
                    INSERT INTO [dbo].[Targets]
                               ([Location]
                               ,[Name]
                               ,[FitnessLevel]
                               ,[UserId])
                    output inserted.*
                         VALUES
                               (@location
                               ,@name
                               ,@fitnessLevel
                               ,@userId)";

                var parameters = new
                {
                    Name = name, 
                    Location = location, 
                    FitnessLevel= fitnessLevel, 
                    UserId = userId
                };

                var newTarget = db.QueryFirstOrDefault<Target>(insertQuery, parameters);

                if (newTarget != null)
                {
                    return newTarget;
                }

                throw new Exception("Could not create target");
            }
        }

        public IEnumerable<Target> GetAll()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var targets = db.Query<Target>("Select * from Targets").ToList();

                return targets;
            }
        }
    }
}
