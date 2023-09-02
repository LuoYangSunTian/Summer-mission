using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelButton : MonoBehaviour
{
    public LabelType labelType;
    private Button button;
    [Header("界面")]
    public GameObject PlayerInterface;
    public GameObject EquipmentInterface;
    public GameObject MagicInterface;
    public GameObject SaveInterface;
    public GameObject SettingInterface;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchInterface);
    }

    public void SwitchInterface()
    {
        CloseAllInterface();
        switch (labelType)
        {
            case LabelType.Player:
                PlayerInterface.SetActive(true);
                break;
            case LabelType.Equipment:
                EquipmentInterface.SetActive(true);
                break;
            case LabelType.Magic:
                MagicInterface.SetActive(true);
                break;
            case LabelType.Save:
                SaveInterface.SetActive(true);
                break;
            case LabelType.Setting:
                SettingInterface.SetActive(true);
                break;
        }

    }

    public void CloseAllInterface()
    {
        PlayerInterface.SetActive(false);
        EquipmentInterface.SetActive(false);
        MagicInterface.SetActive(false);
        SaveInterface.SetActive(false);
        SettingInterface.SetActive(false);
    }
}
