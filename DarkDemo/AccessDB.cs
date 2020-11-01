using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Collections;
namespace DarkDemo
{
    class AccessDB
    {
        public OleDbConnection conn { get; set; }
        public string path { get; set; }
       
        const string CONNECTION_STRING = @"Provider=Microsoft.ACE.OLEDB.12.0 ; Data source= "; //C:\Documents and Settings\username\My Documents\AccessFile.mdb";
        public AccessDB(string path)
        {
            this.path = path;
        }
        public bool Connect(string password)
        {
            conn = new OleDbConnection();
            conn.ConnectionString = CONNECTION_STRING + this.path+ ";Jet OLEDB:Database Password=" + password;
            
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
            return true ;
        }
        public void Disconnect()
        {
            this.conn.Close();
        }
        public List<Hashtable> GetTable(string tableName)
        {
            try
            {


                List<Hashtable> dataGrid = new List<Hashtable>();
                // Hashtable data = new Hashtable();

                string sql = "SELECT * FROM " + tableName;
                OleDbCommand command = new OleDbCommand(sql, conn);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Hashtable temp = new Hashtable();
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        string header = dr.GetName(i);
                        temp[header] = dr[i];
                    }
                    dataGrid.Add(temp);
                }
                return dataGrid;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool DeleteRecord(string tableName,string critfield, object critValue)
        {
            string sql;
            if(critValue is int)
            {
                sql = "DELETE FROM " + tableName + " WHERE " + critfield + " = " + critValue;
            }
            else
                sql= "DELETE FROM " + tableName + " WHERE " + critfield + " = \'" + critValue + "\'";
          
            OleDbCommand command = new OleDbCommand(sql, conn);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }
        }
        public bool UpdateTable(string tableName, Hashtable record, string critfield,object critValue)
        {
            string sql = "UPDATE " + tableName + " SET ";
            int count = 0;
            foreach (string item in record.Keys)
            {
                if (count != 0)
                    sql += " , ";
                if(record[item] is string)
                    sql += "[" + item + "]" + " = \'" + record[item]+"\'";
                else
                    sql += "[" + item + "]" + " = " + record[item];
                count++;
            }
            sql += " WHERE ["+ critfield +"] = @id";
            OleDbCommand command = new OleDbCommand(sql, conn);
            command.Parameters.AddWithValue(("id"), critValue);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }

        //    return true;
        }
 
        public string InsertIntoTable(string tableName, Hashtable record)
        {
            string fields = "(";
            string values = "(";
            int count = 0;
            foreach (string item in record.Keys)
            {
                object tempVal = record[item];
                if (tempVal is string)
                    tempVal = "\'" + tempVal + "\'";
                if (count == 0)
                {
                    fields +="[" +item+ "]";
                    values += tempVal;
                }
                else
                {
                    fields += "," + "[" + item + "]";
                    values += "," + tempVal;
                }
                count++;
            }
            fields += ")";
            values += ")";
            string sql = "INSERT INTO " + tableName + fields + " VALUES " + values + ";";
            OleDbCommand command = new OleDbCommand(sql, conn);
            try
            {
                command.ExecuteNonQuery();
                sql = "Select @@Identity";
                command = new OleDbCommand(sql, conn);
                string s = command.ExecuteScalar().ToString();
                return s;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }
          
         //   return true;
        }
        public List<object> GetFieldFromTable(string tableName,string field)
        {
            List<object> lst = new List<object>();
            string sql = "SELECT * FROM " + tableName;
            OleDbCommand command = new OleDbCommand(sql, conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lst.Add(dr[field]);
            }
            return lst;
         }
        public List<Hashtable> GetTableWithCriteria(string tableName, List<string> fieldToCompare, List<string> valueToCompare)
        {
            List<Hashtable> dataGrid = new List<Hashtable>();
            string sql = "SELECT * FROM " + tableName;

            for (int i = 0; i < fieldToCompare.Count; i++)
            {
                if (i == 0)
                    sql += " WHERE";
                else
                    sql += " AND";
                sql += " " + fieldToCompare[i] + "" + " = \'" + valueToCompare[i] + "\'";
            }
          

            OleDbCommand command = new OleDbCommand(sql, conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                Hashtable temp = new Hashtable();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    string header = dr.GetName(i);
                    temp[header] = dr[i];
                }
                dataGrid.Add(temp);
            }
            return dataGrid;
        }
        public List<object> GetTableWithCriteriaSorting(string tableName,List<string> fieldToCompare, List<string> valueToCompare,string orderByField,string orderFlow,string fieldToGet)
        {
            List<object> lst = new List<object>();
            string sql = "SELECT * FROM " + tableName; 

            for (int i = 0; i < fieldToCompare.Count; i++)
            {
                if (i == 0)
                    sql += " WHERE";
                else
                    sql += " AND";
                sql +=  " " + fieldToCompare[i] +  " = \'" + valueToCompare[i] + "\'";
            }
            sql+= " ORDER BY " + orderByField + " " + orderFlow; 

            OleDbCommand command = new OleDbCommand(sql, conn);
            OleDbDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lst.Add(dr[fieldToGet]);
            }
            return lst;
        }

    }
}
