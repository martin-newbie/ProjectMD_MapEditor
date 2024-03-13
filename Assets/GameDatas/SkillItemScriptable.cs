using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillItem", menuName = "GameDatas/SkillItem", order = int.MinValue)]
public class SkillItemScriptable : DataScriptable
{
    [SerializeField]
    public SkillItemData[] skillItemDatas;
}

[System.Serializable]
public class SkillItemData
{
    public int coin;
    public ItemData[] items;
}

[System.Serializable]
public class ItemData
{
    public int idx;
    public int count;
}