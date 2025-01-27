using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PebbleController : MonoBehaviour
{
   
    public float timetolive;
    private float birthtime;
    private float deathtime;

    SoundManager sm;

    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

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

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 10);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("trigger triggered");
        //6 is arena walls
        if (other.gameObject.layer == LayerMask.NameToLayer("Arena Bounds"))
        {
            if (gameObject) Destroy(gameObject);
        }

        //7 is bubble layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            sm.PlaySFX(sm.bubblePop);
            if (other.gameObject) Destroy(other.gameObject);
            //if no piercing powerup, remove second hallf to allow charging bubble parries
            if (gameObject && other.gameObject.GetComponent<BubbleController>().blown) Destroy(gameObject);
        }

        //might not need 2 ifs here
        if (other.gameObject.CompareTag("Player1"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
            if (gameObject) Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("Player2"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Die();
            if (gameObject) Destroy(gameObject);
        }
    }
}
