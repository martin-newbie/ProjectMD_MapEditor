using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ExSkillItemScriptable), true)]
public class ExSkillLoadButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ExSkillItemScriptable item = target as ExSkillItemScriptable;
        if (GUILayout.Button("Export"))
        {
            item.ExportToData();
        }
    }
}
#endif


[CreateAssetMenu(fileName = "exSkillItem", menuName = "skillData/exSkill", order = int.MinValue)]
public class ExSkillItemScriptable : ScriptableObject
{
    [SerializeField]
    public SkillItemData[] skillItemDatas;

    public void ExportToData()
    {
        string resourcePath = "SkillItem/ExSkill";
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(dataPath, jsonData);
        GUIUtility.systemCopyBuffer = jsonData;
    }
}
