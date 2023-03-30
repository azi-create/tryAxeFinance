
using System.Collections.Generic;
using MongoDataAccess.Models.VocabularyEntites;


namespace MongoDataAccess.Models
{
    /// <summary>
    /// Version Class 
    /// </summary>
    /// <seealso cref="Domain.Entities.BaseEntity" />
    public class Content
    {


        public List<Vocabulary> Vocabularies { get; set; }
        /// <summary>
        /// Gets or sets the rulesets.
        /// </summary>
        /// <value>
        /// The rulesets.
        /// </value>

        public List<Ruleset> Rulesets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Content"/> class.
        /// </summary>
        public Content()
        {

        }
        public Content(List<Vocabulary> vocabularies, List<Ruleset> rulesets)
        {
            Vocabularies = vocabularies;
            Rulesets = rulesets;
        }
    }
}
