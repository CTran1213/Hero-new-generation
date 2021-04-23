using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider cooldownBar;
    private float maxCooldown = 0.2f;
    private float currentCooldown;
    public static Stamina instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldown = maxCooldown;
        cooldownBar.maxValue = maxCooldown;
    }
}
