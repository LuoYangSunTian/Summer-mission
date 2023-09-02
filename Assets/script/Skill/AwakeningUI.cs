using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameobject.Skill;
public class AwakeningUI : MonoBehaviour
{
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject magicSymbol;

    [SerializeField] private GameObject[] magics;

    private int bookPage = 0;

    [SerializeField] private Animator bookAnim;

    [SerializeField] private GameObject TurnPage;
    private SkillType currentSkillType;
    private void OnEnable()
    {
        EventHandler.UpdateAwakeningUI += OnUpdateAwakeningUI;
    }

    private void OnDisable()
    {
        EventHandler.UpdateAwakeningUI -= OnUpdateAwakeningUI;
    }

    private void Update()
    {
        if (TurnPage.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    private void OnUpdateAwakeningUI()
    {
        AweakeningMagic.Instance.isUse = false;
        AweakeningMagic.Instance.isEnter = false;
        backGround.SetActive(true);
        magicSymbol.SetActive(true);
        Invoke("BeginAWakening", 2.2f);
        Invoke("DisplayDescribe", 4f);

    }
    public void Close()
    {

        backGround.SetActive(false);
        foreach (var magic in magics)
        {
            magic.SetActive(false);
        }
        TurnPage.SetActive(false);
        EventHandler.CallCloseAwakeningCircle();
    }

    //书本开始翻页
    public void BeginAWakening()
    {
        magicSymbol.SetActive(false);
        bookAnim.SetTrigger("beginAwakening");
    }


    //显示描述
    public void DisplayDescribe()
    {
        TurnPage.SetActive(true);
        magics[bookPage].SetActive(true);
    }

    //向前翻页

    public void TurningLast()
    {
        if (bookPage > 0)
        {
            magics[bookPage].SetActive(false);
            bookPage--;
            bookAnim.SetTrigger("TurningLast");
            Invoke("DisplayDescribe", 0.33f);
        }


    }

    //向后翻页
    public void TurningNext()
    {
        if (bookPage < magics.Length - 1)
        {
            magics[bookPage].SetActive(false);
            bookPage++;
            bookAnim.SetTrigger("TurningNext");
            Invoke("DisplayDescribe", 0.33f);
        }
    }

    /// <summary>
    /// 添加火系初阶魔法
    /// </summary>
    public void AddFireFirst()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(101));
    }

    public void AddFireSecond()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(102));
    }

    public void AddFireThird()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(103));
    }


    /// <summary>
    /// 添加光系初阶魔法
    /// </summary>
    public void AddHolyFirst()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(201));
    }

    public void AddHolySecond()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(202));
    }

    public void AddHolyThird()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(203));
    }


    public void AddThunderFirst()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(301));
    }

    public void AddThunderSecond()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(302));
    }

    public void AddThunderThird()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(303));
    }

    public void AddWaterFirst()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(401));
    }

    public void AddWaterSecond()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(402));
    }

    public void AddWaterThird()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(403));
    }

    public void AddIceFirst()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(501));
    }

    public void AddIceSecond()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(502));
    }

    public void AddIceThird()
    {
        SkillManager.Instance.AddSkill(SkillManager.Instance.SearchSkillDetail(503));
    }
}
