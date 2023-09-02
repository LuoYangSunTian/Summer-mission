using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TaskNameButton : MonoBehaviour
{
    public TextMeshProUGUI taskNameText;//任务的名字

    public TaskData_SO currentData;//任务的详情

    public TextMeshProUGUI taskContentText;//任务的详细说明

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UpdateTaskContent);
    }

    public void UpdateTaskContent()//更新任务的内容
    {

        taskContentText.text = string.Empty;
        taskContentText.text = currentData.description;//更新任务的描述

        TaskUI.Instance.SetupRequireList(currentData);//更新任务的要求

        foreach (Transform item in TaskUI.Instance.rewardTransform)//删除所有的奖励
        {
            Destroy(item.gameObject);
        }

        foreach (var item in currentData.rewards)//循环生成所有的奖励
        {
            TaskUI.Instance.SetupRewardItem(item.itemID, item.itemAmount);
        }
    }

    public void SetupNameButton(TaskData_SO taskData)//设置任务的按钮
    {
        currentData = taskData;
        if (taskData.isComplete)//如果任务已经达到完成条件
            taskNameText.text = taskData.taskName + "(完成)";
        else
            taskNameText.text = taskData.taskName;
    }
}
