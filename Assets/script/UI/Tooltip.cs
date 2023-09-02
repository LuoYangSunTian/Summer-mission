using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemMessage;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetupTooltip(ItemDetails itemDetails)//设置Tooltip的显示内容
    {
        itemName.text = itemDetails.itemName;
        itemMessage.text = itemDetails.itemDescription;
    }
    private void OnEnable()
    {
        UpdatePosition();
    }
    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);//获得Tooltip四个角的位置

        float width = corners[3].x - corners[0].x;//获得宽
        float height = corners[1].y - corners[0].y;//获得高

        if (mousePos.y < height)//鼠标返回的坐标左下角为原点，判断鼠标到屏幕底部的距离与Tooltip高度的大小
        {
            rectTransform.position = mousePos + Vector3.up * height * 0.6f;//将Tooltip的位置设置在鼠标的上方
        }
        else if (Screen.width - mousePos.x > width)//如果屏幕的宽度减去鼠标的x大于Tooltip的宽度
            rectTransform.position = mousePos + Vector3.right * width * 0.6f;
        else
            rectTransform.position = mousePos + Vector3.left * width * 0.6f;

    }
}
