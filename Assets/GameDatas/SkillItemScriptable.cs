using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(SkillItemScriptable), true)]
public class CommonSkillLoadButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SkillItemScriptable item = target as SkillItemScriptable;
        if (GUILayout.Button("Export"))
        {
            item.ExportToData();
        }
    }
}
#endif


[CreateAssetMenu(fileName = "skillItem", menuName = "skillData/skillItem", order = int.MinValue)]
public class SkillItemScriptable : ScriptableObject
{

    public string title;

    [SerializeField]
    public SkillItemData[] skillItemDatas;

    public void ExportToData()
    {
        string resourcePath = $"SkillItem/{title}";
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(dataPath, jsonData);
        GUIUtility.systemCopyBuffer = jsonData;
    }
}

[System.Serializable]
public class SkillItemData
{
    public int coin;
    public ItemData[] items;
}

[System.Serializable]
public class ItemData
{
    public int idx;
    public int count;
}