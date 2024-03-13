using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using System;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(ExpDataScriptable), true)]
public class ExpLoadButton : DataScriptableButtons
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ExpDataScriptable item = target as ExpDataScriptable;
        if(GUILayout.Button("Load from sheet"))
        {
            item.LoadFromTable();
        }
    }
}
#endif


[CreateAssetMenu(fileName = "ExpData", menuName = "GameDatas/ExpData", order = int.MinValue)]
public class ExpDataScriptable : DataScriptable
{
    public string gid;
    public string range;
    public int[] exp;

    public async void LoadFromTable() 
    {
        string url = $"https://docs.google.com/spreadsheets/d/1JX4ycY9Jo4fvyBsAu-3HucVeANMYGzhMIDJ0miCZAi0/export?format=tsv&range={range}&gid={gid}";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            await www.SendWebRequest();

            var dataSplit = www.downloadHandler.text.Split('\n');
            exp = new int[dataSplit.Length];
            for (int i = 0; i < dataSplit.Length; i++)
            {
                exp[i] = int.Parse(dataSplit[i]);
            }
        }
    }
}

public class UnityWebRequestAwaiter : INotifyCompletion
{
    private UnityWebRequestAsyncOperation asyncOp;
    private Action continuation;

    public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
    {
        this.asyncOp = asyncOp;
        asyncOp.completed += OnRequestCompleted;
    }

    public bool IsCompleted { get { return asyncOp.isDone; } }

    public void GetResult() { }

    public void OnCompleted(Action continuation)
    {
        this.continuation = continuation;
    }

    private void OnRequestCompleted(AsyncOperation obj)
    {
        continuation();
    }
}

public static class ExtensionMethods
{
    public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
    {
        return new UnityWebRequestAwaiter(asyncOp);
    }
}