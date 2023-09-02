using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Sprite normol, conversation, closeAttack, distanceAttack;

    [SerializeField] private Sprite currentSprite; //储存当前鼠标的图片

    private Image cursorImage;

    private RectTransform cursorCanvas;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        EventHandler.SwitchMouseImageEvent += OnSwitchMouseImageEvent;
    }

    private void OnDisable()
    {
        EventHandler.SwitchMouseImageEvent -= OnSwitchMouseImageEvent;
    }

    private void OnSwitchMouseImageEvent(Sprite sprite)
    {
        SetCursorImage(sprite);
    }

    private void Start()
    {

        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();//通过RectTransform获得第一个子物体上的Image
        cursorImage.sprite = normol;

    }

    private void Update()
    {
        cursorImage.transform.position = Input.mousePosition;
    }


    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
}
