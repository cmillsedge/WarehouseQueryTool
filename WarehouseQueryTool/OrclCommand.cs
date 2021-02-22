using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;

namespace WarehouseQueryTool
{
    public class OrclCommand
    {
        //private member variables for encapsulation
        private OracleConnection connection;
        public OrclCommand(DBProperties database)
        {
            try
            {//set member variable values
             //string constr = "User Id=scott;Password=tiger;Data Source=oracle";
                string constr = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + database.Host + ")(PORT=" + database.Port + "))(CONNECT_DATA=(SERVICE_NAME=" + database.SID + ")));User Id=" + database.Schema + ";Password=" + database.Password + ";";
                connection = new OracleConnection(constr);
                connection.Open();
            }
            catch (Exception)
            {
                throw ;
            }

        }

        public DataTable RunQuery(string sql)
        {
            OracleCommand cmd = null;
            OracleDataReader reader = null;
            DataTable dataTable = null;
            try
            {
                // Create the OracleCommand
                cmd = new OracleCommand(sql);

                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;

                // Execute command, create OracleDataReader object
                reader = cmd.ExecuteReader();

                using (reader = cmd.ExecuteReader())
                {
                    dataTable = new DataTable();
                    dataTable.Load(reader);

                }
            }
            catch (Exception)
            {
                throw ;
            }
            finally
            {   
                cmd.Dispose();
                dataTable.Dispose();
            }
            return dataTable;


        }

        public void ExecuteOneParamProc(string procedure, string paramtype, string paramname, string parametervalue)
        {
            OracleCommand cmd = null;
            //add parameters
            OracleParameter prm1 = null;

            try
            {
                cmd = new OracleCommand(procedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (paramtype == "STRING")
                {
                    prm1 = new OracleParameter(paramname, OracleDbType.Varchar2);
                    prm1.Direction = ParameterDirection.Input;
                    prm1.Value = parametervalue;
                }
                else
                {
                    prm1 = new OracleParameter(paramname, OracleDbType.Int32);
                    prm1.Direction = ParameterDirection.Input;
                    prm1.Value = Int32.Parse(parametervalue);
                }

                cmd.Parameters.Add(prm1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {   // Clean up
                cmd.Dispose();
                prm1.Dispose();
            }


        }

        public async Task<int> ASyncExecuteOneParamProc(string procedure, string paramtype, string paramname, string parametervalue)
        {
            OracleCommand cmd = null;
            //add parameters
            OracleParameter prm1 = null;

            try
            {
                cmd = new OracleCommand(procedure, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (paramtype == "STRING")
                {
                    prm1 = new OracleParameter(paramname, OracleDbType.Varchar2);
                    prm1.Direction = ParameterDirection.Input;
                    prm1.Value = parametervalue;
                }
                else
                {
                    prm1 = new OracleParameter(paramname, OracleDbType.Int32);
                    prm1.Direction = ParameterDirection.Input;
                    prm1.Value = Int32.Parse(parametervalue);
                }

                cmd.Parameters.Add(prm1);
                //return cmd.ExecuteNonQuery();
                return await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                return -1;
                throw ;
            }
            finally
            {   // Clean up
                cmd.Dispose();
                prm1.Dispose();
            }


        }

        public void Close()
        {
            try
            {
                connection.Close();
            }
            catch (Exception)
            {
                throw ;
            }

        }

        public OracleConnection Connection   // property
        {
            get { return connection; }   // get method
            set { connection = value; }  // set method
        }

    }
}
