using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]//保证使用该代码时需要DialogueController
public class TaskGiver : MonoBehaviour
{
    DialogueController controller;//对话控制的脚本
    TaskData_SO currentTask;

    public DialogueData_SO startDialogue;//任务开始时的对话
    public DialogueData_SO progressDialogue;//任务过程中的对话
    public DialogueData_SO completeDialogue;//任务条件达成后交任务时的对话
    public DialogueData_SO finishDialogue;//任务已经完成后，不能再接该任务的对话

    #region 获得任务的状态
    public bool IsStarted//判断当前任务是否已经开始
    {
        get
        {
            if (TaskManager.Instance.HaveTask(currentTask))
            {
                return TaskManager.Instance.GetTask(currentTask).IsStarted;
            }
            else
                return false;
        }

    }

    public bool IsComplete
    {
        get
        {
            if (TaskManager.Instance.HaveTask(currentTask))
            {
                return TaskManager.Instance.GetTask(currentTask).IsComplete;
            }
            else
                return false;
        }

    }

    public bool IsFinish
    {
        get
        {
            if (TaskManager.Instance.HaveTask(currentTask))
            {
                return TaskManager.Instance.GetTask(currentTask).IsFinished;
            }
            else
                return false;
        }

    }

    #endregion
    private void Awake()
    {
        controller = GetComponent<DialogueController>();
    }

    private void Start()
    {
        controller.currentData = startDialogue;
        currentTask = controller.currentData.GetTask();
    }

    private void Update()
    {
        //检测任务的状态
        if (IsStarted)
        {
            if (IsComplete)
            {
                controller.currentData = completeDialogue;
            }
            else
            {
                controller.currentData = progressDialogue;
            }
        }
        if (IsFinish)
        {
            controller.currentData = finishDialogue;
        }
    }
}
