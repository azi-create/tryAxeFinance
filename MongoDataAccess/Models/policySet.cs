using MongoDataAccess.Models.Enums;

using System.Collections.Generic;
using System.Xml.Linq;

namespace MongoDataAccess.Models
{

    public class PolicySet : ItemSet
    {
        public List<Policy> Policies { get; set; }

        public ContextType Type { get; set; }
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }
        public PolicySet()
        {

        }
        public PolicySet(string name, string description, ContextType type, string owner, List<Policy> policies)
        {
            Name = name;
            Description = description;
            Policies = policies;
            Type = type;
            Owner = owner;
        }
    }
}
