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
                transform.Translate(Vector3.up);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                anim.SetTrigger("Down");
                transform.Translate(Vector3.down);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                anim.SetTrigger("Left");
                transform.Translate(Vector3.left);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.SetTrigger("Right");
                transform.Translate(Vector3.right);
            }
        }
    }
}
