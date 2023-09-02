using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusUI : MonoBehaviour
{
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    [SerializeField] private TextMeshProUGUI currentHp;
    [SerializeField] private TextMeshProUGUI maxHp;
    [SerializeField] private TextMeshProUGUI currentMp;
    [SerializeField] private TextMeshProUGUI maxMp;
    [SerializeField] private Image Hp;
    [SerializeField] private Image Mp;
    public float currentHpNum = 10;
    public float maxHpNum = 10;
    public float currentMpNum = 20;
    public float maxMpNum = 20;

    private void OnEnable()
    {
        EventHandler.UpdateStatus += OnUpdateStatus;
    }
    private void OnDisable()
    {
        EventHandler.UpdateStatus -= OnUpdateStatus;
    }
    private void OnUpdateStatus(float currentHp, float maxHp, float currentMp, float maxMp)
    {

        currentHpNum = currentHp;
        maxHpNum = maxHp;
        currentMpNum = currentMp;
        maxMpNum = maxMp;
    }
    private void Start()
    {
        Hp.fillAmount = currentHpNum / maxHpNum;
        EventHandler.CallUpdateStatus(gamePlayer.currentHp, gamePlayer.maxHp, gamePlayer.currentMp, gamePlayer.maxMp);
    }
    // Update is called once per frame
    void Update()
    {
        Hp.fillAmount = currentHpNum / maxHpNum;
        Mp.fillAmount = currentMpNum / maxMpNum;
        currentHp.text = currentHpNum.ToString();
        maxHp.text = maxHpNum.ToString();
        currentMp.text = currentMpNum.ToString();
        maxMp.text = maxMpNum.ToString();
    }
}
