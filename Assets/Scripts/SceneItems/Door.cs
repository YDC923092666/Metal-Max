using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class Door : MonoBehaviour 
	{
        private SpriteRenderer sprite;
        private Sprite currentSprite;

        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            currentSprite = sprite.sprite;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == Tags.charactor)
            {
                //Sprite clip = Resources.Load<Sprite>("Sprites/door_open");
                sprite.sprite = null;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == Tags.charactor)
            {
                //Sprite clip = Resources.Load<Sprite>("Sprites/door_open");
                sprite.sprite = currentSprite;
            }
        }
    }
}
