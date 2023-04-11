﻿using InitialProject.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    class UserRepository
    {
        private const string filePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> serializer;

        private List<User> users;

        public UserRepository()
        {
            serializer = new Serializer<User>();
            users = serializer.FromCSV(filePath);
        }

        public User GetByUsername(string username)
        {
            users = serializer.FromCSV(filePath);
            return users.FirstOrDefault(u => u.username == username);
           
        }
        public List<User> GetAll()
        {
            return serializer.FromCSV(filePath);
        }
        public User Save(User user)
        {

            user.id = NextId();
            users = serializer.FromCSV(filePath);
            users.Add(user);
            serializer.ToCSV(filePath, users);
            return user;

        }

        private int NextId()
        {
            users = serializer.FromCSV(filePath);
            if (users.Count < 1)
            {
                return 1;
            }
            return users.Max(c => c.id) + 1;
        }
    }
}
