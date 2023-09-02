using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    [SerializeField] public GameObject up;
    [SerializeField] public GameObject right;
    [SerializeField] public GameObject down;
    [SerializeField] public GameObject left;
    [SerializeField] private GameObject symbol;
    private float startTime;
    [SerializeField] private float IntervalTime;//间隔时间

    private void OnEnable()
    {
        startTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;
        if (startTime >= IntervalTime)
            up.SetActive(true);
        if (startTime >= 2 * IntervalTime)
            right.SetActive(true);
        if (startTime >= 3 * IntervalTime)
            down.SetActive(true);
        if (startTime >= 4 * IntervalTime)
        {
            left.SetActive(true);
            /*            symbol.SetActive(false);*/
        }
    }
}
