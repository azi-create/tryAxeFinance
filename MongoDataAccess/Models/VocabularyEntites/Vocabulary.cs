using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDataAccess.Models;

namespace MongoDataAccess.Models.VocabularyEntites
{
    public class Vocabulary : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string IsChanged { get; set; }

        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }
        /// <summary>
        /// Gets or sets the range values
        /// </summary>
        /// <value>
        /// The range values.
        /// </value>
        public List<string> RangeValues { get; set; }
        /// <summary>
        /// Gets or sets the grid
        /// </summary>
        /// <value>
        /// The grid
        /// </value>
        public GridVocabulary Grid { get; set; }

        public Vocabulary()
        {
        }
        public string LogVocab()
        {
            string vocabString = "Description:\"" + Description + "\",Type:\"" + Type + "\",Source:\"" + Source + "\",Value:\"" + Value + "\"";
            return vocabString;
        }
    }
}
