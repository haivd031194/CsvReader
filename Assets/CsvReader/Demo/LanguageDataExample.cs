using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zitga.CSVSerializer.Dictionary;

namespace Zitga.CsvTools.Demo
{
    public class LanguageDataExample : ScriptableObject
    {
        public StringStringDictionary data;
    }

#if UNITY_EDITOR
    public class LanguagePostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string str in importedAssets)
            {
                if (str.IndexOf("/language.csv", StringComparison.Ordinal) != -1)
                {
                    TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                    string assetFile = str.Replace(".csv", ".asset");
                    LanguageDataExample gm = AssetDatabase.LoadAssetAtPath<LanguageDataExample>(assetFile);
                    if (gm == null)
                    {
                        gm = ScriptableObject.CreateInstance<LanguageDataExample>();
                        AssetDatabase.CreateAsset(gm, assetFile);
                    }
                
                    var rows = CsvReader.Deserialize<RowData>(data.text, '~');

                    gm.data.Clear();
                    foreach (var row in rows)
                    {
                        gm.data.Add(row.key, row.value);
                    }
                
                    EditorUtility.SetDirty(gm);
                    AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                    Debug.Log("Reimport Asset: " + str);
#endif
                }
            }
        }
        
        public class RowData
        {
            public string key;
            public string value;
        }
    }
#endif
}