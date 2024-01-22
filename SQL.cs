using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazin
{
    class SQL
    {
        public string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";

        //Конструктор
        public SQL()
        {

        }
        //#region ExecuteNonQuery
        //public int iExecuteNonQuery(string FileData, string sSql, int where)
        //{
        //    int n = 0;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection())
        //        {
        //            if (where == 0)
        //            {
        //                con.ConnectionString = @"Data Source=" + conString + ";New=True;Version=3";
        //            }
        //            else
        //            {
        //                con.ConnectionString = @"Data Source=" + conString + ";New=False;Version=3";
        //            }
        //            con.Open();
        //            using (SQLCommand sqlCommand = con.CreateCommand())
        //            {



        //                sqlCommand.CommandText = sSql;
        //                n = sqlCommand.ExecuteNonQuery();
        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        n = 0;

        //    }
        //    return n;

        //}
        //#endregion 
        //#region Execute
        //public DataRow[] drExecute(string FileData, string sSql)
        //{
        //    DataRow[] datarows = null;
        //    SQLiteDataAdapter dataadapter = null;
        //    DataSet dataset = new DataSet();
        //    DataTable datatable = new DataTable();
        //    try
        //    {
        //        using (SQLiteConnection con = new SQLiteConnection())
        //        {
        //            con.ConnectionString = @"Data Source=" + FileData + ";New=False;Version=3";
        //            con.Open();
        //            using (SQLiteCommand sqlCommand = con.CreateCommand())
        //            {
        //                dataadapter = new SQLiteDataAdapter(sSql, con);
        //                dataset.Reset();
        //                dataadapter.Fill(dataset);
        //                datatable = dataset.Tables[0];
        //                datarows = datatable.Select();


        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        datarows = null;
        //    }
        //    return datarows;

        //}
        //#endregion 


    }
}
