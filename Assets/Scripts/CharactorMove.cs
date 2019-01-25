using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
	public class CharactorMove : MonoBehaviour
	{
        private Animator anim;
        public float moveSpeed = 3;
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
                transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
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
                transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
            }
            else if (v == 0 && h == 0)
            {
                anim.SetBool("Idle", true);
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
                transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
            }

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == Tags.Tilemap && GameManager.Instance.isInBattleState == false)
            {
                if (Random.Range(0, 101) > 98)
                {
                    var monsterScript = collision.GetComponent<MonsterSpwan>();
                    var minMonsterId = monsterScript.minMonsterId;
                    var maxMonsterId = monsterScript.maxMonsterId;
                    var monsterCount = monsterScript.monsterCount;
                    GameManager.Instance.EnterBattleState(minMonsterId, maxMonsterId, monsterCount);
                }
            }
        }
    }
}