using Application.DTOs.Common.SearchFilter;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common.Services
{
    public static class CommonService
    {
        public static string Upper(this string val)
        {
            return !string.IsNullOrEmpty(val) ? val.ToUpper() : "";
        }

        public static dynamic Assign(dynamic val)
        {
            Type unknown = val.GetType();
            if (unknown.Name == "String")
            {
                return !string.IsNullOrEmpty(val) ? val : "";
            }
            else if (unknown.Name == "Double")
            {
                return double.Parse(val);
            }
            else if (unknown.Name == "Long")
            {
                return long.Parse(val);
            }
            else if (unknown.Name == "DateTime")
            {
                var date = DateTime.ParseExact(val, "mmddyyyy", null);
                return date;
            }
            else
            {
                throw new Exception("Unknow Data Type");
            }

        }

        public static string DoQueryFiltering(string where)
        {
            GenericSearchFilter obj = new GenericSearchFilter();
            var type = obj.GetType().GetProperties();
            where = where.Split("?")[1];
            var query = QueryHelpers.ParseQuery(where);
            List<string> whereClauses = new List<string>();
            string whereQuery = string.Empty;
            foreach (var item in query)
            {
                string key = item.Key;
                if (key == "sort_by" || key == "order_by" || key == "limit")
                {
                    switch (key)
                    {
                        case "sort_by":
                            whereQuery += " ORDER BY ";
                            whereQuery += " " + DoAscDescMerging(item.Value);

                            break;
                        case "limit":

                            break;
                        
                    }
                }
                else
                {
                    whereClauses.Add(item.Key + " = " + item.Value + " ");
                }


            }
            string final ="WHERE "+ string.Join(",", whereClauses) + whereQuery;
            return final;
           /* foreach(var item in type)
            {
                string s = item.GetValue(type);
            }*/
        }

        public static string DoAscDescMerging(string des)
        {
            string[] splitDesc = des.Split(",");
            List<string> AscDesc = new List<string>();
            des = string.Empty;

            foreach (var item in splitDesc)
            {
                string check = item.Contains("asc") ? "asc" : "desc";
                switch (check)
                {
                    case "asc":
                        string checkAscDesc = item.Split("(")[1];
                        checkAscDesc = checkAscDesc.Substring(0, checkAscDesc.Length - 1);
                        if (checkAscDesc.Contains("~"))
                            AscDesc.Add(" " + SplitAscDescTags(checkAscDesc) + " ASC ");
                        else
                            AscDesc.Add(" " + checkAscDesc + " ASC ");
                        break;
                    case "desc":
                        checkAscDesc = item.Split("(")[1];
                        checkAscDesc = checkAscDesc.Substring(0, checkAscDesc.Length - 1);
                        if (checkAscDesc.Contains("~"))
                            AscDesc.Add(SplitAscDescTags(checkAscDesc) + " DESC ");
                        else
                            AscDesc.Add(checkAscDesc + " DESC ");

                        break;
                }


            }
            des = string.Join(" , ", AscDesc);
            return des;
        }

        public static string SplitAscDescTags(string split)
        {
            string[] splitCol = split.Split("~");
            List<string> col = new List<string>();
            split = string.Empty;
            foreach (var item in splitCol)
            {
                col.Add(item);
            }
            split = string.Join(" , ", col);
            return split;
        }

        public static void ApplySorting(in List<dynamic> dataset, string page, string limit)
        {
            int lowerLimit = Int32.Parse(page) > 1 ? (Int32.Parse(page) - 1) * (Int32.Parse(limit)) : 1;
            int maxLimit = Int32.Parse(page) > 1 ? lowerLimit + Int32.Parse(limit) : Int32.Parse(limit);
            dataset.Skip(lowerLimit).Take(maxLimit);
        }
    }


}
