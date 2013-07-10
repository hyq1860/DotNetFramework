using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DotNetTest
{
    public class NPOIExcel
    {
        public static void ReadYinTaiStoreExcel()
        {
            Stream excel=new FileStream(@"E:\工作资料\门店帐号信息完整版.xlsx",FileMode.Open);
            IWorkbook workbook = new XSSFWorkbook(excel);
            ISheet sheet = workbook.GetSheet("门店信息");
            var sb = new StringBuilder();
            for (int i = sheet.FirstRowNum+1; i < sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                int j = row.FirstCellNum+3;
                sb.AppendFormat(
                        "INSERT INTO [YintaiEvent].[dbo].[Store]([StoreId],[City],[StoreName],[CompanyFullName],[Address],[OfficeAddress],[Phone],[Bank1],[BankAccount1],[Bank2],[BankAccount2],[InUserId],[EditUserId],[InDate],[EditDate])VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',{13},{14})", i, row.GetCell(j-2),row.GetCell(j-1), row.GetCell(j), row.GetCell(j + 1), row.GetCell(j + 2), row.GetCell(j + 3), row.GetCell(j + 4), row.GetCell(j + 5), row.GetCell(j + 6), row.GetCell(j + 7), "0", "0", "GetDate()", "GetDate()");
                sb.Append("\n");
            }
            var str = sb.ToString();
        }
    }
}
