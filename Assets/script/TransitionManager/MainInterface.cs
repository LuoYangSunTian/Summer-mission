using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    public void StartGame()
    {
        foreach (var item in items)
        {
            item.SetActive(false);

            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }



    public void CloseGame()
    {
        Application.Quit();
    }

}
