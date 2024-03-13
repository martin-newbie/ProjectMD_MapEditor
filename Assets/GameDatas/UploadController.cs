using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class UploadController
{
    public static string portURL = "http://ksmin.iptime.org:8081/rest/";
    public static string testURL = "http://localhost:8083/rest/";

    public static async void UploadData(string title, string data, bool useAsLocalhost = false)
    {
        var sendData = new SendData(title, data);
        var form = new WWWForm();
        var url = (useAsLocalhost ? testURL : portURL) + "data/upload-data";
        form.AddField("input_data", JsonUtility.ToJson(sendData));

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            await request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
        }
    }
}


public class SendData
{
    public string title;
    public string data;

    public SendData(string title, string data)
    {
        this.title = title;
        this.data = data;
    }
}