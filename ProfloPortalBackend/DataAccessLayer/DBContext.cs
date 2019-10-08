using ProfloPortalBackend.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.DataAccessLayer
{
    public class DBContext
    {
        MongoClient mongoClient;
        IMongoDatabase mongoDatabase;
        public DBContext(IConfiguration configuration)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                mongoClient = new MongoClient(Environment.GetEnvironmentVariable("mongo_db"));
            }
            else
            {
                mongoClient = new MongoClient(configuration.GetSection("MongoDb:server").Value);
            }
            mongoDatabase = mongoClient.GetDatabase(configuration.GetSection("MongoDB:database").Value);
        }
        public IMongoCollection<Team> Teams => mongoDatabase.GetCollection<Team>("Teams");
        public IMongoCollection<Card> Cards => mongoDatabase.GetCollection<Card>("Cards");
        public IMongoCollection<Board> Boards => mongoDatabase.GetCollection<Board>("Boards");
        public IMongoCollection<List> Lists => mongoDatabase.GetCollection<List>("Lists");
        public IMongoCollection<Member> Members => mongoDatabase.GetCollection<Member>("Members");
        public IMongoCollection<Invite> Invites => mongoDatabase.GetCollection<Invite>("Invites");

    }
}
