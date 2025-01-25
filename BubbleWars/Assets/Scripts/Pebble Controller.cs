using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleController : MonoBehaviour
{
    public float timetolive;
    private float birthtime;
    private float deathtime;
    // Start is called before the first frame update
    void Start()
    {
        birthtime = Time.time;
        deathtime = birthtime + timetolive;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: kill pebble if offscreen maybe

        //if (Time.time > deathtime)
       // {
          //  Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger triggered");
        //6 is arena walls
        if (other.gameObject.layer == LayerMask.NameToLayer("Arena Bounds"))
        {
            Destroy(gameObject);
        }

        //7 is bubble layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            Destroy(other.gameObject);
            //if no piercing powerup
            Destroy(gameObject);
        }
    }
}