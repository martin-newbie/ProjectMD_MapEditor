using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{
    StageData stageData;
    int curShowWave;

    [Header("Stage Data")]
    public InputField chapterInput;
    public InputField stageInput;
    public InputField stageLevelInput;

    [Header("Waves")]
    public Button waveButtonPrefab;
    public Transform waveButtonsLayout;
    List<Button> buttonList;

    [Header("Units")]
    public UnitButton unitButtonPrefab;
    public Transform unitButtonsLayout;
    List<UnitButton> unitButtonlist;

    void Start()
    {
        stageData = new StageData();
        var firstWave = new WaveData();
        var firstUnit = new UnitData();
        firstWave.unitDatas.Add(firstUnit);
        stageData.waveDatas.Add(firstWave);

        int waveAllocCount = 10; // maximum wave count
        buttonList = new List<Button>();
        for (int i = 0; i < waveAllocCount; i++)
        {
            int idx = i;
            var button = Instantiate(waveButtonPrefab, waveButtonsLayout);
            button.onClick.AddListener(() => ShowWave(idx));
            button.GetComponentInChildren<Text>().text = $"Wave-{idx}";
            button.transform.SetSiblingIndex(idx);
            button.gameObject.SetActive(false);
            buttonList.Add(button);
        }
        buttonList[0].gameObject.SetActive(true);

        int unitAllocCount = 8;
        unitButtonlist = new List<UnitButton>();
        for (int i = 0; i < unitAllocCount; i++)
        {
            var button = Instantiate(unitButtonPrefab, unitButtonsLayout);
            button.InitButton(this, i);
            unitButtonlist.Add(button);
        }
        unitButtonlist[0].UpdateButton(firstUnit);

        curShowWave = 0;

        UpdateWaveList();
        UpdateUnitPanel();
    }

    public void LoadTextAsset()
    {
        int chapter = int.Parse(chapterInput.text);
        int stageIdx = int.Parse(stageInput.text);

        var chapterData = GetChapterData(chapter);
        stageData = chapterData.stageDatas[stageIdx];

        curShowWave = 0;
        UpdateUnitPanel();
        UpdateWaveList();
    }

    public void ShowWave(int index)
    {
        curShowWave = index;
        UpdateUnitPanel();
    }

    public void AddNewWave()
    {
        if (stageData.waveDatas.Count == 10) return;

        var waveData = new WaveData();
        waveData.unitDatas.Add(new UnitData());
        stageData.waveDatas.Add(waveData);
        UpdateWaveList();
    }

    public void DestroyCurWave()
    {
        stageData.waveDatas.RemoveAt(curShowWave);
        curShowWave = 0;
        UpdateWaveList();
        UpdateUnitPanel();
    }

    public void UpdateWaveList()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i < stageData.waveDatas.Count)
            {
                buttonList[i].gameObject.SetActive(true);
            }
            else
            {
                buttonList[i].gameObject.SetActive(false);
            }
        }
    }

    public void AddNewUnit()
    {
        if (stageData.waveDatas[curShowWave].unitDatas.Count == 8) return;

        UnitData unit = new UnitData();
        stageData.waveDatas[curShowWave].unitDatas.Add(unit);
        UpdateUnitPanel();
    }

    public void RemoveUnitAt(int idx)
    {
        stageData.waveDatas[curShowWave].unitDatas.RemoveAt(idx);
        UpdateUnitPanel();
    }

    public void UpdateUnitIndex(int buttonIdx, int unitIdx)
    {
        stageData.waveDatas[curShowWave].unitDatas[buttonIdx].index = unitIdx;
    }

    public void UpdateUnitType(int buttonIdx, int typeIdx)
    {
        stageData.waveDatas[curShowWave].unitDatas[buttonIdx].unit_type = typeIdx;
    }

    public void UpdateUnitPanel()
    {
        var list = stageData.waveDatas.Count > 0 ? stageData.waveDatas[curShowWave].unitDatas : new List<UnitData>();
        for (int i = 0; i < unitButtonlist.Count; i++)
        {
            if (i < list.Count)
            {
                unitButtonlist[i].UpdateButton(list[i]);
            }
            else
            {
                unitButtonlist[i].UpdateButton(null);
            }
        }
    }

    public void ExportJSON()
    {

        int chapter = int.Parse(chapterInput.text);
        int stageIdx = int.Parse(stageInput.text);
        int stageLevel = int.Parse(stageLevelInput.text);

        var chapterData = GetChapterData(chapter);

        chapterData.stageDatas[stageIdx].waveDatas = stageData.waveDatas;
        chapterData.stageDatas[stageIdx].stageLevel = stageLevel;

        string chapterJson = JsonUtility.ToJson(chapterData);
        string jsonData = JsonUtility.ToJson(stageData);

        string resourcePath = "Chapter/" + chapter.ToString();
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        File.WriteAllText(dataPath, chapterJson);
        GUIUtility.systemCopyBuffer = jsonData;
    }

    ChapterData GetChapterData(int idx)
    {
        string resourcePath = "Chapter/" + idx.ToString();
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        ChapterData chapter = null;
        if (File.Exists(dataPath))
        {
            var textAsset = Resources.Load<TextAsset>(resourcePath);
            chapter = JsonUtility.FromJson<ChapterData>(textAsset.text);
        }
        else
        {
            chapter = new ChapterData(idx);
            File.WriteAllText(dataPath, JsonUtility.ToJson(chapter));
        }

        return chapter;
    }
}
