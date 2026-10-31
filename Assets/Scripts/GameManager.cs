using UnityEngine;

public class GameManager : MonoBehaviour
{
     public int bestScore;
     public int currentScore;
     public int currentLevel = 0; 
     public static GameManager singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        currentLevel++;
    }

    public void RestartLevel()
    {
        currentScore = 0;
    }

    public void AddScore(int score)
    {
        currentScore += score;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
        }
    }
 
}
