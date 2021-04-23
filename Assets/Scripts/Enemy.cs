//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int life = 4;
    GameController gameController;
    Waypoint nextWP;
    PlaneController planeController;
    float moveSpeed = 30f;
    float rotateSpeed = 300f;
    
    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        planeController = FindObjectOfType<PlaneController>();

        nextWP = planeController.FindNearest(transform);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        // Target position
        Vector2 nextDestination = new Vector2(nextWP.transform.position.x, nextWP.transform.position.y);
        // Current position
        Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
        
        if(Vector2.Distance(nextDestination, currPos) > 20f) 
        {
            // Move forward
            transform.position += transform.up * moveSpeed * Time.deltaTime;

            // Rotate
            Vector2 lookDirection = nextDestination - currPos;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion qTo = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, rotateSpeed * Time.deltaTime);
        }
        else
        {
            nextWP = planeController.FindNext(nextWP);
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
