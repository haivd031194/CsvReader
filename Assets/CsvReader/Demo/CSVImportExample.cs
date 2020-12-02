using UnityEngine;
using Zitga.CsvTools;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CSVImportExample : ScriptableObject
{
    [System.Serializable]
    public class Sample
    {
        public int year;
        public string make;
        public string model;
        public string description;
        public float price;
    }
    public Sample[] m_Sample;
}

#if UNITY_EDITOR
public class CSVImportExamplePostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf("/sample.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                CSVImportExample gm = AssetDatabase.LoadAssetAtPath<CSVImportExample>(assetfile);
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance<CSVImportExample>();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                gm.m_Sample = CsvReader.Deserialize<CSVImportExample.Sample>(data.text);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }
            if (str.IndexOf("/f1ranking2018.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                RankingData gm = AssetDatabase.LoadAssetAtPath<RankingData>(assetfile);
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance<RankingData>();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                var items = CsvReader.Deserialize<RankingData.Item>(data.text);

                foreach (var item in items)
                {
                    gm.itemDict.Add(item.ranking, item);
                }
                
                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }
            
            if (str.IndexOf("/const.csv") != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetfile = str.Replace(".csv", ".asset");
                ConstData gm = AssetDatabase.LoadAssetAtPath<ConstData>(assetfile);
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance<ConstData>();
                    AssetDatabase.CreateAsset(gm, assetfile);
                }

                ConstData readdata = CsvReader.DeserializeIdValue<ConstData>(data.text);
                EditorUtility.CopySerialized(readdata, gm);

                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
#if DEBUG_LOG || UNITY_EDITOR
                Debug.Log("Reimported Asset: " + str);
#endif
            }
        }
    }
}
#endif