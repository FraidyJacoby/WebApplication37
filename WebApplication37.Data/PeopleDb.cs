using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace WebApplication37.Data
{
    public class PeopleDb
    {
        private string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                conn.Open();
                var ppl = new List<Person>();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ppl.Add(new Person
                    {
                        Id = (int)reader["Id"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"]
                    });
                }
                return ppl;
            }
        }

        public void AddPerson(Person person)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO People(FirstName, LastName, Age)
                                    VALUES(@firstName, @lastName, @age)
                                    SELECT SCOPE_IDENTITY()";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                conn.Open();
                person.Id = (int)(decimal)cmd.ExecuteScalar();
            }
        }

        public void EditPerson(Person person)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE People
                                    SET FirstName = @firstName, LastName = @lastName, Age = @age
                                    WHERE Id = @id";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                cmd.Parameters.AddWithValue("@id", person.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePerson(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM People WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
