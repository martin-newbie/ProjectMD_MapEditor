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
    public List<UnitData> unitsData;

    public WaveData()
    {
        unitsData = new List<UnitData>();
    }
}

[Serializable]
public class StageData
{
    public int chapter_index;
    public int stage_index;
    public int stage_level;

    public List<WaveData> wavesData;

    public StageData()
    {
        wavesData = new List<WaveData>();
    }
}

[Serializable]
public class ChapterData
{
    public int chapter_index;
    public List<StageData> stagesData;

    public ChapterData()
    {
        stagesData = new List<StageData>(new StageData[20]);
    }
}