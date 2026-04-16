using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaypointFollower : MonoBehaviour
{
    Quaternion targetRotation;
    private Vector2 moveDirection;
    public Transform[] waypoints;
    public float speed = 3f;

    private int currentWaypointIndex = 0;

    private void SpriteDirection()
    {
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            if (moveDirection.x > 0)
                targetRotation = Quaternion.Euler(0, 0, 0);
            else
                targetRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            if (moveDirection.y > 0)
                targetRotation = Quaternion.Euler(0, 0, 90);
            else
                targetRotation = Quaternion.Euler(0, 0, -90);
        }
    }
    void Update()
    {
        //smooth sprite rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 720f * Time.deltaTime);

        if (waypoints.Length == 0) return;

        // Huidige waypoint
        Transform target = waypoints[currentWaypointIndex];

        //richting berekenen
        Vector2 direction = (target.position - transform.position).normalized;
        moveDirection = direction;

        //sprite rotation aanroepen
        SpriteDirection();

        // Beweeg naar target
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        // Check of we er zijn
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            // Loop terug naar begin
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }            
        }
    }
   
}