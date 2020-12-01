using System;
 
using UnityEngine;
using Zitga.CSVSerializer.Dictionary;

// ---------------
//  String => Int
// ---------------
[Serializable]
public class StringIntDictionary : SerializableDictionary<string, int> {}

// ---------------
//  String => String
// ---------------
[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}

