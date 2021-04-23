using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Waypoint[] waypoints;
    bool randomMovement = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            randomMovement = !randomMovement;
        }
    }

    public Waypoint FindNearest(Transform tf)
    {
        Waypoint nearest = waypoints[0];
        float min = Vector3.Distance(tf.position, nearest.transform.position);

        foreach(var wp in waypoints)
        {
            float distance = Vector3.Distance(tf.position, wp.transform.position);
            if(distance < min)
            {
                min = distance;
                nearest = wp;
            }
        }

        return nearest;
    }

    public Waypoint FindNext(Waypoint lastWP)
    {
        if(randomMovement)
        {
            return waypoints[Random.Range(0, waypoints.Length)];
        }
        else
        {
            for(int i = 0 ; i < waypoints.Length; i++)
            {
                if(i == waypoints.Length - 1)
                {
                    return waypoints[0];
                }
                else if(waypoints[i] == lastWP)
                {
                    return waypoints[i+1];
                }
            }
            return waypoints[0];
        }   
    }
}
