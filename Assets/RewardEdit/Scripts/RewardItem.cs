using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(RewardItem), true)]
public class DataBaseLoadButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RewardItem item = target as RewardItem;
        if (GUILayout.Button("Export"))
        {
            item.ExportToScript();
        }
    }
}
#endif

[CreateAssetMenu(fileName = "RewardItem", menuName = "RewardEditor/RewardItem", order = int.MinValue), System.Serializable]
public class RewardItem : ScriptableObject
{
    public int chapter;
    public RewardData[] datas;

    public void ExportToScript()
    {
        string resourcePath = "Reward/" + chapter.ToString();
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(dataPath, jsonData);
        GUIUtility.systemCopyBuffer = jsonData;
    }
}
