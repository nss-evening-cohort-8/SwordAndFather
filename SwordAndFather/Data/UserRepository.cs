using System.Collections.Generic;
using System.Data.SqlClient;
using SwordAndFather.Models;

namespace SwordAndFather.Data
{
    public class UserRepository
    {
        static List<User> _users = new List<User>();

        public User AddUser(string username, string password)
        {
            var newUser = new User(username, password);

            newUser.Id = _users.Count + 1;

            _users.Add(newUser);

            return newUser;
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
                var user = new User(username, password) {Id = id};

                users.Add(user);
            }

            connection.Close();

            return users;
        }
    }
}