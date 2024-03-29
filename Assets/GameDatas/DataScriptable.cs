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
            UploadController.UploadData(item.title, item.ExportToJSON(), item.uploadAsLocalhost);
        }
    }
}
#endif


public abstract class DataScriptable : ScriptableObject
{
    public bool uploadAsLocalhost = true;
    public string title;

    public virtual string ExportToJSON()
    {
        string resourcePath = $"Datas/{title}";
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        string jsonData = JsonUtility.ToJson(this);
        File.WriteAllText(dataPath, jsonData);
        GUIUtility.systemCopyBuffer = jsonData;
        return jsonData;
    }
}
