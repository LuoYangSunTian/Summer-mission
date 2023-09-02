using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagOpen_Close : MonoBehaviour
{

    [SerializeField] private GameObject BagBar;
    // Start is called before the first frame update
    public void SwitchBag()
    {
        if (BagBar.activeSelf)
            BagBar.SetActive(false);
        else
            BagBar.SetActive(true);
    }
}
