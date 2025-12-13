using _17_AdoNetExample.DbServices.Abstract;
using _17_AdoNetExample.Models;
using Microsoft.Data.SqlClient;

namespace _17_AdoNetExample.DbServices.Concrete
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;

        public DbService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // ExecuteNonQuery Ado.NET'te Insert, Update, Delete gibi veri değiştiren
        // işlemleri gerçekleştirmek için kullanılır. Geriye int döner.

        public void ExecuteNonQuery(string query)
        {
            // Veri sızıntısını önlemek için Dispose işlemi kullanılır.
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Parametreli ExecuteNonQuery metodu
        public void ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if(parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // ExecuteReader metodu, veri tabanından veri okumak için kullanılır. Geriye tablo
        // şeklinde veri döner. (SELECT * FROM Students)
        public List<Student> ExecuteReader(string query)
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Age = Convert.ToInt32(reader["Age"])
                            };
                            students.Add(student); // tablodan gelen her satırı listeye ekliyoruz
                        }
                    }
                }
            }

            return students;
        }

        // ExecuteScalar metodu, tek bir değer döndüren sorgular için kullanılır.
        // Aggregate fonksiyonlar (COUNT, SUM, AVG) için idealdir.
        // ExecuteScalar geriye object döner.
        public object ExecuteScalar(string query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    return command.ExecuteScalar();
                }
            }
        }
    }
}
