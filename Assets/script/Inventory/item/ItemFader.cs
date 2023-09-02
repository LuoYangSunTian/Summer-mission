
using UnityEngine;
using DG.Tweening;//DOT插件的命名空间

[RequireComponent(typeof(SpriteRenderer))]//保证挂载该组件的物体有SpriteRender
public class ItemFader : MonoBehaviour
{
    private SpriteRenderer spriterender;
    private void Awake()
    {
        spriterender = GetComponent<SpriteRenderer>();
    }

    //通过DOT插件实现颜色的渐变
    public void Fadein()//逐渐恢复颜色
    {

        Color targetColor = new Color(1, 1, 1, 1);
        spriterender.DOColor(targetColor, Settings.fadeDuration);//直接调用Settings里的数值控制颜色的恢复时间
    }

    public void Fadeout()//实现半透明
    {
        Color targetColor = new Color(1, 1, 1, Settings.targetAlpha);
        spriterender.DOColor(targetColor, Settings.fadeDuration);//直接调用Settings里的数值控制颜色的半透明的时间
    }
}
