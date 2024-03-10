using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(CommonSkillItemScriptable), true)]
public class CommonSkillLoadButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CommonSkillItemScriptable item = target as CommonSkillItemScriptable;
        if (GUILayout.Button("Export"))
        {
            item.ExportToData();
        }
    }
}
#endif


[CreateAssetMenu(fileName = "commonSkillItem", menuName = "skillData/commonSkill", order = int.MinValue)]
public class CommonSkillItemScriptable : ScriptableObject
{
    [SerializeField]
    public SkillItemData[] skillItemDatas;

    public void ExportToData()
    {
        string resourcePath = "SkillItem/CommonSkill";
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(dataPath, jsonData);
        GUIUtility.systemCopyBuffer = jsonData;
    }
}

[System.Serializable]
public class SkillItemData
{
    public ItemData[] items;
}

[System.Serializable]
public class ItemData
{
    public int idx;
    public int count;
}