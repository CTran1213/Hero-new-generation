//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int life = 4;
    GameController gameController;
    Vector3 nextDestination;
    PlaneController planeController;
    float moveSpeed = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        planeController = FindObjectOfType<PlaneController>();

        nextDestination = planeController.FindNearest(transform);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(transform.position != nextDestination) 
        {
            transform.up = Vector3.Normalize(nextDestination - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, nextDestination, moveSpeed * Time.deltaTime);
        }
        else
        {
            nextDestination = planeController.FindNext(transform);
        }
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") 
        {
            gameController.UpdateEnemyTouchedText();
            selfDestruct();
        }
        else if(collision.gameObject.tag == "Egg") 
        {
            if(life > 1) 
            {
                // Setting the transparency of the sprite
                GetComponent<SpriteRenderer>().color = new Color(
                    GetComponent<SpriteRenderer>().color.r, 
                    GetComponent<SpriteRenderer>().color.g,
                    GetComponent<SpriteRenderer>().color.b,
                    GetComponent<SpriteRenderer>().color.a * 0.8f);

                --life;
            }
            else
            {
                selfDestruct();
            }
        }
    }

    void selfDestruct()
    {
        Destroy(gameObject);
        gameController.EnemyDestroyed();
    }
}
