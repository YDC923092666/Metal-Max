using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetalMax
{
	public class House : MonoBehaviour 
	{
        public string sceneName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == Tags.charactor)
            {
                LoadScene(sceneName);
            }
        }

        void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
