using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Role1Move : MonoBehaviour 
	{
        private Animator anim;
        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetTrigger("Up");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetTrigger("Down");
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetTrigger("Left");
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetTrigger("Right");
            }
        }
    }
}
