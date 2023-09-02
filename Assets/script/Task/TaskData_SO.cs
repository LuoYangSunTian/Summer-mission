using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Gameobject.Inventory;

[CreateAssetMenu(fileName = "New Task", menuName = "Task/Task Data")]
public class TaskData_SO : ScriptableObject//所有的任务
{
    [System.Serializable]
    public class TaskRequire
    {
        public string name;//目标的名字
        public int requireAmount;//需要完成的目标数量
        public int currentAmount;//当前已完成的数量
    }

    public string taskName;//任务的名字
    [TextArea]
    public string description;//任务的描述
    public bool isStarted;//任务的状态为开始
    public bool isComplete;//状态为达成条件
    public bool isFinished;//状态为已经提交

    public List<TaskRequire> taskRequires = new List<TaskRequire>();//完成任务所需要的东西

    public List<InventoryItem> rewards = new List<InventoryItem>();//任务的奖励



    public void CheckQuestProgress()//检查任务是否完成
    {
        var finishRequires = taskRequires.Where(r => r.requireAmount <= r.currentAmount);//返回一个包含所有满足currentAmount >= requireAmount的无序列表不能用for循环遍历，只能用foreach
        isComplete = finishRequires.Count() == taskRequires.Count;//如果当前完成的任务的数量与本来的需求数量相等的话，表示任务已经完成
    }

    //当前任务中需要 收集、消灭的目标的名字列表
    public List<string> RequireTargetName()//返回一个string 类型的列表
    {
        List<string> targetNameList = new List<string>();

        foreach (var require in taskRequires)
        {
            targetNameList.Add(require.name);
        }
        return targetNameList;
    }

    //给奖励
    public void GiveRewards()
    {
        foreach (var reward in rewards)
        {
            InventoryManager.Instance.AddItemByID(reward.itemID, reward.itemAmount);
        }
    }
}
