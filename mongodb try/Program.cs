using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using MongoDataAccess.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MongoDataAccess.Models.VocabularyEntites;
string connectionString = "mongodb://localhost:27017";
string databaseName = "friendsList";







Chore mdh = new Chore();


var context = new ContextType();

var resultsOne = new Result();
resultsOne.Name = "My Result  5";
resultsOne.Value = "189";

var resultsTwo = new Result();
resultsTwo.Name = "My Result 4";
resultsTwo.Value = "811";
List<Result> listResult = new List<Result>();
listResult.Add(resultsOne);
listResult.Add(resultsTwo);



//El rulset
var ruleOne = new Rule();
var ruleTwo = new Rule();
List<Rule> rules = new List<Rule>();
rules.Add(ruleOne);
rules.Add(ruleTwo);

// El vocabulary
var VocabularyOne = new Vocabulary();
VocabularyOne.Name = "vocab1";
var VocabularyTwo = new Vocabulary();
VocabularyTwo.Name = "vocab2";

List<Vocabulary> outputs = new List<Vocabulary>();



var Vocabulary0 = new Vocabulary();
Vocabulary0.Name = "number 0";


var Vocabulary7 = new Vocabulary();
Vocabulary7.Name = "number 7";





var Vocabulary3 = new Vocabulary();
Vocabulary3.Name = "number 3";
var Vocabulary4 = new Vocabulary();
Vocabulary4.Name = "number 4";
var Vocabulary5 = new Vocabulary();
Vocabulary5.Name = "number 5";
var Vocabulary6 = new Vocabulary();
Vocabulary6.Name = "number 6";
List<Vocabulary> newout = new List<Vocabulary>();
newout.Add(Vocabulary7);
newout.Add(Vocabulary4);
newout.Add(Vocabulary5);
newout.Add(Vocabulary6);


outputs.Add(Vocabulary7);
outputs.Add(Vocabulary3);
outputs.Add(Vocabulary4);
outputs.Add(Vocabulary0);



var node = new Node();


var rulsetOne = new Ruleset("name rulset ", "description rulset 2",  rules, 1, outputs, node, listResult, "output type 1");
var rulsetTwo = new Ruleset("name rulset 2", "description rulset 2 ", rules, 2, outputs, node, listResult, "output type 2");
var rulsetThree = new Ruleset("name rulset 3", "description rulset 3 ", rules, 3, outputs, node, listResult, "output type 3");
var rulsetFour = new Ruleset("name rulset 4", "description rulset 4", rules, 4, outputs, node, listResult, "output type 4");


List<Ruleset> rulesets = new List<Ruleset>();
rulesets.Add(rulsetOne);
rulesets.Add(rulsetTwo);
rulesets.Add(rulsetFour);

List<Ruleset> newRulesets = new List<Ruleset>();
newRulesets.Add(rulsetOne);
newRulesets.Add(rulsetThree);




var content = new Content(outputs, rulesets);
var newContent = new Content(newout, newRulesets);

var policyOne = new Policy("name policy 3", "description policy 3", "owner policy ", "statu policy", "version policy 3",content, listResult);
var policyTwo = new Policy("name policy 4", "description policy 4", "owner policy ", "statu policy", "version policy 2 ", newContent, listResult);



string jsonStringOne = JsonConvert.SerializeObject(policyOne);
string jsonStringTwo = JsonConvert.SerializeObject(policyTwo);


JToken jTokenOne = JToken.Parse(jsonStringOne);
JToken jTokenTwo = JToken.Parse(jsonStringTwo);




(JToken Result1, JToken Result2) = mdh.AddChangedAttribute(jTokenOne, jTokenTwo);

Console.WriteLine("result original = " + Result1);
//Console.WriteLine("//////////////");
//Console.WriteLine("result modif  = " + Result2);


























































































