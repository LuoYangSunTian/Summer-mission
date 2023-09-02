using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Gameobject.Inventory;
public class OptionUI : MonoBehaviour
{
    public TextMeshProUGUI optionText;//当前选择显示的内容
    private Button thisButton;

    private DialoguePiece currentPiece;//当前选择对应的Piece
    private string nextPieceId;//下一句对话的ID
    private bool takeTask;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClick);//将按钮点按的方法挂载到按钮上


    }
    public void UpdateOption(DialoguePiece piece, DialogueOption option)//更新选择的内容
    {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceId = option.targerID;
        takeTask = option.takeTask;
    }

    public void OnOptionClick()//按钮点按的方法
    {
        if (currentPiece.task != null)
        {
            var newTask = new TaskManager.Task//将可以接的任务的类型转化为已经接了的任务类型
            {
                taskData = Instantiate(currentPiece.task)//初始化的时候给taskdata赋值
            };
            if (takeTask)
            {
                //添加到任务列表
                //判断任务是否已经存在
                if (TaskManager.Instance.HaveTask(newTask.taskData))
                {

                    //判断是否完成\
                    if (TaskManager.Instance.GetTask(newTask.taskData).IsComplete)
                    {
                        newTask.taskData.GiveRewards();
                        TaskManager.Instance.GetTask(newTask.taskData).IsFinished = true;

                    }
                }
                else
                {
                    //接受任务
                    TaskManager.Instance.tasks.Add(newTask);//将接取到的任务添加到任务列表中
                    TaskManager.Instance.GetTask(newTask.taskData).IsStarted = true;
                    //检查背包中是否存在任务的物品
                    foreach (var requireItem in newTask.taskData.RequireTargetName())
                    {
                        InventoryManager.Instance.CheckTaskItemInBag(requireItem);
                    }
                }
            }
        }
        if (nextPieceId == "")
        {
            DialogueUI.Instance.dialogue.SetActive(false);
            DialogueUI.Instance.optionPanel.gameObject.SetActive(false);
            return;
        }
        else
        {
            DialogueUI.Instance.UpdateCurrentDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextPieceId]);
        }
    }

}
