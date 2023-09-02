using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//管理场景的头文件

namespace Transition//使用命名空间使得使用相同命名空间互相连接不会调用出错
{
    public class TransitionManager : MonoBehaviour
    {
        public string startSceneName = string.Empty;
        private CanvasGroup fadeCanvasGroup;
        private bool isFade;
        private void OnEnable()
        {
            EventHandler.TransitionEvent += OnTransitionEvent;
        }



        private void OnDisable()
        {
            EventHandler.TransitionEvent -= OnTransitionEvent;
        }

        private void OnTransitionEvent(string sceneToGo, Vector3 positionToGo)
        {
            if (!isFade)
                StartCoroutine(Transition(sceneToGo, positionToGo));
        }
        private void Start()
        {
            StartCoroutine(LoadSceneSetActive(startSceneName));
            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();//通过类型的形式去查找组件，因为当前只有一个物体挂有CanvasGroup的组件
        }

        private IEnumerator Transition(string sceneName, Vector3 targetPosition)
        {

            yield return Fade(1);//淡入加载界面
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);//使用异步加载的方式获得当前场景的名字来卸载当前场景

            yield return LoadSceneSetActive(sceneName);//携程中调用携程不用StartCoroutine，加载目标场景

            EventHandler.CallMoveToPostion(targetPosition);


            yield return Fade(0);

        }

        private IEnumerator LoadSceneSetActive(string sceneName)//使用携程加载场景
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);//使用异步加载通过场景的名字加载场景，此处yield return作用为暂停携程执行等待异步加载完成

            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);//SceneManager.GetSceneAt（index）通过索引获得目标场景
            /*            Debug.Log("||" + SceneManager.GetSceneByName(sceneName).buildIndex);
                        Debug.Log(SceneManager.sceneCount - 1);*/
            SceneManager.SetActiveScene(newScene);//将目标场景设置为活动场景

            EventHandler.CallAfterSceneLoadedEvent();
        }

        /// <summary>
        /// 淡入淡出场景
        /// </summary>
        /// <param name="targetAlpha">1是黑， 0是透明</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)//加载场景的加载界面
        {
            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();//通过类型的形式去查找组件，因为当前只有一个物体挂有CanvasGroup的组件
            isFade = true;

            fadeCanvasGroup.blocksRaycasts = true;//阻止鼠标的射线投射

            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / Settings.loadFadeDuration;//加载界面透明化的速度

            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))//数学方法判断float类型的是否相等
            {
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);//以一定的速度改变加载界面的透明度
                yield return null;
            }

            fadeCanvasGroup.blocksRaycasts = false;

            isFade = false;
        }
    }
}

