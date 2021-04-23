using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{
    // Hero movement
    public float speed = 20f;
    const float maxSpeed = 50f;
    const float heroRotateSpeed = 45f; // 90-degrees in 2 seconds
    bool followMousePosition = true;
    const float speedIncrement = 0.1f;
    Rigidbody2D rb2d;

    // Bullet
    public GameObject bulletPrefab;
    const float cooldown = 0.2f;
    float nextFire = 0f;

    // Game controller
    GameController gameController;

    // Hero health

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.rotation = Quaternion.identity;
            followMousePosition = !followMousePosition;
        }

        if (followMousePosition)
        {
            MoveWithMouse();
        }
        else
        {
            MoveWithKey();
        }

        ProcessBulletSpwan();
        
    }

    private void MoveWithKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speed = Mathf.Clamp(speed + speedIncrement, 0f, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (speed <= 0)
            {
                speed = 0f;
            }
            else
            {
                speed -= speedIncrement;
            }
        }

        // Change direction
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, heroRotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -1 * heroRotateSpeed * Time.deltaTime);
        }
        rb2d.velocity = transform.up * speed;
        updatePosition(transform.position);
    }

    private void MoveWithMouse()
    {
        Vector3 pos = transform.position;
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        updatePosition(pos);
    }

    void updatePosition(Vector3 pos) {
        // Limist the movement of hero so that it doesn't go outside the world bounds
        pos.x = Mathf.Clamp(pos.x, gameController.xMin + 1f, gameController.xMax - 1f);
        pos.y = Mathf.Clamp(pos.y, gameController.yMin + 1f, gameController.yMax - 1f);
        pos.z = 0f;
        transform.position = pos;
    }

    private void ProcessBulletSpwan() {
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && Time.time > nextFire) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            ++gameController.eggCount;
            nextFire = Time.time + cooldown;
        }
        gameController.UpdateCooldown(nextFire - Time.time);
    }
}
