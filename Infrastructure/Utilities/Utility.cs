using Application.Parameters;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public class Utility
    {
        public static string FilterQuery(string Query, object entity, string sortcolumn, string sortorder, out DynamicParameters dynamicParameters)
        {
            DynamicParameters dynamic = new DynamicParameters();

            if (entity != null)
            {
                PropertyInfo[] Properties = entity.GetType().GetProperties();
                string whereClause = "";
                int count = 0;


                foreach (var property in Properties)
                {

                    if (property.GetValue(entity) != null && property.GetValue(entity).ToString() != "0" && property.GetValue(entity).ToString() != "\0" && property.GetValue(entity).ToString() != "1/1/0001 12:00:00 AM" && property.Name != "UserTokens")
                    {
                        if (count >= 1 && count < Properties.Length)
                        {
                            whereClause += " and ";
                        }

                        whereClause += "UPPER(" + property.Name + ")=:" + property.Name;
                        dynamic.Add(property.Name, property.GetValue(entity).ToString().ToUpper());
                        count++;
                    }

                }

                Query = count >= 1 ? Query + " where " + whereClause : Query;
            }

            sortcolumn = sortcolumn != null && sortcolumn != "" ? sortcolumn : " 1 ";
            sortorder = sortorder != null ? sortorder.ToUpper() : "";
            string orderClause = " ORDER BY  " + sortcolumn;
            sortorder = sortorder == " DESC" ? " DESC" : " ASC";


            Query = Query + orderClause + sortorder;
            dynamicParameters = dynamic;
            return Query;
        }

        public static string PaginatedQuery(string query, RequestPageParameter requestPageParameter)
        {
            int lowrlimit, upperlimit;
            requestPageParameter.PageNumber = requestPageParameter.PageNumber == 0 ? 1 : requestPageParameter.PageNumber;
            requestPageParameter.PageSize = requestPageParameter.PageSize == 0 ? 10 : requestPageParameter.PageSize;
            lowrlimit = requestPageParameter.PageNumber == 1 ? 0 : (requestPageParameter.PageNumber - 1) * requestPageParameter.PageSize;
            upperlimit = requestPageParameter.PageNumber == 1 ? requestPageParameter.PageSize : lowrlimit + requestPageParameter.PageSize;
            // Query to get rownum along with data 
            query = "select a.*,rownum rn from ( " + query + " ) a";
            query = "select * from (" + query + ") a  where a.rn > " + lowrlimit + "  and a.rn <= " + upperlimit;

            return query;
        }
    }
}
