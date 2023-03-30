using MongoDataAccess.DataAccess;
using MongoDB.Driver;
using mongodb_try;
using MongoDataAccess.Models;
using MongoDB.Bson;
using System.Xml.Linq;
using MongoDataAccess.Models.Enums;
using System.Threading.Channels;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonDiffPatchDotNet;
using System.Collections;
using System.Reflection.Metadata;
using Json.Comparer;
using System.Xml;
using Amazon.Runtime.Internal.Transform;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices.JavaScript;
using MongoDataAccess.Models.VocabularyEntites;

string connectionString = "mongodb://localhost:27017";
string databaseName = "friendsList";
//string collectionName = "people";

var client = new MongoClient(connectionString);
var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<PersonMode>(collectionName);





ChoreDataAccess mth = new ChoreDataAccess();
Chore mdh = new Chore();


//var changes = await mth.GetAllChanges();

//foreach (var result in changes.ToList())
//{
//    Console.WriteLine($"{result.Id}: {result.timeOfOperation} {result.operation}");
//}

//var change = new changesModel{ timeOfOperation = "12:02:2023", operation = "get"};

//await mth.CreateChange(change);


//var person = new userModel { Id= "64021379004b4a832b5e2f46",  lastName = "mch iron", name = "mch man" };

//await mth.CreateUser(new userModel() { name = "Rachel", lastName = "green" });

//await mth.UpdateUser(person);
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
outputs.Add(VocabularyOne);
outputs.Add(VocabularyTwo);





var Vocabulary3 = new Vocabulary();
Vocabulary3.Name = "jdid";
var Vocabulary4 = new Vocabulary();
VocabularyTwo.Name = "new";
List<Vocabulary> newout = new List<Vocabulary>();
newout.Add(Vocabulary3);
newout.Add(Vocabulary4);




//el node
var node = new Node();


var rulsetOne = new Ruleset("name rulset 3", "description rulset 2",  rules, 1, outputs, node, listResult, "output type 1","same");

var rulsetTwo = new Ruleset("name rulset 9", "description rulset 9 ", rules, 2, outputs, node, listResult, "output type 1", "same");
List<Ruleset> rulesets = new List<Ruleset>();
rulesets.Add(rulsetOne);
rulesets.Add(rulsetTwo);





var content = new Content(outputs, rulesets);
var newContent = new Content(newout, rulesets);

var policyOne = new Policy( "same" ,"name policy 3", "description policy 3", "owner policy ", "statu policy", "version policy 3",content, listResult);
var policyTwo = new Policy("same", "name policy 4", "description policy 4", "owner policy ", "statu policy", "version policy 2 ", newContent, listResult);



//////bool isEqual = policyOne.Equals(policyTwo);
//////Console.WriteLine(isEqual);

var policies = new List<Policy>();
policies.Add(policyOne);
policies.Add(policyTwo);    

var policySetTwo = new PolicySet("name 2", "description 2", context,  "owner 2" , policies);

//var policySetTOne = new PolicySet("name 1", "description 1", context, "owner 1", policies);


//Console.WriteLine(policyOne.Name);
//await mth.CreatePolicySet(policySetOne);
//await mth.CreatePolicySet(policySetTwo);






//var NewPolicies = await mth.GetAllPolicySet();
var resultOne = mth.GetPolicySetByName("name 1");
var resultTwo = mth.GetPolicySetByName("name 2");


//var resultTwo = mth.GetPolicySetByName("name 3");

string jsonStringOne = JsonConvert.SerializeObject(policyOne);
string jsonStringTwo = JsonConvert.SerializeObject(policyTwo);








JToken jTokenOne = JToken.Parse(jsonStringOne);
JToken jTokenTwo = JToken.Parse(jsonStringTwo);






/////////////////////////////

string originalJson = @"

        {

            'name': 'John',

            'age': 30,

          

            'pets': [

                {

                    'name': 'Fido',

                    'type': 'dog',
                

                },

                {

                    'name': 'Mittens',

                    'type': 'cat',
                    

                },
                {

                    'name': 'mohssen',

                    'type': 'tiger',
                   
                }

            ]

        }";



// JSON modifié

string modifiedJson = @"

        {

            'name': 'Jane',

            'age': 30,

         

            'pets': [

                {

                    'name': 'Fido',

                    'type': 'dog',

                   

                },

                {

                    'name': 'Mittens',

                    'type': 'cat',

                    

                },

                {

                    'name': 'Spot',

                    'type': 'dog',

                    

                }

            ]

        }";












// Convertir les deux JSON en objets JToken

JToken originalToken = JToken.Parse(originalJson);

JToken modifiedToken = JToken.Parse(modifiedJson);



JToken result = mdh.AddChangedAttribute(jTokenOne, jTokenTwo);
Console.WriteLine("result = " + result);






// Trouver les différences entre les deux objets JToken

//JToken diff = mth.FindDiff(originalToken, modifiedToken);
//Console.WriteLine(diff);

// Ajouter l'attribut "changed" à chaque nœud qui a été modifié

//mth.MarkChanges(modifiedToken, diff);



//////// Convertir l'objet JToken modifié en JSON

//////string modifiedJsonWithChanges = modifiedToken.ToString();



//////// Afficher le JSON modifié avec les changements

//////Console.WriteLine(modifiedJsonWithChanges);















































Dictionary<string, object> dictionaryOne = jTokenOne.ToObject<Dictionary<string, object>>();
Dictionary<string, object> dictionaryTwo = jTokenTwo.ToObject<Dictionary<string, object>>();

//mth.Compare(dictionaryOne, dictionaryTwo);

Dictionary<string, object> dictionaryResult = mth.ParcourDics(dictionaryOne, dictionaryTwo);

//mth.Comparer(dictionaryOne, dictionaryTwo);






var jdp = new JsonDiffPatch();
var patch = jdp.Diff(jTokenOne, jTokenTwo);





string json = JsonConvert.SerializeObject(dictionaryResult);
JToken jToken = JToken.Parse(json);
//Console.WriteLine(jToken);











































































































