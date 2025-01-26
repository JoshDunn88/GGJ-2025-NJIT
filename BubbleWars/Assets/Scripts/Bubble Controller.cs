using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public WindController windController;
    public float timetolive;

    public bool blown;
    private float birthtime;
    private float deathtime;

    // Start is called before the first frame update
    void Start()
    {
        blown = false;
        birthtime = Time.time;
        deathtime = birthtime + timetolive;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > deathtime)
        {
            if (gameObject) Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (blown)
            GetComponent<Rigidbody2D>().velocity += windController.GetComponent<WindController>().getWindVector();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //6 is arena walls
        if (other.gameObject.layer == LayerMask.NameToLayer("Arena Bounds"))
        {
            if (gameObject) Destroy(gameObject);
        }

        /*optional make players pop bubbles
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (gameObject) Destroy(gameObject);
        }
        */
    }
}
