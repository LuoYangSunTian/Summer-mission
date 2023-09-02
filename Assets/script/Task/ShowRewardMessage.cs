using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Gameobject.Inventory;
public class ShowRewardMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//实现鼠标滑入和鼠标滑出的两个接口
{
    private TaskRewardSoltUI rewardSoltUI;
    private void Awake()
    {
        rewardSoltUI = GetComponent<TaskRewardSoltUI>();
    }
    private void OnDisable()
    {
        TaskUI.Instance.tooltip.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        TaskUI.Instance.tooltip.gameObject.SetActive(true);
        TaskUI.Instance.tooltip.SetupTooltip(rewardSoltUI.itemDetails);


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TaskUI.Instance.tooltip.gameObject.SetActive(false);
    }
}
