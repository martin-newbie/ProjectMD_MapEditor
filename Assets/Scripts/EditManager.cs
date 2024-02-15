using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class EditManager : MonoBehaviour
{
    ChapterData chapterData;
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
        firstWave.unitsData.Add(firstUnit);
        stageData.wavesData.Add(firstWave);

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

        string resourcePath = "Chapter/" + chapter.ToString();
        string dataPath = Application.dataPath + "/Resources/"  + resourcePath + ".txt";
        if (File.Exists(dataPath))
        {
            var textAsset = Resources.Load<TextAsset>(resourcePath);
            chapterData = JsonUtility.FromJson<ChapterData>(textAsset.text);
            stageData = chapterData.stagesData[stageIdx];
        }
        else
        {
            chapterData = new ChapterData();
            chapterData.stagesData[stageIdx] = stageData;
            File.WriteAllText(dataPath, JsonUtility.ToJson(chapterData));
        }

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
        if (stageData.wavesData.Count == 10) return;

        var waveData = new WaveData();
        waveData.unitsData.Add(new UnitData());
        stageData.wavesData.Add(waveData);
        UpdateWaveList();
    }

    public void DestroyCurWave()
    {
        stageData.wavesData.RemoveAt(curShowWave);
        curShowWave = 0;
        UpdateWaveList();
        UpdateUnitPanel();
    }

    public void UpdateWaveList()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (i < stageData.wavesData.Count)
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
        if (stageData.wavesData[curShowWave].unitsData.Count == 8) return;

        UnitData unit = new UnitData();
        stageData.wavesData[curShowWave].unitsData.Add(unit);
        UpdateUnitPanel();
    }

    public void RemoveUnitAt(int idx)
    {
        stageData.wavesData[curShowWave].unitsData.RemoveAt(idx);
        UpdateUnitPanel();
    }

    public void UpdateUnitIndex(int buttonIdx, int unitIdx)
    {
        stageData.wavesData[curShowWave].unitsData[buttonIdx].index = unitIdx;
    }

    public void UpdateUnitType(int buttonIdx, int typeIdx)
    {
        stageData.wavesData[curShowWave].unitsData[buttonIdx].unit_type = typeIdx;
    }

    public void UpdateUnitPanel()
    {
        var list = stageData.wavesData[curShowWave].unitsData;
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

        stageData.chapter_index = chapter;
        stageData.stage_index = stageIdx;
        stageData.stage_level = stageLevel;

        chapterData.stagesData[stageIdx] = stageData;
        string chapterJson = JsonUtility.ToJson(chapterData);
        string jsonData = JsonUtility.ToJson(stageData);

        string resourcePath = "Chapter/" + chapter.ToString();
        string dataPath = Application.dataPath + "/Resources/" + resourcePath + ".txt";
        File.WriteAllText(dataPath, chapterJson);
        GUIUtility.systemCopyBuffer = jsonData;
    }
}
