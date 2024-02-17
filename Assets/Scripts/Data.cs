using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class UnitData
{
    public int index;
    public int level;
    public int unit_type; // 0: native, 1: elite, 2: boss
}

[Serializable]
public class WaveData
{
    public List<UnitData> unitDatas;

    public WaveData()
    {
        unitDatas = new List<UnitData>();
    }
}

[Serializable]
public class StageData
{
    public int chapterIndex;
    public int stageIndex;
    public int stageLevel;

    public List<WaveData> waveDatas;

    public StageData()
    {
        waveDatas = new List<WaveData>();
    }
}

[Serializable]
public class ChapterData
{
    public int chapterIndex;
    public List<StageData> stageDatas;

    public ChapterData()
    {
        stageDatas = new List<StageData>(new StageData[20]);
    }
}