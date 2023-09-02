
using UnityEngine;


//单例模式
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get => instance;
    }

    protected virtual void Awake()
    {
        if (instance != null)//必须满足唯一性的原则
            Destroy(gameObject);
        else
            instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
