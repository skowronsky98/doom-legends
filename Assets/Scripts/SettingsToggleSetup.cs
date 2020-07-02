using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsToggleSetup : MonoBehaviour
{
    private List<Toggle> toggles;
    void Awake()
    {
        toggles = new List<Toggle>();
        GetComponentsInChildren(toggles);
        
        SetupToggles();
    }

    private void SetupToggles()
    {
        if (AudioController.Instance.LoadSettings("MuteMusic"))
            toggles[1].isOn = false;
        else
            toggles[1].isOn = true;
    }

        
}
