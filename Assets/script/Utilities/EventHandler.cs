using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用event和Action需要的头文件
//事件中心脚本
public static class EventHandler
{
    //更新物品栏的UI
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;//注册事件

    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);//加载事件
    }


    //更新武器栏的UI
    public static event Action<List<InventoryWeapon>> UpdateInventoryWeapon;
    public static void CallUpdateInventoryWeapon(List<InventoryWeapon> list)
    {
        UpdateInventoryWeapon?.Invoke(list);
    }

    //注册一个在地面上生成物品的事件
    public static event Action<int, Vector3> InstantiateItemInScene;
    public static void CallInstantiateItemScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }

    //注册一个刷新状态的事件
    public static event Action<float, float, float, float> UpdateStatus;

    public static void CallUpdateStatus(float currentHp, float maxHP, float currentMp, float maxMp)
    {
        UpdateStatus?.Invoke(currentHp, maxHP, currentMp, maxMp);
    }

    //注册一个加载场景的事件
    public static event Action<string, Vector3> TransitionEvent;

    public static void CallTransitionEvent(string sceneName, Vector3 pos)
    {
        TransitionEvent?.Invoke(sceneName, pos);
    }

    //注册一个卸载场景之前需要做的事情的事件

    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    //加载场景之后要做的事件
    public static event Action AfterSceneLoadedEvent;

    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    //加载场景时人物位置的改变
    public static event Action<Vector3> MoveToPosition;

    public static void CallMoveToPostion(Vector3 targetPosition)
    {
        MoveToPosition?.Invoke(targetPosition);
    }


    //切换鼠标的图片
    public static event Action<Sprite> SwitchMouseImageEvent;

    public static void CallSwitchMouseImageEvent(Sprite sprite)
    {
        SwitchMouseImageEvent?.Invoke(sprite);
    }

    //觉醒魔法
    public static event Action AwakeningMagic;

    public static void CallAwakeningMagic()
    {
        AwakeningMagic?.Invoke();
    }

    //更新觉醒UI
    public static event Action UpdateAwakeningUI;

    public static void CallUpdateAwakeningUI()
    {
        UpdateAwakeningUI?.Invoke();
    }

    //更新技能UI
    public static event Action<SkillDetails> UpdateSkillUI;

    public static void CallUpdateSkillUI(SkillDetails skillDetails)
    {
        UpdateSkillUI?.Invoke(skillDetails);
    }

    //关闭觉醒法阵
    public static event Action CloseAwakeningCircle;

    public static void CallCloseAwakeningCircle()
    {
        CloseAwakeningCircle?.Invoke();
    }

    //取消武器的选择

    public static event Action CancelWeapon;

    public static void CallCancelWeapon()
    {
        CancelWeapon?.Invoke();
    }

    //释放魔法
    public static event Action<int> CreateMagic;
    public static void CallCreateMagic(int index)
    {
        CreateMagic?.Invoke(index);
    }


    //更新商店UI
    public static event Action<WeaponDataList_So> UpdateShopUI;

    public static void CallUpdateShopUI(WeaponDataList_So weaponList)
    {
        UpdateShopUI?.Invoke(weaponList);
    }

    //打开商店
    public static event Action OpenShop;
    public static void CallOpenShop()
    {
        OpenShop?.Invoke();
    }

    //更新金钱
    public static event Action<int> UpdateMoney;
    public static void CallUpdateMoney(int income)
    {
        UpdateMoney?.Invoke(income);
    }

    //复活
    public static event Action Resurgence;
    public static void CallResurgence()
    {
        Resurgence?.Invoke();
    }
}
