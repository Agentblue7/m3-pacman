using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float moveDistance = 1f;

    Vector2 currentDir = Vector2.zero;
    Quaternion targetRotation;

    private Vector3 targetPosition;
    private bool moving = false;
    Vector2 lastDir = Vector2.zero;

    void Start()
    {
        targetPosition = transform.position;
    }
    void RotateSprite(Vector2 dir)
    {
        if (dir == Vector2.up)
            targetRotation = Quaternion.Euler(0, 0, 180);

        else if (dir == Vector2.down)
            targetRotation = Quaternion.Euler(0, 0, 0);

        else if (dir == Vector2.left)
            targetRotation = Quaternion.Euler(0, 0, -90);

        else if (dir == Vector2.right)
            targetRotation = Quaternion.Euler(0, 0, 90);
    }
    void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,720f * Time.deltaTime);


        if (!moving)
        {
            Vector2 dir = GetPlayerDirection();

            if (dir != currentDir)
            {
                currentDir = dir;
                RotateSprite(dir);
            }

            targetPosition = transform.position + (Vector3)(dir * moveDistance);
            moving = true;
        }

        transform.position = Vector3.MoveTowards(transform.position,targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            moving = false;
    }
    //welke richting de enemy kan en moet nemen om naar de player te komen
    Vector2 GetPlayerDirection()
    {
        {
            Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

            float bestDist = Mathf.Infinity;
            Vector2 bestDir = lastDir;

            foreach (var dir in dirs)
            {
                if (dir == -lastDir) continue;

                if (IsWall(dir)) continue;

                float dist = Vector2.Distance(
                    transform.position + (Vector3)(dir * moveDistance),
                    player.position
                );

                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestDir = dir;
                }
            }

            return bestDir;
        }
        //checkt voor muren 
        bool IsWall(Vector2 dir)
        {
            Vector2 checkPos = (Vector2)transform.position + dir * moveDistance;

            Collider2D hit = Physics2D.OverlapCircle(checkPos, 0.2f);

            return hit != null && hit.CompareTag("Wall");
        }
    }
}