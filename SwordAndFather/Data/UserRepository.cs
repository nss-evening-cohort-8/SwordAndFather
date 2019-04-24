using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        const string ConnectionString = "Server=localhost;Database=SwordAndFather;Trusted_Connection=True;";

        public User AddUser(string username, string password)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var insertUserCommand = connection.CreateCommand();
                insertUserCommand.CommandText = $@"Insert into users (username,password)
                                              Output inserted.*
                                              Values(@username,@password)";

                insertUserCommand.Parameters.AddWithValue("username", username);
                insertUserCommand.Parameters.AddWithValue("password", password);

                var reader = insertUserCommand.ExecuteReader();

                if (reader.Read())
                {
                    var insertedPassword = reader["password"].ToString();
                    var insertedUsername = reader["username"].ToString();
                    var insertedId = (int)reader["Id"];

                    var newUser = new User(insertedUsername, insertedPassword) { Id = insertedId };

                    return newUser;
                }
            }

            throw new Exception("No user found");
        }










        public List<User> GetAll()
        {
            var users = new List<User>();

            var connection = new SqlConnection("Server=localhost;Database=SwordAndFather;Trusted_Connection=True;");
            connection.Open();

            var getAllUsersCommand = connection.CreateCommand();
            getAllUsersCommand.CommandText = @"select username,password,id  
                                               from users";

            var reader = getAllUsersCommand.ExecuteReader();

            while (reader.Read())
            {
                var id = (int)reader["Id"];
                var username = reader["username"].ToString();
                var password = reader["password"].ToString();
                var user = new User(username, password) { Id = id };

                users.Add(user);
            }

            connection.Close();

            return users;
        }
    }
}