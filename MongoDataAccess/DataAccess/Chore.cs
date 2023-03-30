using MongoDataAccess.Models;
using MongoDB.Driver;
using MongoDataAccess.Models.Enums;
using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using JsonDiffPatchDotNet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Xml.Linq;
using System.Text.Json.Nodes;
using System.Xml;
using SharpCompress.Compressors.Xz;
using System.ComponentModel;
using System.IO;
using System.Reflection.Metadata;

namespace MongoDataAccess.DataAccess;


public class Chore
{
    public  JToken AddChangedAttribute(JToken original, JToken modified)
    {
        var originalObject = original.ToObject<JObject>();
        var modifiedObject = modified.ToObject<JObject>();
        AddChangedAttributeRecursive(originalObject, modifiedObject);
        string jsonStringOne = JsonConvert.SerializeObject(originalObject);
        

        JToken jTokenOne = JToken.Parse(jsonStringOne);
        return jTokenOne;
    }

    private  void AddChangedAttributeRecursive(JObject originalObject, JObject modifiedObject)
    {
        foreach (var property in modifiedObject.Properties())
        {
            
            var originalValue = originalObject[property.Name];
            var modifiedValue = modifiedObject[property.Name];

            if (originalValue != null && modifiedValue != null)
            {
                if (modifiedValue.Type == JTokenType.Object)
                {
         
                    AddChangedAttributeRecursive((JObject)originalValue, (JObject)modifiedValue);
                }
                else if (modifiedValue.Type == JTokenType.Array)
                {
                    var originalArray = (JArray)originalValue;
                    var modifiedArray = (JArray)modifiedValue;

                    if (originalArray.Count == modifiedArray.Count)
                    {
                        for (int i = 0; i < originalArray.Count; i++)
                        {
                            if (modifiedArray[i].Type == JTokenType.Object)
                            {
                                AddChangedAttributeRecursive((JObject)originalArray[i], (JObject)modifiedArray[i]);
                            }
                        }
                    }
                }
                else if (!JToken.DeepEquals(originalValue, modifiedValue))
                {




                    if (originalValue.Parent.Parent["changed"].HasValues == false)
                     
                    {
                        originalValue.Parent.Parent["changed"] = true;
                    }


                }
                else if (JToken.DeepEquals(originalValue, modifiedValue))
                {
                    if (originalValue.Parent.Parent.SelectToken("changed") == null)
                    {
                        originalValue.Parent.Parent["changed"] = false;
                    }
                }
            }
            
        }
    }








































}






