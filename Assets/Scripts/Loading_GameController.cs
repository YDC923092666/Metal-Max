using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace MetalMax
{
	public class Loading_GameController : MonoBehaviour 
	{
        private Slider slider;
        private string nextSceneName;

        private float loadingSpeed = 1;

        private float targetValue;

        private AsyncOperation operation;

        private void Start()
        {
            //暂时隐藏UI
            //UICanvas = GameObject.Find("UICanvas");
            //UICanvas.SetActive(false);

            slider = FindObjectOfType<Slider>();
            slider.value = 0.0f;

            nextSceneName = GameManager.nextSceneName;

            if (SceneManager.GetActiveScene().name == "Loading")
            {
                //启动协程  
                StartCoroutine(AsyncLoading());
            }
        }

        private void Update()
        {
            targetValue = operation.progress;

            if (operation.progress >= 0.9f)
            {
                //operation.progress的值最大为0.9  
                targetValue = 1.0f;
            }

            if (targetValue != slider.value)
            {
                //插值运算  
                slider.value = Mathf.Lerp(slider.value, targetValue, Time.deltaTime * loadingSpeed);
                if (Mathf.Abs(slider.value - targetValue) < 0.01f)
                {
                    slider.value = targetValue;
                }
            }

            if ((int)(slider.value * 100) == 100)
            {
                //允许异步加载完毕后自动切换场景  
                operation.allowSceneActivation = true;
            }
        }

        IEnumerator AsyncLoading()
        {
            operation = SceneManager.LoadSceneAsync(nextSceneName);
            //阻止当加载完成自动切换  
            operation.allowSceneActivation = false;

            yield return operation;
        }
    }
}
