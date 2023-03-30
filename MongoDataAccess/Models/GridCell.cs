using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class GridCell
    {
        /// <summary>
        /// Gets or sets the column name.
        /// </summary>
        public string Name { get; set; }

        public string Value { get; set; }

        public string Source { get; set; }

        public string Type { get; set; }

        public string Bold { get; set; }

        public string Italic { get; set; }

        public string Underline { get; set; }

        public string Color { get; set; }
    }
}
