using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models
{
    public class ItemSet
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime UpdatedDate { get; set; }
        public ItemSet()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
        public ItemSet(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Sets the updated date.
        /// </summary>
        public virtual void SetUpdatedDate()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}
