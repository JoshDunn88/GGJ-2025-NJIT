using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    public GameObject projectile;
    public float playerSpeed;
    public float projectileSpeed;
    public float playerRotation;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
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

            if (Input.GetKeyDown(KeyCode.T))
            {
                Vector3 projectilePosition = transform.position + transform.right * 0.7f;
                projectilePosition.z = 0;
                GameObject pebble = Instantiate(projectile, projectilePosition, transform.rotation);
                pebble.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
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
            if (Input.GetKey(KeyCode.I))
            {
                newPosition.y = transform.position.y + movement;
            }
            if (Input.GetKey(KeyCode.K))
            {
                newPosition.y = transform.position.y - movement;
            }
            if (Input.GetKey(KeyCode.J))
            {
                newRotation.z = newRotation.z + rotation;
            }
            if (Input.GetKey(KeyCode.L))
            {
                newRotation.z = newRotation.z - rotation;
            }

            if (Input.GetKeyDown(KeyCode.Slash))
            {
                Vector3 projectilePosition = transform.position + transform.right * 0.7f;
                projectilePosition.z = 0;
                GameObject pebble = Instantiate(projectile, projectilePosition, transform.rotation);
                pebble.GetComponent<Rigidbody2D>().velocity = transform.right * projectileSpeed;
            }

            //rotation clamp
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
