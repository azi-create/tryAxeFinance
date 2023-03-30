using Amazon.Auth.AccessControlPolicy;
using MongoDataAccess.Models.Enums;


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MongoDataAccess.Models
{
    /// <summary>
    /// Rule class 
    /// </summary>
    public class Rule : BaseEntity
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
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the when.
        /// </summary>
        /// <value>
        /// The when.
        /// </value>
        public Condition When { get; set; }

        /// <summary>
        /// Gets or sets the then.
        /// </summary>
        /// <value>
        /// The then.
        /// </value>
        public List<Action> Then { get; set; }
        public bool Result { get; set; }

        public Rule()
        {

        }
        public Rule( string ischanged , string name, string priority, string description, Condition when, List<Action> then)
        {
            IsChanged = ischanged;
            Name = !string.IsNullOrWhiteSpace(name) ? name : throw new ArgumentNullException(nameof(name));
            Priority = priority;
            Description = description;
            When = when;
            Then = then;
        }
    }
}
