using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Dapper;
using Practice11june.Domain.Model;

namespace Practice11june.Infrastructure.Repository
{
    public class DapperRepository<T> where T : IEntity
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public T GetById(int id)
        {
            var tableName = GenerateTableName();
            var Id = typeof(T).GetProperties().First(p => p.Name.StartsWith(typeof(T).Name)).Name;
            T itemToReturn;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"Select * from {tableName} where {Id} = @id";

                var item = connection.QueryFirstOrDefault<T>(query, new{id});
                itemToReturn = item;
            }

            return itemToReturn;
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null)
        {
            var tableName = GenerateTableName();

            IEnumerable<T> list;

            string whereExpression;

            if (filter != null)
            {
                whereExpression = ConvertLinqToSql(filter);
            }
            else
            {
                whereExpression = string.Empty;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"Select * from {tableName} {whereExpression}";

                var items = connection.Query<T>(query, new { });
                list = items;
            }

            return list;
        }

        public int Add(T obj)
        {
            int affectedFields = 0;

            var tableName = GenerateTableName();

            var allproperties = typeof(T).GetProperties();

            var Id = typeof(T).GetProperties().First(p => p.Name.StartsWith(typeof(T).Name)).Name;

            var properties = allproperties.Where(p => p.Name != Id);

            var propnames = properties.Select(p => p.Name).ToArray();

            var propnamestring = string.Join(", ", propnames);

            var propvalues = properties.Select(p => p.GetValue(obj)).ToArray();

            var propvaluestring = string.Join(", ", propnames.Select(p => p = "@" + p.ToString()));

            var dynamicParameters = new DynamicParameters();

            for (int i = 0; i < propvalues.Length; i++)
            {
                dynamicParameters.Add("@" + propnames[i], propvalues[i]);
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"Insert into {tableName} ({propnamestring}) values ({propvaluestring})";
                affectedFields = connection.Execute(query, dynamicParameters);
            }

            return affectedFields;
        }

        public void Delete(T obj)
        {
            var tableName = GenerateTableName();

            var Id = typeof(T).GetProperties().First(p => p.Name.StartsWith(typeof(T).Name)).Name;

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"Delete from {tableName} where {Id} = @ObjectId";

                var id = typeof(T).GetProperty(Id).GetValue(obj);

                var result = connection.Execute(query, new{ ObjectId = id});
            }
        }

        public int Update(T obj)
        {
            int affected = 0;

            var tableName = GenerateTableName();

            var Id = typeof(T).GetProperties().First(p => p.Name.StartsWith(typeof(T).Name)).Name;

            var properties = typeof(T).GetProperties().Where(p => p.Name != Id);

            var propnames = properties.Select(p => p.Name).ToArray();

            var propvalues = properties.Select(p => p.GetValue(obj)).ToArray();

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < propvalues.Length; i++)
            {
                if (propvalues[i] is string)
                {
                    sb.Append($"{propnames[i]} = '{propvalues[i]}'");
                }
                else if(propvalues[i] is DateTime)
                {
                    sb.Append($"{propnames[i]} = '{propvalues[i]}'");
                }
                else
                {
                    sb.Append($"{propnames[i]} = {propvalues[i]}");
                }

                if (i != propvalues.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            var namevaluepairs = sb.ToString();

            using (var connection = new SqlConnection(connectionString))
            {
                var query = $"Update {tableName} " +
                            $"set {namevaluepairs} " +
                            $"where {Id} = @EntityId";

                var idvalue = typeof(T).GetProperty(Id).GetValue(obj);

                affected = connection.Execute(query, new {EntityId = idvalue });
            }

            return affected; 
        }

        private string GenerateTableName()
        {
            var tableName = string.Empty;
            var className = typeof(T).Name;

            if (className.EndsWith('y'))
            {
                tableName = className.Substring(0, className.Length - 1) + "ies";
            }
            else
            {
                tableName = className + 's';
            }

            return tableName;
        }

        private string ConvertLinqToSql(Expression<Func<T, bool>> filter)
        {
            var stringFilter = filter.Body.ToString();
            var a = filter.Parameters;
            var value = string.Empty;

            dynamic exp = filter.Body;
            var right = (Expression)exp.Right;

            var ba = filter.Compile();

            stringFilter = stringFilter.Replace(right.ToString(), "");

            if (right is ConstantExpression)
            {
                var rightConst = right as ConstantExpression;
                value = string.Format(value, rightConst.Value);
            }
            else if(right is MemberExpression)
            {
                var rightMem = right as MemberExpression;
                MemberExpression rightMemMem = null;

                ConstantExpression rightConst;

                if (rightMem.Expression is ConstantExpression)
                {
                    rightConst = rightMem.Expression as ConstantExpression;
                }
                else
                {
                    rightMemMem = rightMem.Expression as MemberExpression;
                    rightConst = rightMemMem.Expression as ConstantExpression;
                }

                var member = rightMem.Member.DeclaringType;
                var type = rightMem.Member.MemberType;
                object? val;

                if (rightMemMem != null)
                {
                    member = rightMemMem.Member.DeclaringType;
                    val = member.GetField(rightMem.Member.Name).GetValue(rightConst.Value);
                }
                else
                {
                    val = member.GetField(rightMem.Member.Name).GetValue(rightConst.Value);
                }
                
                if (val is string)
                {
                    value = $"'{val.ToString()}'";
                }
                else
                {
                    value = val.ToString();
                }
            }
            else if(right is Expression)
            {
                var rightMem = right as MemberExpression;
                var rightConst = rightMem.Expression as ConstantExpression;
                var member = rightMem.Member.DeclaringType;
                var type = rightMem.Member.MemberType;
                var val = member.GetField(rightMem.Member.Name).GetValue(rightConst.Value);
                if (val is string)
                {
                    value = $"'{val.ToString()}'";
                }
                else
                {
                    value = val.ToString();
                }
            }


            stringFilter = stringFilter
                .Substring(1, stringFilter.Length - 2)
                .Replace("AndAlso", "and")
                .Replace("OrElse", "or")
                .Replace("==", "=")
                .Replace("!=", "<>")
                .Replace("\"", "'");

            stringFilter = stringFilter + $" {value}";

            foreach (var parameterExpression in a)
            {
                stringFilter = stringFilter.Replace(parameterExpression + ".", "");
            }

            if (!string.IsNullOrEmpty(stringFilter))
            {
                stringFilter = "where " + stringFilter;
            }

            return stringFilter;
        }
    }
}
