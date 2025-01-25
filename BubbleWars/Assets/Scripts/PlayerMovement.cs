using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber;
    public float playerSpeed;
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
        float movement = playerSpeed / 100;
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
        }

        transform.position = newPosition;
    }
}
