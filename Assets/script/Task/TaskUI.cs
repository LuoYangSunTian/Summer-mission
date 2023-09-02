using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Gameobject.Inventory;

public class TaskUI : Singleton<TaskUI>
{
    [Header("组件")]
    public GameObject taskPanel;
    public TextMeshProUGUI title;
    //ItemToolTip
    public Tooltip tooltip;
    bool isOpen;
    [Header("Task Name")]
    public RectTransform taskListTransform;//生成的任务名字按钮挂载到的父物体
    public TaskNameButton taskNameButton;//每一个的任务按钮

    [Header("任务的内容")]
    public TextMeshProUGUI taskContentText;

    [Header("任务完成的条件")]
    public RectTransform requireTransform;//任务完成条件文本挂载到的父物体
    public TaskRequire taskRequire;

    [Header("任务的奖励")]
    public RectTransform rewardTransform;//任务奖励生成后挂载的父物体
    public TaskRewardSoltUI rewardSoltUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOpen = !isOpen;
            taskPanel.SetActive(isOpen);
            taskContentText.text = string.Empty;
            title.text = "";
            //显示任务的列表
            OpenTaskList();
        }
    }

    public void OpenTaskList()
    {
        foreach (Transform item in taskListTransform)//通过父物体循环删除所有任务名字
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in rewardTransform)//循环删除所有的奖励
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in requireTransform)//循环删除所有的任务要求
        {
            Destroy(item.gameObject);
        }

        foreach (var task in TaskManager.Instance.tasks)//将所有玩家接的任务生成在任务列表的UI下方
        {
            var newTask = Instantiate(taskNameButton, taskListTransform);
            newTask.SetupNameButton(task.taskData);//设置每个生成任务列表
            title.text = "任务";
            newTask.taskContentText = taskContentText;//为每个任务的按钮的任务描述组件赋值

        }
    }

    public void SetupRequireList(TaskData_SO taskData)//设置任务的要求
    {
        foreach (Transform item in requireTransform)//循环删除所有的任务要求
        {
            Destroy(item.gameObject);
        }

        foreach (var require in taskData.taskRequires)
        {
            var q = Instantiate(taskRequire, requireTransform);//根据脚本生成脚本所挂载的物体
            if (taskData.isFinished)
                q.SetRequirement(require.name, true);
            else
                q.SetRequirement(require.name, require.requireAmount, require.currentAmount);//设置任务要求
        }
    }

    public void SetupRewardItem(int itemID, int amount)//设置奖励物品
    {
        var item = Instantiate(rewardSoltUI, rewardTransform);
        item.UpdateTaskRewardUI(itemID, amount);
    }
}
