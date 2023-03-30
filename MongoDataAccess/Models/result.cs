using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class Result
    {
        public string IsChanged { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
        //public string V1 { get; }
        //public string V2 { get; }

        public Result()
        {

        }

        //public Result(string v1, string v2, string v)
        //{
        //    V1 = v1;
        //    V2 = v2;
        //}

        public string LogResult()
        {
            return "Name:\"" + Name + "\",Value:\"" + Value + "\"";

        }
    }
}
