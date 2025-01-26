using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WindController : MonoBehaviour
{
    public GameObject windArrow;
    public GameObject windText;
    public float changeFrequency;
    public float maxSpeed;
    public float minSpeed;

    public float direction;
    public float magnitude;

    private float targetDirection;
    private float targetMagnitude;
    private float lastChange;
    private float magnitudeChangeTime = 20;
    private float directionChangeTime = 20;

    public Vector2 getWindVector()
    {
        Vector2 wind = new Vector2 (0, 0);
        wind.x = Mathf.Cos(direction * Mathf.Deg2Rad) * magnitude;
        wind.y = Mathf.Sin(direction * Mathf.Deg2Rad) * magnitude;

        return wind;
    }

    private Quaternion getWindRotation()
    {
        
        Quaternion newRot = Quaternion.Euler(0f, 0f, direction-90);
        return newRot;
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
            targetMagnitude = Random.Range(minSpeed/1000, maxSpeed/1000);
            lastChange = Time.time;
        }
        //
        windArrow.GetComponent<RectTransform>().rotation = getWindRotation();
        windText.GetComponent<TMP_Text>().text = (Mathf.Floor(magnitude * 10000) / 10 + " MPH");
    }

    private void FixedUpdate()
    {
        direction += (targetDirection - direction) / directionChangeTime;
        magnitude += (targetMagnitude - magnitude) / magnitudeChangeTime;
    }
}
