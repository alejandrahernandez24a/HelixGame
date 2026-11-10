
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public TMP_Text currentScoreText;
    public TMP_Text bestScoreText;

    public Slider slider;

    public TMP_Text actualLevelText;
    public TMP_Text nextLevelText;

    public Transform topTransform;
    public Transform goalTransform;
    public Transform ball;

    void Update()
    {
        if (GameManager.singleton == null)
            return;

        // Actualizar puntuaciones
        currentScoreText.text = "Current Score: " +
            GameManager.singleton.currentScore;

        bestScoreText.text = "Best Score: " +
            GameManager.singleton.bestScore;

        // Actualizar nivel y progreso
        ChangeSliderLevelAndProgress();
    }

    public void ChangeSliderLevelAndProgress()
    {
        if (topTransform == null || goalTransform == null ||
            ball == null || slider == null)
            return;

        // Actualizar nombres de los niveles
        actualLevelText.text = " " +
            (GameManager.singleton.currentLevel + 1);

        nextLevelText.text = " " +
            (GameManager.singleton.currentLevel + 2);

        // Calcular el progreso de la pelota
        float progress = Mathf.InverseLerp(
            topTransform.position.y,
            goalTransform.position.y,
            ball.position.y
        );

        // Mantener el valor entre 0 y 1
        progress = Mathf.Clamp01(progress);

        // Actualizar suavemente el slider
        slider.value = Mathf.Lerp(
            slider.value,
            progress,
            Time.deltaTime * 5f
        );
    }
}
