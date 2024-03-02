

[System.Serializable]
public class StageRewardData
{
    public RewardData[] datas;
}

[System.Serializable]
public class RewardData
{
    public float acquire_range;
    public int count_min;
    public int count_max;

    public int type;
    public int idx;
}