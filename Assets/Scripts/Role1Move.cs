using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Role1Move : MonoBehaviour 
	{
        public float speed = 2;
        private Animator anim;
        private Rigidbody2D body;
        private void Start()
        {
            anim = GetComponent<Animator>();
            body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("Up",true);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.up, Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                anim.SetTrigger("Down");
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                anim.SetTrigger("Left");
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                anim.SetBool("Test", true);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("Up", false);
            }
            body.velocity = new Vector3(0, 0, 0);
        }
    }
}
