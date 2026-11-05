using UnityEngine;

public class PassScorePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Objeto que entró: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (GameManager.singleton != null)
            {
                GameManager.singleton.AddScore(1);

                Debug.Log("Score actualizado: " +
                    GameManager.singleton.currentScore);

                Destroy(gameObject);
            }
        }
    }
}

 
