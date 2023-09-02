using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueOption
{
    [TextArea]
    public string text; //选择的回答内容
    public string targerID;//选择后跳转到哪一条ID
    public bool takeTask;//是否接收任务
}
