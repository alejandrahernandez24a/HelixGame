
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

        BallController ball =
            FindAnyObjectByType<BallController>();

        HelixController helix =
            FindAnyObjectByType<HelixController>();

        if (ball == null || helix == null)
        {
            Debug.LogError(
                "No se encontró la pelota o HelixController."
            );
            return;
        }

        ball.ResetBall();

        helix.LoadStage(currentLevel);

        Debug.Log("Next Level: " + currentLevel);
    }

    public void RestartLevel()
    {
        currentScore = 0;

        BallController ball =
            FindAnyObjectByType<BallController>();

        HelixController helix =
            FindAnyObjectByType<HelixController>();

        if (ball == null || helix == null)
        {
            Debug.LogError(
                "No se encontró la pelota o HelixController."
            );
            return;
        }

        ball.ResetBall();

        helix.LoadStage(currentLevel);

        Debug.Log("Nivel reiniciado: " + currentLevel);
    }

    public void AddScore(int scoretoAdd)
    {
        currentScore += scoretoAdd;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;

            PlayerPrefs.SetInt(
                "HighScore",
                currentScore
            );

            PlayerPrefs.Save();
        }
    }
}
