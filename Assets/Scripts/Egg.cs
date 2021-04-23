using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public const float kEggSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (kEggSpeed * Time.smoothDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        --FindObjectOfType<GameController>().eggCount;
        Destroy(gameObject);
    }
}
