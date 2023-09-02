using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeaponTurn : MonoBehaviour
{
    private player gamePlayer;
    [SerializeField] private Camera mainCamera;
    private void OnEnable()
    {
        gamePlayer = GetComponentInParent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        if ((mouseWorldPos.x - transform.position.x) < 0f)
            transform.localScale = new Vector3(-1f, 1f, 0);
        else
            transform.localScale = new Vector3(1f, 1f, 0);
    }
}
