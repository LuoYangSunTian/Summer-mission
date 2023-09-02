using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;//使用DOTO的命名空间

public class DialogueUI : Singleton<DialogueUI>
{
    public GameObject dialogue;
    public Text dialogueText;
    public Image faceLeft;
    public Image faceRight;
    public GameObject HintBar;//提示按下空格继续
    private int index;//对话的编号
    [Header("Option")]
    public RectTransform optionPanel;//获得optionPanel的Recttransform
    public OptionUI optionPrefab; //获得选择的预制体
    [Header("Data")]
    public DialogueData_SO currentData;

    private void Update()
    {
        if (HintBar.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (index >= currentData.dialoguePieces.Count)
            {
                dialogue.SetActive(false);
                optionPanel.gameObject.SetActive(false);
                return;
            }
            UpdateCurrentDialogue(currentData.dialoguePieces[index]);
        }
    }
    public void UpdateDialogueData(DialogueData_SO data)
    {
        dialogue.SetActive(true);
        currentData = data;
        index = 0;
    }

    public void UpdateCurrentDialogue(DialoguePiece dialoguePiece)
    {
        dialogueText.text = "";
        /*        dialogueText.text = dialoguePiece.text;*/
        dialogueText.DOText(dialoguePiece.text, 1f);
        if (dialoguePiece.image != null)
            faceLeft.sprite = dialoguePiece.image;
        else
            faceLeft.enabled = false;
        if (dialoguePiece.options.Count == 0 && index <= currentData.dialoguePieces.Count)
        {
            HintBar.SetActive(true);
        }
        else
            HintBar.SetActive(false);
        index++;
        CreateOptions(dialoguePiece);
    }

    private void CreateOptions(DialoguePiece piece)//创造每一句话对应的选择
    {
        if (piece.options.Count > 0)
            optionPanel.gameObject.SetActive(true);
        if (optionPanel.childCount > 0)//判断选择的panel的子物体数量,如果大于0就销毁
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);//根据父物体来销毁其的子物体
            }
        }

        for (int i = 0; i < piece.options.Count; i++)
        {
            var option = Instantiate(optionPrefab, optionPanel);//根据piece中的option个数生成选择
            option.UpdateOption(piece, piece.options[i]);
        }
    }

}
