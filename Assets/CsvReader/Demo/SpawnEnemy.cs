using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Zitga.CsvTools.Tutorials
{
    public class SpawnEnemyExample : ScriptableObject
    {
        [System.Serializable]
        public class Bonus
        {
            public int typeId, number, interval, bonusHp, bonusMoveSpeed, bonusAtk;
        }
    
        [System.Serializable]
        public class SpawnEnemy
        {
            public int timeStart;
            public int timeEnd;
            public int[] zoneId;
            public Bonus[] bonuses;
        }
    
        public SpawnEnemy[] spawnEnemies;
    }
    
    #if UNITY_EDITOR
    public class SpawnEnemyPostprocessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (string str in importedAssets)
            {
                if (str.IndexOf("/SpawnEnemy.csv") != -1)
                {
                    TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                    string assetfile = str.Replace(".csv", ".asset");
                    SpawnEnemyExample gm = AssetDatabase.LoadAssetAtPath<SpawnEnemyExample>(assetfile);
                    if (gm == null)
                    {
                        gm = ScriptableObject.CreateInstance<SpawnEnemyExample>();
                        AssetDatabase.CreateAsset(gm, assetfile);
                    }
    
                    gm.spawnEnemies = CsvReader.Deserialize<SpawnEnemyExample.SpawnEnemy>(data.text);
    
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
}
