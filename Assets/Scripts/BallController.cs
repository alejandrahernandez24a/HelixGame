
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;

    public float impulseForce = 3f;
    private bool ignoreNextCollision;
    private Vector3 startPosition;



    private void Start()
    {
        startPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        {
            return;
        }

        DeathPart deathPart = collision.gameObject.GetComponent<DeathPart>();

        if (deathPart != null)
        {
            Debug.Log("Game Over");
            GameManager.singleton.RestartLevel();
            return;
        }

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;
        Invoke(nameof(AllowNextCollision), 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    
    }
}
