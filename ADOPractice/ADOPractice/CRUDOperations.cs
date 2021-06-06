using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADOPractice
{
    public static class CRUDOperations
    {
        public static void ToConsole(List<dynamic[]> list)
        {
            foreach (var row in list)
            {
                foreach (var property in row)
                {
                    if (property.ToString() == "")
                    {
                        Console.Write("| NULL |\t");
                    }
                    else
                    {
                        Console.Write("| " + property + " |\t");
                    }
                }

                Console.WriteLine();
            }
        }

        public static List<dynamic[]> GetResultNonParametrized(string connectionString, string expression, int columns)
        {
            var toupleList = new List<dynamic[]>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(expression, sqlConnection);
                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        dynamic[] arr = new dynamic[columns];

                        for (int i = 0; i < columns; i++)
                        {
                            arr[i] = dataReader.GetValue(i);
                        }

                        toupleList.Add(arr);
                    }
                }
                else
                {
                    dataReader.Close();
                }
            }

            return toupleList;
        }

        public static List<dynamic[]> GetResultParametrized(string connectionString, string expression, int columns, Dictionary<string, object> parameters)
        {
            var toupleList = new List<dynamic[]>();

            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var sqlCommand = new SqlCommand(expression, sqlConnection);

                foreach (var param in parameters)
                {
                    sqlCommand.Parameters.AddWithValue(param.Key, param.Value);
                }

                var dataReader = sqlCommand.ExecuteReader();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        dynamic[] arr = new dynamic[columns];

                        for (int i = 0; i < columns; i++)
                        {
                            arr[i] = dataReader.GetValue(i);
                        }

                        toupleList.Add(arr);
                    }
                }
                else
                {
                    dataReader.Close();
                }
            }

            return toupleList;
        }

        //так как реализация этих функций никак не отличается, этот метод выполняет как добавление и обновление данных, так и удаление. Но считывание не поддерживает
        public static int Insert_Update_Delete_NonParametrized(string connectionString, string expression)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(expression, connection);

                int affected = command.ExecuteNonQuery();

                return affected;
            }
        }

        public static int Insert_Update_Delete_Parametrized(string connectionString, string expression, Dictionary<string, object> parameters)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(expression, connection);

                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }

                int affected = command.ExecuteNonQuery();

                return affected;
            }
        }
    }
}
