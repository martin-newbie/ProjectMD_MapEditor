using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TierData", menuName = "GameDatas/TierData", order = int.MinValue)]
public class TierUpgradeScriptable : DataScriptable
{
    public TierData[] datas;
}

[System.Serializable]
public class TierData
{
    public int item_require;
    public int coin_require;
}