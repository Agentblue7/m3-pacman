using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 100;
    // TODO: Maak health variabele

    // TODO: Maak TakeDamage functie met if-check
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Debug.Log("GameOver");
        } 
    void Start()
    {
        // Test: neem 3x 40 damage
        TakeDamage(40);
        TakeDamage(40);
        TakeDamage(40);
    }
}