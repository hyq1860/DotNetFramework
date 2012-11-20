using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DotNet.Data;

namespace DotNet.Base.DataAccess {
    public class CommonPager
    {
 
        //public static void Test()
        //{
        //    string tablename = "SRM.dbo.Product as t1 left join SRM.dbo.Category as t2 on t1.CategoryId=t2.CategoryId ";
        //    string counts = DotNet.Common.Page.DataPager.getPageListCounts(tablename, "ProductId", "", 0);
        //    int count = 0;
        //    using (DataCommand cmd = DataCommandManager.GetDataCommand("GetUsers"))
        //    {
        //        cmd.CommandText = counts;
        //        count = (int)cmd.ExecuteScalar();
        //    }
        //    int pageCount = 0;
        //    getParkDataList("", 1, out pageCount, 2, count);
        //}


        //public static  IList getParkDataList(string key, int curPage, out int pageCount, int pageSize, int Counts) 
        //{

        //    IList list = new ArrayList();

        //    string SECLECT_FIELD = "*";
        //    string SECLECT_TABLE = "SRM.dbo.Product as t1 left join SRM.dbo.Category as t2 on t1.CategoryId=t2.CategoryId ";
        //    string SECLECT_CONDITION = string.Empty;


        //    //if (key != string.Empty) {
        //    //    SECLECT_CONDITION = " AND T_Park.ParkTitle like '%" + key + "%'";
        //    //}

        //    string SELECT_ID = "ProductId";
        //    string SELECT_FLDSORT = "ProductId";
        //    int SELECT_SORT = 1;
        //    int SELECT_DIST = 0;
        //    string SQL = DotNet.Common.Page.DataPager.getPageListSql(SECLECT_TABLE, SECLECT_FIELD, pageSize, curPage, out pageCount, Counts, SELECT_FLDSORT, SELECT_SORT, SECLECT_CONDITION, SELECT_ID, SELECT_DIST);
        //    using (DataCommand cmd = DataCommandManager.GetDataCommand("GetUsers"))
        //    {
        //        cmd.CommandText = SQL;
        //        DataTable dt = cmd.ExecuteDataTable();
        //    }
        //    //string strCondition;        
        //    //OleDb db = new OleDb();
        //    //ParkBE park;
        //    //using (OleDbDataReader dr = (OleDbDataReader)db.ExecuteReader(CommonFun.GetConnectionString(), CommandType.Text, SQL)) {
        //    //    while (dr.Read()) {
        //    //        park = new ParkBE();
        //    //        park.ParkID = Convert.ToInt32(dr[0]);
        //    //        park.ParkTitle = dr[1].ToString();
        //    //        park.ParkLetter = dr[2].ToString();
        //    //        park.ParkAreaName = dr[3].ToString();
        //    //        park.ParkTypeName = dr[4].ToString();
        //    //        list.Add(park);
        //    //    }
        //    //}
        //    return list;
        //}

    }
}
