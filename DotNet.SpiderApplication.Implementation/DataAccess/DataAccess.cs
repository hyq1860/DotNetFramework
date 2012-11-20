// -----------------------------------------------------------------------
// <copyright file="DataAccess.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Data;
using DotNet.Common;
using DotNet.Data;

namespace DotNet.SpiderApplication.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataAccess
    {
        public static void Insert(string categoryId, string name, string url, string parentCategoryId)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProductCategory"))
            {
                cmd.CommandText = cmd.CommandText.Replace("@CategoryId", categoryId).Replace("@Name", name).Replace("@Url", url).Replace("@ParentCategoryId", parentCategoryId);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool IsExistUrl(string url)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("IsExistUrl"))
            {
                cmd.SetParameterValue("@Url", url);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public static void InsertProduct(string name, string url, int categoryId, int commentNumber, string ListImage)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("InsertProduct"))
            {
                cmd.SetParameterValue("@ProductId", PrimaryKeyGenerator.NewComb().ToString().Replace("-", ""));
                cmd.SetParameterValue("@Name", name);
                cmd.SetParameterValue("@Url", url);
                cmd.SetParameterValue("@ListImage", ListImage);
                cmd.SetParameterValue("@CategoryId", categoryId);
                cmd.SetParameterValue("@Supplier", 1);
                //cmd.SetParameterValue("@Inventory", );
                cmd.SetParameterValue("@CommentNumber", commentNumber);
                cmd.SetParameterValue("@InDate", DateTime.Now);
                cmd.SetParameterValue("@EditDate", DateTime.Now);
                cmd.ExecuteNonQuery();
            }

        }

        public static DataTable GetProductCategory(string sqlWhere)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductCategory"))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText = cmd.CommandText + " and " + sqlWhere;
                }
                return cmd.ExecuteDataTable();
            }
        }

        public static DataTable GetProduct(string sqlWhere)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProducts"))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    cmd.CommandText += sqlWhere;
                }
                return cmd.ExecuteDataTable();
            }
        }

        public static void UpdateProduct(string productId, string name, decimal price, int isProcess, double elapse,DateTime editTime)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("UpdateProduct"))
            {
                cmd.SetParameterValue("@ProductId", productId);
                cmd.SetParameterValue("@Name", name);
                cmd.SetParameterValue("@Price", price);
                cmd.SetParameterValue("@IsProcess", isProcess);
                cmd.SetParameterValue("@Elapse", elapse);
                cmd.SetParameterValue("@EditDate", editTime);
                cmd.ExecuteNonQuery();
            }
        }

        public static void SaveHtmlSource(string productId,string html)
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("SaveHtmlSource"))
            {
                cmd.SetParameterValue("@ProductId", productId);
                cmd.SetParameterValue("@Source", html);
                cmd.ExecuteNonQuery();
            }
        }

        public static int GetProductsCount()
        {
            using (DataCommand cmd = DataCommandManager.GetDataCommand("GetProductsCount"))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

    }
}
