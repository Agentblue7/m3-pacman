using UnityEngine;

public class ScoreCounter1 : MonoBehaviour
{
    private int score;
    // TODO: Maak een private int score variabele

    // TODO: Maak een publieke functie AddPoints(int points)
    // die de score verhoogt en print met Debug.Log
  
    void Start()
    {
        AddPoints(10);
        Debug.Log("Score: " + score);
    }

    private void AddPoints(int points) 
    { 
        score += 10;
    }
}
