using MongoDataAccess.Models;
using MongoDataAccess.Models.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDataAccess.Models.VocabularyEntites;


namespace MongoDataAccess.Models
{
    public class ActionHandSide
    {
        public string Type { get; set; }

        public string Value { get; set; }

        public Grid Grid { get; set; }

        public ActionHandSide()
        {

        }

        public ActionHandSide(string type, string value)
        {
            Type = type;
            Value = value;
        }
        public string LogActionHandside()
        {
            var _type = "";
            var _value = "";
            if (!String.IsNullOrEmpty(Type))
            {
                _type = Type;
            }
            if (!String.IsNullOrEmpty(Value))
            {
                _value = Value;
            }
            return "Type:\"" + _type + "\",Value:\"" + _value + "\",Grid:\"" + Grid + "\"";
        }
    }
}
