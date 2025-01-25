using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
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
        if (Time.time > deathtime)
        {
            Destroy(gameObject);
        }
    }
}
