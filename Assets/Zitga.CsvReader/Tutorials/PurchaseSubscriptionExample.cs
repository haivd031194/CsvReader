using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Zitga.CsvTools.Tutorials
{
    public class PurchaseSubscriptionExample : ScriptableObject
{

    [Serializable]
    public class Reward
    {
        public int resType;
        public int resId;
        public int resNumber;
        public int resData;
    }

    [Serializable]
    public class ShopGroup
    {
        public int id;
        public string sgProductId;
        public Reward reward;
        public Reward[] instantReward;
        public bool allowSkipVideo;
        public float price;
        public int trialDurationInDays;
    }

    public ShopGroup[] shopGroups;
    
    [Serializable]
    public class ShopGroupDictionary : SerializableDictionary<int, ShopGroup> { }
}

#if UNITY_EDITOR
public class PurchasePostprocessor : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf("/purchase_subscription_pack.csv", StringComparison.Ordinal) != -1)
            {
                TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                string assetFile = str.Replace(".csv", ".asset");
                PurchaseSubscriptionExample gm = AssetDatabase.LoadAssetAtPath<PurchaseSubscriptionExample>(assetFile);
                if (gm == null)
                {
                    gm = ScriptableObject.CreateInstance<PurchaseSubscriptionExample>();
                    AssetDatabase.CreateAsset(gm, assetFile);
                }
                
                gm.shopGroups = CsvReader.Deserialize<PurchaseSubscriptionExample.ShopGroup>(data.text);
                
                EditorUtility.SetDirty(gm);
                AssetDatabase.SaveAssets();
                Debug.Log("Reimport Asset: " + str);
            }
        }
    }
}
#endif
}
