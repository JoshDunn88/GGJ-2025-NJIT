using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WindController : MonoBehaviour
{
    public float changeFrequency;
    public float maxSpeed;

    public float direction;
    public float magnitude;

    private float targetDirection;
    private float targetMagnitude;
    private float lastChange;
    private float magnitudeChangeTime = 5;
    private float directionChangeTime = 2;

    public Vector2 getWindVector()
    {
        Vector2 wind = new Vector2 (0, 0);
        wind.x = Mathf.Cos(direction) * magnitude;
        wind.y = Mathf.Sin(direction) * magnitude;

        return wind;
    }

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
        magnitude = 0;
        lastChange = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastChange + changeFrequency)
        {
            targetDirection = Random.Range(0, 360);
            targetMagnitude = Random.Range(0, maxSpeed/1000);
            lastChange = Time.time;
        }
    }

    private void FixedUpdate()
    {
        direction += (targetDirection - direction) / directionChangeTime;
        magnitude += (targetMagnitude - magnitude) / magnitudeChangeTime;
    }
}
