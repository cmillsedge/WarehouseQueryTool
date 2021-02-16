using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WarehouseQueryTool
{
    class DBSerializer
    {
        public static void SerializeDB(DBProperties database, string filename)
        {

            try
            {
                string fullFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
                //Class to serialize connection information other than password to allow retention of last log on info
                Stream stream = File.Open(fullFileName, FileMode.Create);
                BinaryFormatter bformatter = new BinaryFormatter();

                Console.WriteLine("Writing DB Information");
                bformatter.Serialize(stream, database);
                stream.Close();
            }
            catch
            {
                throw;
            }
        }

        public static DBProperties DeSerializeDB(string filename)
        {
            DBProperties database;
            try
            {
                string fullFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), filename);
                //Open the file written above and read values from it.
                Stream stream = File.Open(fullFileName, FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                database = (DBProperties)bformatter.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                throw;
            }
            return database;
        }
    }
}
