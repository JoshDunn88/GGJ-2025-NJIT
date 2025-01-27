using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gm;
    public WindController wc;
    public int playerNumber;
    public GameObject projectile;
    public GameObject bubbile;
    public GameObject tool;
    public Sprite sling;
    public Sprite wand;
    public float playerSpeed;
    public float projectileSpeed;
    public float bubbleSpeed;
    public float playerRotation;
    public float shotFireRate;
    public float bubbleFireRate;
    public float projectileChargeTime;
    public float bubbleChargeTime;
    public float bubbleSizeIncrement;

    //for pause etc
    public bool canShoot;
    public bool canMove;
    public bool canBlow;

    private Rigidbody2D rb;
    private GameObject currentBubble;
    private GameObject currentPebble;
    private float lastShot;
    private float lastBubble;
    private float chargeStart;
    private bool chargin;

    private float lastStep;
    private float stepFrequency = 0.5f;
    private float footstepVolume = 0.08f;

    SoundManager sm;

    private void Awake()
    {
        sm = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        sm.PlaySFX(sm.death);
        if (playerNumber == 1) 
        {
            //p2 won
            gm.level.EndRound();
            gm.level.AddScore(1);
            sm.PlaySFX(sm.score);
        }

        if (playerNumber == 2)
        {
            //p1 won
            gm.level.EndRound();
            gm.level.AddScore(0);
            sm.PlaySFX(sm.score);
        }
    }

    private void ChargeShot()
    {
        
    }

    private void ChargeBubbleStart()
    {
        Vector3 bubblePosition = tool.transform.position;
        bubblePosition.z = 0;
        currentBubble = Instantiate(bubbile, bubblePosition, transform.rotation);
        currentBubble.GetComponent<BubbleController>().windController = wc;
        chargeStart = Time.time;
        chargin = true;
        tool.GetComponent<SpriteRenderer>().sprite = wand;
    }
    private void ChargeBubble()
    {
        //can happen if charged bubble hits or is hit by enemy pebble, penalize with reload time
        if (!currentBubble)
        {
            chargin = false;
            lastBubble = Time.time;
            return;
        }

        //move bubble to wand
        Vector3 bubblePosition = tool.transform.position + transform.right * (0.45f * currentBubble.transform.localScale.x/bubbleSizeIncrement);
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
        Vector3 projectilePosition = transform.position + transform.right;
        projectilePosition.z = 0;
        GameObject pebble = Instantiate(projectile, projectilePosition, transform.rotation);
        sm.PlaySFX(sm.slingshot);
        pebble.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
        tool.GetComponent<SpriteRenderer>().sprite = sling;
    }
    private void Blow()
    {
        lastBubble = Time.time;
        currentBubble.GetComponent<Rigidbody2D>().velocity = transform.right * bubbleSpeed;
        currentBubble.GetComponent<BubbleController>().blown = true;
        sm.PlaySFX(sm.bubbleCharge);
        currentBubble = null;
        //tool.GetComponent<SpriteRenderer>().sprite = sling;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        canShoot = false;
        canMove = false;
        canBlow = false;
        lastShot = Time.time - shotFireRate;
        lastBubble = Time.time - bubbleFireRate;
        rb = GetComponent<Rigidbody2D>();
        chargin = false;
        lastStep = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.V) && Time.time >= lastShot + shotFireRate && canShoot && !chargin)
            {
                Shoot();
            }

            if (Input.GetKeyDown(KeyCode.B) && Time.time >= lastBubble + bubbleFireRate && canBlow)
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
            if (Input.GetKeyDown(KeyCode.RightBracket) && Time.time >= lastShot + shotFireRate && canShoot && !chargin)
            {
                Shoot();
            }


            if (Input.GetKeyDown(KeyCode.Backslash) && Time.time >= lastBubble + bubbleFireRate && canBlow)
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
            if (canMove)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    newPosition.y = transform.position.y + movement;
                    if (Time.time > lastStep + stepFrequency)
                    {
                        sm.PlaySFX(sm.footsteps, footstepVolume);
                        lastStep = Time.time;
                    }
                       
                }
                if (Input.GetKey(KeyCode.S))
                {
                    newPosition.y = transform.position.y - movement;
                    if (Time.time > lastStep + stepFrequency)
                    {
                        sm.PlaySFX(sm.footsteps, footstepVolume);
                        lastStep = Time.time;
                    }
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
        }
        if (playerNumber == 2)
        {
            if (canMove)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    newPosition.y = transform.position.y + movement;
                    if (Time.time > lastStep + stepFrequency)
                    {
                        sm.PlaySFX(sm.footsteps, footstepVolume);
                        lastStep = Time.time;
                    }
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    newPosition.y = transform.position.y - movement;
                    if (Time.time > lastStep + stepFrequency)
                    {
                        sm.PlaySFX(sm.footsteps, footstepVolume);
                        lastStep = Time.time;
                    }
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
        }

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
    }
}
