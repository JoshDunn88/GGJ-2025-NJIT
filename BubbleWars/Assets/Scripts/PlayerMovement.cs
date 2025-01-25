using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    public GameObject projectile;
    public GameObject bubbile;
    public float playerSpeed;
    public float projectileSpeed;
    public float bubbleSpeed;
    public float playerRotation;
    public float fireRate;

    private Rigidbody2D rb;
    private float lastShot;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time - fireRate;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
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

            if (Input.GetKeyDown(KeyCode.V) && Time.time >= lastShot + fireRate)
            {
                lastShot = Time.time;
                Vector3 projectilePosition = transform.position + transform.right * 0.7f;
                projectilePosition.z = 0;
                GameObject pebble = Instantiate(projectile, projectilePosition, transform.rotation);
                pebble.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
            }

            if (Input.GetKeyDown(KeyCode.B) && Time.time >= lastShot + fireRate)
            {
                lastShot = Time.time;
                Vector3 bubblePosition = transform.position + transform.right * 0.7f;
                bubblePosition.z = 0;
                GameObject bubble = Instantiate(bubbile, bubblePosition, transform.rotation);
                bubble.GetComponent<Rigidbody2D>().velocity = transform.right * bubbleSpeed;
            }

            //rotation clamp
            if (newRotation.z < 270f && newRotation.z > 180f)
            {
                print("under");
                newRotation.z = 270f;

            }

            if (newRotation.z > 90f && newRotation.z < 180f)
            {
                print("over");
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

            if (Input.GetKeyDown(KeyCode.RightBracket) && Time.time >= lastShot + fireRate)
            {
                lastShot = Time.time;
                Vector3 projectilePosition = transform.position + transform.right * 0.7f;
                projectilePosition.z = 0;
                GameObject pebble = Instantiate(projectile, projectilePosition, transform.rotation);
                pebble.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Backslash) && Time.time >= lastShot + fireRate)
            {
                lastShot = Time.time;
                Vector3 bubblePosition = transform.position + transform.right * 0.7f;
                bubblePosition.z = 0;
                GameObject bubble = Instantiate(bubbile, bubblePosition, transform.rotation);
                bubble.GetComponent<Rigidbody2D>().velocity = transform.right * bubbleSpeed;
            }

            //rotation clamp, different for p2 because rotated to face center
            if (newRotation.z > 270f && newRotation.z < 360f)
            {
                print("under");
                newRotation.z = 270f;

            }

            if (newRotation.z < 90f && newRotation.z > 0f)
            {
                print("over");
                newRotation.z = 90f;

            }
        }

        transform.position = newPosition;

        //rotation clamp 
        
            
        transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
    }
}
