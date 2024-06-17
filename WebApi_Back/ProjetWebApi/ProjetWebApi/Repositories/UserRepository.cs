﻿using Newtonsoft.Json;
using ProjetWebApi.Data.Entities;
using StackExchange.Redis;

namespace ProjetWebApi.Repositories
{
    public class UserRepository
    {
        private static readonly ConnectionMultiplexer redis = ConnectionMultiplexer
            .Connect("redis-19723.c258.us-east-1-4.ec2.redns.redis-cloud.com:19723,password=3aWyyDQO7gJZR2tqLnY35gk1C2ZxbBT8");
        private readonly IDatabase _database;

        public UserRepository()
        {
            _database = redis.GetDatabase();
        }

        public void CreateUser(Utilisateur user)
        {
            long newId = _database.StringIncrement("user:id_counter");

            user.Id = (int)newId;

            var json = JsonConvert.SerializeObject(user);

            _database.StringSet($"user:{user.Id}", json);
        }

        public Utilisateur GetUser(int userId)
        {
            var json = _database.StringGet($"user:{userId}");
            return json.HasValue ? JsonConvert.DeserializeObject<Utilisateur>(json) : null;
        }

        public void UpdateUser(Utilisateur user)
        {
            var json = JsonConvert.SerializeObject(user);
            _database.StringSet($"user:{user.Id}", json);
        }

        public void DeleteUser(int userId)
        {
            _database.KeyDelete($"user:{userId}");
        }
    }
}
