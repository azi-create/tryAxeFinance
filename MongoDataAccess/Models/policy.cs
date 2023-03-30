using MongoDataAccess.Models.Enums;
using MongoDataAccess.Models.VocabularyEntites;
using MongoDataAccess.Models;
namespace MongoDataAccess.Models
{
    /// <summary>
    /// Policy Class
    /// </summary>
    /// <seealso cref="MongoDataAccess.Models.BaseEntity" />
    public class Policy : BaseEntity
    {
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
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public string Owner { get; set; }

        public string Status { get; set; }

        public string Version { get; set; }

        public Content Content { get; set; }

        public List<Result> Results { get; set; }

        public Guid TriggeringRuleId { get; set; }

        public Guid TriggeringPolicyId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class.
        /// </summary>
        public Policy()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Policy"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="owner">The owner.</param>
        /// <param name="content">The policy versions.</param>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public Policy(string ischanged ,string name, string description, string owner, string status, string version, Content content, ContextType type, List<Result> results, Guid triggeringRuleId, Guid triggeringPolicyId)
        {
            IsChanged = ischanged;
            Name = name;
            Description = description;
            Owner = owner;
            Content = content;
            Status = status;
            Version = version;
            Results = results;
            TriggeringRuleId = triggeringRuleId;
            TriggeringPolicyId = triggeringPolicyId;
        }

        public Policy(string ischanged, string name, string description, string owner, string status, string version, Content content, List<Result> results)
        {
            IsChanged = ischanged;
            Name = name;
            Description = description;
            Owner = owner;
            Content = content;
            Status = status;
            Version = version;
            Results = results;
        }
    }
}
