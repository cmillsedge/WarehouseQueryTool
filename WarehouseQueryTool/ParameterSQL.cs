using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseQueryTool
{
    public class ParameterSQL
    {
        public string PivotClause { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Datatype{ get; set; }
        public ParameterSQL(Parameter param)
        {
            Name = param.Name;
            Level = param.ContextLevel;
            Datatype = param.DataType;
            GetPivotSQL();

        }

        public ParameterSQL()
        {

        }


        public void GetPivotSQL ()
        {

            switch (Datatype)
            {
                case "n": //number
                    PivotClause = "MAX(DECODE(c.name, '" + Name + "', a.display_value, NULL)) as " + (char)34 + Name + (char)34 + " ,";
                    break;
                case "d": //dictionary
                    PivotClause = "MAX(DECODE(c.name, '" + Name  + "', a.text_value, NULL)) as " + (char)34 + Name + (char)34 + " ,";
                    break;
                case "s": //text
                    PivotClause = "MAX(DECODE(c.name, '" + Name + "', a.text_value, NULL)) as " + (char)34 + Name + (char)34 + " ,";
                    break;
                case "t": //datetime
                    PivotClause = "MAX(DECODE(c.name, '" + Name + "', a.date_value, NULL)) as " + (char)34 + Name + (char)34 + " ,";
                    break;
                default:
                    PivotClause = "MAX(DECODE(c.name, '" + Name + "', a.text_value, NULL)) as " + (char)34 + Name + (char)34 + " ,";
                    break;
            }
        }
    }
}
