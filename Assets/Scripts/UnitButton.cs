using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public InputField indexInput;
    public Dropdown typeDropdown;

    EditManager manager;
    int buttonIdx;

    public void InitButton(EditManager _manager, int btnIdx)
    {
        manager = _manager;
        buttonIdx = btnIdx;
    }

    public void UpdateButton(UnitData data)
    {
        if (data == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        indexInput.text = data.index.ToString();
        typeDropdown.value = data.unit_type;
    }

    public void OnIndexValueChanged()
    {
        manager.UpdateUnitIndex(buttonIdx, int.Parse(indexInput.text));
    }

    public void OnTypeValueChanged()
    {
        manager.UpdateUnitType(buttonIdx, typeDropdown.value);
    }

    public void RemoveUnit()
    {
        manager.RemoveUnitAt(buttonIdx);
    }
}
