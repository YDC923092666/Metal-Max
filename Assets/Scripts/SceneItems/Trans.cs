using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetalMax
{
	public class Trans : MonoBehaviour
	{
        public string sceneName;
        public string spawnPoint;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == Tags.charactor)
            {
                LoadScene(sceneName, spawnPoint);
            }
        }

        void LoadScene(string sceneName, string spawnPoint)
        {
            PlayerPrefs.SetString("sceneName", sceneName);
            PlayerPrefs.SetString("spawnPoint", spawnPoint);

            GameManager.nextSceneName = sceneName;
            SceneManager.LoadScene("Loading");
        }
    }
}