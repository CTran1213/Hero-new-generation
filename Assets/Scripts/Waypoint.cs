//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    // Start is called before the first frame update
    float x;
    float y;

    int life = 4;

    GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Egg")
        {
            if(life > 1) 
            {
                // Setting the transparency of the sprite
                GetComponent<SpriteRenderer>().color = new Color(
                    GetComponent<SpriteRenderer>().color.r, 
                    GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b,
                    GetComponent<SpriteRenderer>().color.a * 0.75f);

                --life;
            }
            else
            {
                life = 4;
                GetComponent<SpriteRenderer>().color = new Color(
                    GetComponent<SpriteRenderer>().color.r, 
                    GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b,
                    1f);
                MoveToNewPosition();
            }
        }
    }

    private void MoveToNewPosition()
    {
        transform.position = new Vector3(
            Random.Range(x - 15, x + 15),
            Random.Range(y - 15, y + 15),
            0);
    }
}
