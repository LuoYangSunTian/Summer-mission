using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO currentData;

    [SerializeField] private GameObject TalkSymbol;

    private bool canTalk = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && canTalk)
            OpenDialogue();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TalkSymbol.SetActive(true);
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TalkSymbol.SetActive(false);
            DialogueUI.Instance.dialogue.SetActive(false);
            DialogueUI.Instance.optionPanel.gameObject.SetActive(false);
            canTalk = false;
        }
    }

    private void OpenDialogue()//开始对话
    {
        //打开UI面板
        DialogueUI.Instance.UpdateDialogueData(currentData);
        DialogueUI.Instance.UpdateCurrentDialogue(currentData.dialoguePieces[0]);
    }
}
