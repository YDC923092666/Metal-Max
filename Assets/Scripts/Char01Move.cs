using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Char01Move : MonoBehaviour 
	{
        private Animator anim;
        public float speed = 3;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float v = Input.GetAxisRaw("Vertical");
            float h = Input.GetAxisRaw("Horizontal");

            if (v != 0 && h == 0)
            {
                anim.SetFloat("v", v);
                anim.SetBool("Idle", false);
                transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
            }
            else if (v == 0 && h != 0)
            {
                anim.SetFloat("v", v);
                anim.SetFloat("h", h);
                anim.SetBool("Idle", false);
                transform.Translate(Vector3.right * h * speed * Time.fixedDeltaTime, Space.World);
            }
            else if(v == 0 && h == 0)
            {
                anim.SetBool("Idle",true);
            }
            else
            {
                anim.SetFloat("v", v);
                anim.SetBool("Idle", false);
                transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
            }

        }
    }
}
