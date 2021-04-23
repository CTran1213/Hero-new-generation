using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Waypoint[] waypoints;

    bool randomMovement = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(waypoints[0].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            randomMovement = !randomMovement;
        }
    }

    public Vector3 FindNearest(Transform tf)
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

        return nearest.transform.position;
    }

    public Vector3 FindNext(Transform tf)
    {
        if(randomMovement)
        {
            return waypoints[Random.Range(0, waypoints.Length)].transform.position;
        }
        else
        {
            for(int i = 0 ; i < waypoints.Length; i++)
            {
                if(i == waypoints.Length - 1)
                {
                    return waypoints[0].transform.position;
                }
                else if(tf.position == waypoints[i].transform.position)
                {
                    return waypoints[i+1].transform.position;
                }
            }
            return new Vector3(0,0,0);
        }   
    }
}
