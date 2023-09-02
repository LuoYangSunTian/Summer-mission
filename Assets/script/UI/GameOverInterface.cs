using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverInterface : MonoBehaviour
{
    public string sceneToGO;
    public Vector3 positionToGO;
    private player gamePlayer => GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
    [SerializeField] private GameObject display;

    private void Update()
    {
        if (gamePlayer.currentHp <= 0f)
            display.SetActive(true);
    }

    public void Resurgence()//复活
    {
        if (ShopManager.Instance.money >= 100)
        {
            ShopManager.Instance.money -= 100;
            EventHandler.CallUpdateMoney(ShopManager.Instance.money);
            EventHandler.CallResurgence();
            display.SetActive(false);
        }
    }

    public void Restart()
    {
        display.SetActive(false);
        gamePlayer.currentHp = gamePlayer.maxHp;
        Time.timeScale = 1f;
        EventHandler.CallBeforeSceneUnloadEvent();
        EventHandler.CallTransitionEvent(sceneToGO, positionToGO);
        Invoke("ReGame", 4f);
    }

    public void ReGame()
    {
        EventHandler.CallResurgence();

    }

    public void Close()
    {
        display.SetActive(false);
        Application.Quit();
    }
}
