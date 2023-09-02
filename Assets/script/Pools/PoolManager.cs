using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    //通过字典储存不同对象的对象池，键表示对象的prefabs的路径，值表示对应的对象池
    private static Dictionary<string, object> pools;
    // Start is called before the first frame update
    void Start()
    {
        pools = new Dictionary<string, object>();
    }


    /// <summary>
    /// 实例化对象
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>

    public static GameObject CreatGameObject(string dir, Vector2 pos)
    {
        GameObject o = Resources.Load<GameObject>(dir);
        GameObject go = Instantiate(o);
        go.transform.position = pos; //localPosition本地位置是相对于父物体的坐标系而言的，它描述了物体在父物体坐标系中的位置偏移量。
        return go;
    }

    public static void CreatPool<T>(ObjectPool<T> target) where T : MonoBehaviour, Recycleable
    {
        pools.Add(target.prefab, target);
    }

    public static void Recycle<T>(T target, string prefabs) where T : MonoBehaviour, Recycleable
    {
        ObjectPool<T> pool = (ObjectPool<T>)pools[prefabs];
        pool.Recycle(target);
    }

    /*    public static T GetItem<T>(string prefabs, float x = 0, float y = 0) where T : MonoBehaviour, Recycleable
        {
            return GetItem<T>(prefabs, new Vector2(x, y));
        }*/
    public static T GetItem<T>(string prefabs, Vector3 pos) where T : MonoBehaviour, Recycleable
    {
        if (!pools.ContainsKey(prefabs))
        {
            PoolManager.CreatPool<T>(new ObjectPool<T>(prefabs));
        }
        ObjectPool<T> pool = (ObjectPool<T>)pools[prefabs];
        T it = pool.GetItem();
        if (it == null)
        {
            GameObject gm = CreatGameObject(prefabs, pos);
            if ((it = gm.GetComponent<T>()) == null)
            {
                //报错
                Debug.LogError("物体上没有脚本T");
            }
        }

        it.gameObject.transform.position = pos;
        it.AfterGet();
        return it;
    }
}
