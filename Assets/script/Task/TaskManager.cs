using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;//使用Linq的命名空间，可以使用在数组和列表中循环查找目标

public class TaskManager : Singleton<TaskManager>
{

    private void Start()
    {
        foreach (var task in tasks)//循环所有的任务查找是否存在名字和传入名字相同的任务
        {
            if (task.IsFinished)
                continue;
            task.taskData.CheckQuestProgress();
        }
    }
    [System.Serializable]
    public class Task//已经接了的任务
    {
        public TaskData_SO taskData;
        public bool IsStarted { get { return taskData.isStarted; } set { taskData.isStarted = value; } }
        public bool IsComplete { get { return taskData.isComplete; } set { taskData.isComplete = value; } }
        public bool IsFinished { get { return taskData.isFinished; } set { taskData.isFinished = value; } }
    }

    public List<Task> tasks = new List<Task>();

    public bool HaveTask(TaskData_SO data)
    {
        if (data != null)
        {
            return tasks.Any(q => q.taskData.taskName == data.taskName);//通过Linq的函数在遍历tasks查看里面是否存在名字与传入的任务名字相同的任务
        }
        else
            return false;
    }

    public Task GetTask(TaskData_SO data)//从Task列表中根据传入的data的名字查找对应的值
    {
        return tasks.Find(q => q.taskData.taskName == data.taskName);
    }

    //敌人死亡，拾取物品，使用物品调用
    public void UpdateTaskProgress(string requireName, int amount)//进行任务检测
    {
        foreach (var task in tasks)//循环所有的任务查找是否存在名字和传入名字相同的任务
        {
            if (task.IsFinished)
                continue;
            var matchTask = task.taskData.taskRequires.Find(r => r.name == requireName);//查找任务的要求中是否存在与传入名字相同的require
            if (matchTask != null)
                matchTask.currentAmount += amount;
            task.taskData.CheckQuestProgress();
        }
    }
}
