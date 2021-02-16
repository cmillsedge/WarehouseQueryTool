using System;
using System.Runtime.Serialization;

namespace WarehouseQueryTool
{
    [Serializable()]    //Set this attribute to all the classes that want to serialize
    public class DBProperties : ISerializable //derive your class from ISerializable
    {

        public string Schema { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string SID { get; set; }


        public DBProperties(string schema, string password, string host, string port, string sid)
        {
            Schema = schema;
            Password = password;
            Host = host;
            Port = port;
            SID = sid;
        }

        //Deserialization constructor.
        public DBProperties(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            Schema = (String)info.GetValue("Schema", typeof(string));
            //null password as this is just to load the form fields up again without the password
            Password = String.Empty;
            Host = (String)info.GetValue("Host", typeof(string));
            Port = (String)info.GetValue("Port", typeof(string));
            SID = (String)info.GetValue("SID", typeof(string));
        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //Serialize class
            info.AddValue("Schema", Schema);
            //don't write the password out
            //info.AddValue("Password", Password);
            info.AddValue("Host", Host);
            info.AddValue("Port", Port);
            info.AddValue("SID", SID);

        }

    }
}
