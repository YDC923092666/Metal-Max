using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Door : MonoBehaviour 
	{
        private SpriteRenderer sprite;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == Tags.charactor)
            {
                Sprite clip = Resources.Load<Sprite>("Sprites/door_open");
                sprite.sprite = clip;
            }
        }
    }
}
