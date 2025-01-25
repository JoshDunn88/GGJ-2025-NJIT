using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gm;
    public int playerNumber;
    public GameObject projectile;
    public GameObject bubbile;
    public float playerSpeed;
    public float projectileSpeed;
    public float bubbleSpeed;
    public float playerRotation;
    public float fireRate;
    public float projectileChargeTime;
    public float bubbleChargeTime;
    public float bubbleSizeIncrement;


    private Rigidbody2D rb;
    private GameObject currentBubble;
    private GameObject currentPebble;
    private float lastShot;
    private float chargeStart;

    private bool chargin;

    private void ChargeShot()
    {
        
    }

    private void ChargeBubbleStart()
    {
        Vector3 bubblePosition = transform.position + transform.right * 0.7f;
        bubblePosition.z = 0;
        currentBubble = Instantiate(bubbile, bubblePosition, transform.rotation);
        chargeStart = Time.time;
        chargin = true;
    }
    private void ChargeBubble()
    {
        //can happen if charged bubble hits or is hit by enemy pebble, penalize with reload time
        if (!currentBubble)
        {
            chargin = false;
            lastShot = Time.time;
            return;
        }

        Vector3 bubblePosition = transform.position + transform.right * 0.7f;
        bubblePosition.z = 0;
        currentBubble.transform.position = bubblePosition;
        //enforce max bubble size
        if(Time.time < chargeStart + bubbleChargeTime)
        {
            float sizeIncrement = bubbleSizeIncrement / 100;
            Vector3 sizeVector = new Vector3(sizeIncrement, sizeIncrement, 0f);
            currentBubble.transform.localScale += sizeVector;

        }
            
    }
    private void Shoot()
    {
        lastShot = Time.time;
        Vector3 projectilePosition = transform.position + transform.right * 0.7f;
        projectilePosition.z = 0;
        GameObject pebble = Instantiate(projectile, projectilePosition, transform.rotation);
        pebble.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
    }
    private void Blow()
    {
        lastShot = Time.time;
        currentBubble.GetComponent<Rigidbody2D>().velocity = transform.right * bubbleSpeed;
        currentBubble = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time - fireRate;
        rb = GetComponent<Rigidbody2D>();
        chargin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.V) && Time.time >= lastShot + fireRate)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.B) && Time.time >= lastShot + fireRate)
            {
                ChargeBubbleStart();
            }

            if (Input.GetKeyUp(KeyCode.B) && chargin)
            {
                chargin = false;
                Blow();
            }
        }

        if (playerNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.RightBracket) && Time.time >= lastShot + fireRate)
            {
                Shoot();
            }


            if (Input.GetKeyDown(KeyCode.Backslash) && Time.time >= lastShot + fireRate)
            {
                ChargeBubbleStart();
            }
            
            if (Input.GetKeyUp(KeyCode.Backslash) && chargin)
            {
                chargin = false;
                Blow();
            }
        }
    }

    private void FixedUpdate()
    {
        //player movement
        Vector2 newPosition = transform.position;
        Vector3 newRotation = transform.rotation.eulerAngles;
       
        float movement = playerSpeed / 100;
        float rotation = playerRotation / 10;

        if (playerNumber == 1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                newPosition.y = transform.position.y + movement;
            }
            if (Input.GetKey(KeyCode.S))
            {
                newPosition.y = transform.position.y - movement;
            }

            if (Input.GetKey(KeyCode.A))
            {
                newRotation.z = newRotation.z + rotation;
            }
            if (Input.GetKey(KeyCode.D))
            {
                newRotation.z = newRotation.z - rotation;
            }

            if (Input.GetKey(KeyCode.B) && chargin)
            {
                ChargeBubble();
            }

            //rotation clamp
            if (newRotation.z < 270f && newRotation.z > 180f)
            {
                newRotation.z = 270f;
            }

            if (newRotation.z > 90f && newRotation.z < 180f)
            {
                newRotation.z = 90f;
            }
        }

        if (playerNumber == 2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                newPosition.y = transform.position.y + movement;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                newPosition.y = transform.position.y - movement;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                newRotation.z = newRotation.z + rotation;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                newRotation.z = newRotation.z - rotation;
            }

            if (Input.GetKey(KeyCode.Backslash) && chargin)
            {
                ChargeBubble();
            }
            //rotation clamp, different for p2 because rotated to face center
            if (newRotation.z > 270f && newRotation.z < 360f)
            {
                newRotation.z = 270f;
            }

            if (newRotation.z < 90f && newRotation.z > 0f)
            {
                newRotation.z = 90f;
            }
        }

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
    }
}
