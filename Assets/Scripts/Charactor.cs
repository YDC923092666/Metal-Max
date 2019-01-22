using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

namespace MetalMax
{
	public class Charactor : MonoBehaviour 
	{
        public static int initLv = 1;
        public static int initHp = 20;
        public static int initSpeed = 1;
        public static int initDamage = 20;
        public static int initDefense = 20;

        private Animator anim;
        public float speed = 3;
        public static Vector2 direction;

        private void Start()
        {
            DontDestroyOnLoad(this);
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
                    direction = Vector2.up;
                }
                else
                {
                    v = -1;
                    direction = Vector2.down;
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
                    direction = Vector2.right;
                }
                else
                {
                    h = -1;
                    direction = Vector2.left;
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.tag == Tags.Tilemap)
            {
                if (UnityEngine.Random.Range(0, 101) > 98)
                {
                    print("Battle");
                }
            }
        }
    }
}
