using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Sayan.CardGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float xmoveRate, yMoveRate;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.position += new Vector3(0, yMoveRate);
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                // later use mathf.lerp for smooth
                transform.position += new Vector3(xmoveRate, 0);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position -= new Vector3(xmoveRate, 0);
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Collect")
            {
                Destroy(collision.gameObject);
                Debug.Log("GOT POINT");
                // Point++
            }
            else if (collision.gameObject.tag == "Obstacle")
            {
                Debug.Log("HIT ENEMY");
                Destroy(gameObject);
            }
        }
    }
}
