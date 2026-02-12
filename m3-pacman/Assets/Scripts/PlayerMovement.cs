using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            movementDirection = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            movementDirection = Vector2.down;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            movementDirection = Vector2.left;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            movementDirection = Vector2.right;
    }


        void FixedUpdate()
        {
            // changing direction 
            if (CanMove(movementDirection))
            {
                movement = movementDirection;
            }

            // keeping direction
            if (CanMove(movement))
            {
                rb.linearVelocity = movement * speed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }

        bool CanMove(Vector2 direction)
        {
            if (direction == Vector2.zero)
                return false;

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                direction,
                0.55f
            );

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall"))
                    return false;
            }

            return true;
        }
    }
