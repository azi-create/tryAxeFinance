using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDataAccess.Models.Enums
{
    public enum PolicyActionType
    {
        CreatePolicySet = 0,
        UpdatePolicySet = 1,
        DeletePolicySet = 2,
        DuplicatePolicySet = 3,

        CreatePolicy = 4,
        UpdatePolicy = 5,
        DeletePolicy = 6,
        DuplicatePolicy = 7,
        PublishPolicy = 8,

        DuplicateRuleset = 9,
        UpdatePolicyVocabulary = 10
    }
}
