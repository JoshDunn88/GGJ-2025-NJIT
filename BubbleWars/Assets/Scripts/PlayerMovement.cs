using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    public float playerSpeed;
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
        }

        transform.position = newPosition;

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
            
        transform.rotation = Quaternion.Euler(newRotation.x, newRotation.y, newRotation.z);
    }
}
