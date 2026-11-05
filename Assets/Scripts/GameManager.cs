
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
        else if (singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        bestScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void NextLevel()
    {
        currentLevel++;
        Debug.Log("Next Level: " + currentLevel);
    }

    public void RestartLevel()
    {
        currentScore = 0;

        Debug.Log("Restarting Level: " + currentLevel);

        BallController ball = FindAnyObjectByType<BallController>();

        if (ball != null)
        {
            ball.ResetBall();
        }
        else
        {
            Debug.LogWarning("No se encontró BallController en la escena.");
        }
    }

    public void AddScore(int scoretoAdd)
    {
        currentScore += scoretoAdd;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);
        }
    }
}
