using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TaskRequire : MonoBehaviour
{
    private TextMeshProUGUI requireName;

    private TextMeshProUGUI progressNumber;
    private void Awake()
    {
        requireName = GetComponent<TextMeshProUGUI>();
        progressNumber = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetRequirement(string name, int amount, int currentAmount)//设置任务的条件
    {
        requireName.text = name;
        progressNumber.text = currentAmount.ToString() + " / " + amount.ToString();
    }

    public void SetRequirement(string name, bool isFinished)
    {
        if (isFinished)
        {
            requireName.text = name;
            progressNumber.text = "完成";
            requireName.color = Color.gray;
            progressNumber.color = Color.gray;
        }
    }
}
