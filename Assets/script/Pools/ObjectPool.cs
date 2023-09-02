using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour, Recycleable
{
    private List<T> pool;//存储对象的脚本
    public string prefab;//对象的prefab路径
    /*    private int capacity;//目前池子的大小
        private int fillSize;//每次扩充时扩充的个数*/

    public ObjectPool(string dir)
    {
        pool = new List<T>();
        prefab = dir;
        /*        capacity = 0;
                fillSize = 5;*/
    }

    public void Recycle(T target)
    {
        target.BeforeRecycle();

        target.gameObject.SetActive(false);
        target.transform.SetParent(null);
        pool.Add(target);
        target.AfterRecycle();
    }

    /// <summary>
    /// 从池子中获取对象
    /// </summary>
    /// <returns></returns>
    public T GetItem()
    {
        if (pool.Count <= 0) return null;//检查池子中是否有闲置的对象
        T it = pool[0];
        if (it.transform.parent != null || !it.gameObject.activeInHierarchy)
            return null;
        it.BeforeGet();
        pool.RemoveAt(0);
        it.gameObject.SetActive(true);
        return it;
    }
}
