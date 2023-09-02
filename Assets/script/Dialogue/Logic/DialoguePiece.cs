using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialoguePiece
{
    public string ID;//对话的编号
    public Sprite image;
    [TextArea]//扩大文本输入区域
    public string text;//对话的内容
    public TaskData_SO task;//任务
    public List<DialogueOption> options = new List<DialogueOption>();//对话的选择

}
