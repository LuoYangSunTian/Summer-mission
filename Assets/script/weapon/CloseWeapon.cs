using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseWeapon : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject attackDisplay;
    [SerializeField] public float attackNum;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !InteractWithUI())
            anim.SetBool("isAttack", true);
    }

    public void AttackDisplay()
    {
        attackDisplay.SetActive(true);
    }

    public void CloseDisplay()
    {
        attackDisplay.SetActive(false);
        anim.SetBool("isAttack", false);
    }

    /// <summary>
    /// 判断鼠标是否与UI有互动
    /// </summary>
    /// <returns></returns>
    private bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        else
            return false;
    }
}
