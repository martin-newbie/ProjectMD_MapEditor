using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(DataScriptable), true)]
public class DataScriptableButtons : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DataScriptable item = target as DataScriptable;

        if (GUILayout.Button("Export"))
        {
            item.ExportToJSON();
        }
        if (GUILayout.Button("Upload"))
        {
            
        }
    }
}
#endif


public abstract class DataScriptable : ScriptableObject
{
    public string title;

    public virtual void ExportToJSON()
    {
        string resourcePath = $"Datas/{title}";
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(dataPath, jsonData);
        GUIUtility.systemCopyBuffer = jsonData;
    }
}
