using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

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
            float v = ETCInput.GetAxis("Vertical");
            float h = ETCInput.GetAxis("Horizontal");

            if (v != 0 && h == 0)
            {
                if (v > 0)
                {
                    v = 1;
                }
                else
                {
                    v = -1;
                }
                anim.SetFloat("v", v);
                anim.SetBool("Idle", false);
                transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
            }
            else if (v == 0 && h != 0)
            {
                if (h > 0)
                {
                    h = 1;
                }
                else
                {
                    h = -1;
                }
                anim.SetFloat("v", 0);
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
                if (v > 0)
                {
                    v = 1;
                }
                else
                {
                    v = -1;
                }
                anim.SetFloat("v", v);
                anim.SetFloat("h", 0);
                anim.SetBool("Idle", false);
                transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
            }

        }
    }
}
