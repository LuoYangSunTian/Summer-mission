using Cinemachine;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += SwitchfinerShape;//因为事件没有参数直接，加载到事件中
    }


    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= SwitchfinerShape;
    }


    private void SwitchfinerShape()
    {
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();

        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = confinerShape;

        //清除缓存
        confiner.InvalidatePathCache();

    }
}
