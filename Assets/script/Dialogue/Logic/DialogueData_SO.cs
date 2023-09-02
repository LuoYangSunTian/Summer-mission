using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogueData_SO : ScriptableObject
{
    public List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();//使用列表储存语句条
    public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();//使用字典将编号和对应的piece联系起来

    private void OnValidate()//当inspector窗口中的值被修改时调用
    {
        dialogueIndex.Clear();//清空字典
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }

    public TaskData_SO GetTask()
    {
        TaskData_SO currentTask = null;
        foreach (var piece in dialoguePieces)
        {
            if (piece.task != null)
            {
                currentTask = piece.task;

            }
        }
        return currentTask;
    }
}
