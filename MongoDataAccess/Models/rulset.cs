using MongoDataAccess.Models.VocabularyEntites;
using MongoDataAccess.Models;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;

namespace MongoDataAccess.Models
{
    /// <summary>
    /// Ruleset class
    /// </summary>
    public class Ruleset : BaseEntity
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
        /// Gets or sets the rules.
        /// </summary>
        /// <value>
        /// The rules.
        /// </value>
        public List<Rule> Rules { get; set; }

        public int Priority { get; set; }

        public List<Vocabulary> Outputs { get; set; }

        public Node Node { get; set; }

        public List<Result> Results { get; set; }

        public string OutputType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ruleset"/> class.
        /// </summary>
        public Ruleset()
        {
        }

        public Ruleset(  string name, string description, List<Rule> rules, int priority, List<Vocabulary> outputs, Node node, List<Result> results, string outputType, string isChanged)
        {
            Name = name;
            Description = description;
            Rules = rules;
            Priority = priority;
            Outputs = outputs;
            Node = node;
            Results = results;
            OutputType = outputType;
            IsChanged = isChanged;
        }

        public Ruleset(List<Rule> rules)
        {
            Rules = rules;
        }

    }
}
