using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassManager : MonoBehaviour
{

    [SerializeField] private List<GameObject> enemyManager = new List<GameObject>();
    [SerializeField] private GameObject transfer;
    [SerializeField] private bool isBoos;
    public GameObject passDoor;
    public GameObject AllEnemy;
    // Update is called once per frame
    void Update()
    {
        CheckEnemyAmount();
        if (enemyManager.Count == 0)
        {
            //开门
            AndioManager.Instance.SwitchBackMusic("common");
            passDoor.SetActive(false);
            gameObject.SetActive(false);
            if (isBoos)
                transfer.SetActive(true);
        }
    }

    public void CheckEnemyAmount()
    {
        for (int x = 0; x < enemyManager.Count; x++)
        {
            if (enemyManager[x] == null)
            {
                enemyManager.Remove(enemyManager[x]);
            }
        }
    }


}
