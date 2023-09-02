using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameobject.Inventory
{
    public class ItemManager : MonoBehaviour
    {
        public item itemPrefab;

        public Transform itemParent;

        private Dictionary<string, List<SceneItem>> sceneItemDict = new Dictionary<string, List<SceneItem>>();//使用字典储存所有场景中物品数据

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
            EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        }

        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
            EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
            EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        }

        private void OnBeforeSceneUnloadEvent()
        {
            GetAllSceneItems();
        }

        private void OnAfterSceneLoadedEvent()
        {
            itemParent = GameObject.FindGameObjectWithTag("ItemParent").transform;
            RecreateAllItem();
        }

        private void OnInstantiateItemInScene(int ID, Vector3 pos)
        {
            var item = Instantiate(itemPrefab, pos, Quaternion.identity, itemParent);
            item.itemID = ID;
        }

        /// <summary>
        /// 获得当前场景的所有item
        /// </summary>
        private void GetAllSceneItems()//获得当前场景中所有物品的信息
        {
            List<SceneItem> currentSceneItems = new List<SceneItem>();

            foreach (var item in FindObjectsOfType<item>()) //找到所有挂有item组件的物品
            {
                SceneItem sceneItem = new SceneItem //每一个物体都实例化一个类
                {
                    itemID = item.itemID,
                    position = new SerializableVector3(item.transform.position)
                };

                currentSceneItems.Add(sceneItem);
            }

            if (sceneItemDict.ContainsKey(SceneManager.GetActiveScene().name))  //判断字典是否有当前的场景，有则直接更新场景中的物品数据
            {
                sceneItemDict[SceneManager.GetActiveScene().name] = currentSceneItems;
            }
            else   //如果字典没有当前场景，新加进去
            {
                sceneItemDict.Add(SceneManager.GetActiveScene().name, currentSceneItems);
            }
        }


        /// <summary>
        /// 刷新重建当前场景的物品
        /// </summary>
        private void RecreateAllItem()
        {
            List<SceneItem> currentSceneItems = new List<SceneItem>();

            if (sceneItemDict.TryGetValue(SceneManager.GetActiveScene().name, out currentSceneItems))
            {
                //清场
                foreach (var item in FindObjectsOfType<item>())
                {
                    Destroy(item.gameObject);
                }

                //重新生成
                foreach (var item in currentSceneItems)
                {
                    item newItem = Instantiate(itemPrefab, item.position.ToVector(), Quaternion.identity, itemParent);
                    newItem.Init(item.itemID);
                }
            }

        }
    }
}

