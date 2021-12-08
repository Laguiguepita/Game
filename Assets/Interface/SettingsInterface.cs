using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsInterface : MonoBehaviour
{
    public GameObject Settings;
    public GameObject Interface;

    public void SettingsOn()
    {
        Settings.SetActive(true);
        Interface.SetActive(false);
    }

    public void SettingOff()
    {
        Settings.SetActive(false);
        Interface.SetActive(true);
    }
}
