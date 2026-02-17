using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector2 moveDirection;
    private Vector2 nextDirection;
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        transform.position = RoundToGrid(transform.position);
        targetPosition = transform.position;
    }

    void Update()
    {
        HandleInput();

        if (!isMoving)
        {
            TryMove(nextDirection);
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.fixedDeltaTime
            );

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                transform.position = targetPosition;
                isMoving = false;

                // continue moving if holding direction
                TryMove(nextDirection);
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            nextDirection = Vector2.up;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            nextDirection = Vector2.down;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            nextDirection = Vector2.left;

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            nextDirection = Vector2.right;
    }

    void SpriteDirection()
    {
        if (moveDirection == Vector2.up)
            transform.rotation = Quaternion.Euler(0, 0, 90);

        else if (moveDirection == Vector2.down)
            transform.rotation = Quaternion.Euler(0, 0, -90);

        else if (moveDirection == Vector2.left)
            transform.rotation = Quaternion.Euler(0, 0, 180);

        else if (moveDirection == Vector2.right)
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void TryMove(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        Vector3 newPosition = targetPosition + (Vector3)direction;

        if (!IsWall(newPosition))
        {
            moveDirection = direction;
            targetPosition = newPosition;
            isMoving = true;

            SpriteDirection();
        }
    }

    bool IsWall(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, 0.2f);
        if (hit != null && hit.CompareTag("Wall"))
            return true;

        return false;
    }

    Vector3 RoundToGrid(Vector3 pos)
    {
        return new Vector3(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y),
            0
        );
    }
}