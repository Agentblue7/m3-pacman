using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float duration = 3f;
    public float boostedSpeed = 8f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();

            player.StartCoroutine(PowerUpRoutine(player, sr));
            Destroy(gameObject);

            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator PowerUpRoutine(PlayerMovement player, SpriteRenderer sr)
    {
        float originalSpeed = player.moveSpeed;

        sr.color = Color.red;
        player.moveSpeed = boostedSpeed;

        yield return new WaitForSeconds(duration);

        player.moveSpeed = originalSpeed;
        sr.color = Color.white;

    }
}