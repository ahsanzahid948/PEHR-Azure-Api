using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    public static class DatabaseService
    {

        public static string SQLWhere(params object[] obj)
        {
            string query = "";
            for (int i = 0; i < obj.Length; i = i + 2)
            {
                if (obj[i + 1] == null || string.IsNullOrEmpty(obj[i + 1].ToString()))
                {
                    continue;
                }
                if (obj[i].ToString().Contains("LIKE") && obj[i + 1].ToString().Length == 2)
                {
                    continue;
                }
                query += " " + obj[i] + " = " + " '" + obj[i + 1] + "' " + " ";
                query += obj[i + 1] != null && (obj[i].ToString().Contains("LIKE") && obj[i + 1].ToString().Length == 2) ? " , " : " ";
            }
            var s = query.Length;
            if (query.Length > 0)
            {
                query = " Where " + query;
            }
            return query;
        }
        public static string SQLWhereParameterized(out DynamicParameters param, params object[] obj)
        {
            string query = "";
            if (obj.Length % 2 != 0)
            {
                throw new Exception("Missing Value/Key in Parameters");
            }
            param = new DynamicParameters();
            var temp = string.Empty;
            int count = 0;
            // param = new Dictionary<string, string>();

            for (int i = 0; i < obj.Length; i = i + 2)
            {

                if (obj[i + 1] == null || string.IsNullOrEmpty(obj[i + 1].ToString()))//Checking Null Value and Continue Loop From Start
                {
                    continue;
                }
                //Appending To Query and Params
                //Checks UPPER WITH LIKE

                if ((!string.IsNullOrEmpty(obj[i + 1].ToString()) && !obj[i + 1].ToString().Contains("%%")) && (obj[i].ToString().Contains("UPPER") && obj[i].ToString().Contains("LIKE")))
                {
                    temp = obj[i].ToString().Split("(")[1];
                    temp = temp.Split(")")[0].ToUpper();
                    query = AddANDSpaceInQuery(count, query);
                    query += obj[i] + " :" + temp + " ";
                    //   param.Add(obj[i].ToString().ToUpper(), obj[i + 1].ToString().ToUpper());
                    param.Add(temp, obj[i + 1].ToString().ToUpper());
                    count++;
                }
                //Checks LIKE ONLY
                else if ((!string.IsNullOrEmpty(obj[i + 1].ToString()) && !obj[i + 1].ToString().Contains("%%")) && (obj[i].ToString().Contains("LIKE") && !obj[i].ToString().Contains("UPPER")))
                {

                    temp = obj[i].ToString().Split("(")[1];
                    temp = temp.Split(")")[0].ToUpper();
                    query = AddANDSpaceInQuery(count, query);
                    query += obj[i] + " = UPPER(:" + temp + ") ";
                    // param.Add(obj[i].ToString().ToUpper(), obj[i + 1].ToString().ToUpper());
                    param.Add(temp, obj[i + 1].ToString().ToUpper());
                    count++;
                }
                //CHECKS ONLY VALUE
                else if (!string.IsNullOrEmpty(obj[i].ToString()) && !string.IsNullOrEmpty(obj[i + 1].ToString()) && !obj[i + 1].ToString().Contains("%%"))
                {
                    temp = obj[i].ToString();
                    //temp = temp.Split(")")[0].ToUpper();
                    query = AddANDSpaceInQuery(count, query);
                    query += " " + obj[i] + " = " + " :" + temp + " " + " ";
                    //  param.Add(obj[i].ToString().ToUpper(), obj[i + 1].ToString().ToUpper());
                    param.Add(temp, obj[i + 1].ToString().ToUpper());
                    count++;
                }


            }

            if (query.Length > 0)
            {
                query = " AND " + query;
            }
            return query;
        }
        public static string AddANDSpaceInQuery(int count, string val)
        {
            if (count >= 1)
                val = val + "AND";
            return val;
        }
        public static DateTime Date(this string val)
        {

            DateTime result;
            if (string.IsNullOrEmpty(val))
            {
                return new DateTime();
            }
            DateTime.TryParse(val, out result);

            if (result == DateTime.MinValue)
            {
                return new DateTime();
            }
            else
            {
                return result;
            }


        }

        public static string SQLWhereParameterizedObject(out DynamicParameters dynamicQueryParams, dynamic obj, string QueryCase)
        {
            PropertyInfo[] myProperty = obj.GetType().GetProperties();
            dynamicQueryParams = new DynamicParameters();
            int queryConditioning;
            string query = string.Empty;
            switch (QueryCase.ToUpper())
            {
                case "INSERT":
                    queryConditioning = 0;
                    foreach (var item in myProperty)
                    {
                        dynamic propertyValue = item.GetValue(obj);
                        query += ":" + item.Name + " ";
                        dynamicQueryParams.Add(item.Name, propertyValue);
                        queryConditioning++;
                        if (queryConditioning >= 1 && queryConditioning < myProperty.Length)
                        {
                            query += " , ";
                        }
                    }
                    query = " Values ( " + query + " )";
                    break;
                case "UPDATE":
                    queryConditioning = 0;
                    foreach (var prop in myProperty)
                    {
                        dynamic val = prop.GetValue(obj);
                        if (val == null)
                            continue;
                        query += prop.Name + "= :" + prop.Name + " ";
                        dynamicQueryParams.Add(prop.Name, val);
                        queryConditioning++;
                        if (queryConditioning >= 1 && queryConditioning < myProperty.Length)
                        {
                            query += " , ";
                        }
                    }
                    query = "SET  " + query + " ";
                    break;
                case "SELECT":
                    queryConditioning = 0;
                    foreach (var prop in myProperty)
                    {
                        dynamic val = prop.GetValue(obj);
                        if (val == null)
                            continue;
                        query += prop.Name + "= :" + prop.Name + " ";
                        dynamicQueryParams.Add(prop.Name, val);
                        queryConditioning++;
                        if (queryConditioning >= 1 && queryConditioning < myProperty.Length)
                        {
                            query += " , ";
                        }
                    }
                    query = "WHERE ( " + query + " ) ";
                    break;
                default:
                    break;
            }

            return query;
        }

        public static DynamicParameters KeyValue(object[] whereParams = null)
        {
            DynamicParameters dynamicParam = new DynamicParameters();
            for (int i = 0; i < whereParams.Length; i = i + 2)
            {
                dynamicParam.Add(whereParams[i].ToString(), whereParams[i + 1]);
            }
            return dynamicParam;
        }
        public async static Task<List<dynamic>> SelectCommandList<dynamic>(IConfiguration _configuration, string connectionString, string query, params object[] whereParams)
        {

            using (var connection = new OracleConnection(_configuration.GetConnectionString(connectionString)))
            {
                connection.Open();
                var response = await connection.QueryAsync<dynamic>(query, DatabaseService.KeyValue(whereParams));
                return response.ToList();
            }
        }
        public async static Task<dynamic> SelectCommandValue<dynamic>(IConfiguration _configuration, string connectionString, string query, params object[] whereParams)
        {

            using (var connection = new OracleConnection(_configuration.GetConnectionString(connectionString)))
            {
                connection.Open();
                var response = await connection.QueryFirstOrDefaultAsync<dynamic>(query, DatabaseService.KeyValue(whereParams));
                return response;
            }
        }
        public async static Task<int> InsertUpdateCommandList(IConfiguration _configuration, string connectionString, string query, params object[] whereParams)
        {
            using (var connection = new OracleConnection(_configuration.GetConnectionString(connectionString)))
            {
                connection.Open();
                var response = await connection.ExecuteAsync(query, DatabaseService.KeyValue(whereParams));
                return response;
            }
        }

        public static DynamicParameters ObjectKeyValue(dynamic obj)
        {
            PropertyInfo[] myProperty = obj.GetType().GetProperties();
            DynamicParameters dynamicProp = new DynamicParameters();
            foreach (var item in myProperty)
            {
                var s = item.PropertyType.Name;
                var val = item.GetValue(obj);
                switch (s.ToLower())
                {
                    case "double":
                        if (item.GetValue(obj) > 0)
                        {
                            dynamicProp.Add(item.ToString().ToUpper(), double.Parse(item.GetValue(obj)));
                        }
                        break;
                    case "long":
                        if (item.GetValue(obj) > 0)
                        {
                            dynamicProp.Add(item.ToString().ToUpper(), long.Parse(item.GetValue(obj)));
                        }
                        break;

                    case "string":
                        if (!string.IsNullOrEmpty(item.GetValue(obj)))
                        {
                            dynamicProp.Add(item.ToString().ToUpper(), item.GetValue(obj));
                        }
                        break;
                    case "char":

                        break;
                    case "datetime":
                        if (item.GetValue(obj) != DateTime.MinValue)
                        {
                            dynamicProp.Add(item.ToString().ToUpper(), item.GetValue(obj));
                        }
                        break;

                }
            }
            return dynamicProp;
        }
    }
}
