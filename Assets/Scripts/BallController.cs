
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;

    public float impulseForce = 3f;

    private bool ignoreNextCollision;
    private Vector3 startPosition;

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        {
            return;
        }

        DeathPart deathPart =
            collision.gameObject.GetComponent<DeathPart>();

        if (deathPart != null)
        {
            Debug.Log("Game Over");

            if (GameManager.singleton != null)
            {
                GameManager.singleton.RestartLevel();
            }

            return;
        }

        if (rb == null)
        {
            Debug.LogError(
                "No se encontró el Rigidbody de la pelota."
            );
            return;
        }

        rb.linearVelocity = Vector3.zero;

        rb.AddForce(
            Vector3.up * impulseForce,
            ForceMode.Impulse
        );

        ignoreNextCollision = true;

        Invoke(nameof(AllowNextCollision), 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        CancelInvoke(nameof(AllowNextCollision));

        ignoreNextCollision = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        transform.position = startPosition;

        Debug.Log("Pelota reiniciada.");
    }
}
