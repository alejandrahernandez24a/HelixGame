using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text bestScoreText;

    void Update()
    {
        if (GameManager.singleton == null)
            return;

        if (currentScoreText != null)
        {
            currentScoreText.text =
                "Score: " + GameManager.singleton.currentScore;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text =
                "Best Score: " + GameManager.singleton.bestScore;
        }
    }
}