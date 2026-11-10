
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody rb;
    public float impulseForce = 3f;
    private bool ignoreNextCollision;
    private Vector3 startPosition;
    [HideInInspector]
    public int perfectPass;
    public float superSpeed = 8; 
    private bool isSuperSpeedActive; 
    private int perfectPassCount = 3;



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

        if (isSuperSpeedActive && !collision.transform.GetComponent<GoalController>())
        {
            Destroy(collision.transform.parent.gameObject, 0.2f);
        }
        else  
        {
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();

        if (deathPart)
        {
              GameManager.singleton.RestartLevel();
        }
        }

        
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        ignoreNextCollision = true;

        Invoke(nameof(AllowNextCollision), 0.2f);

        perfectPass =0; 
        isSuperSpeedActive = false; 
    }

    private void Update()
    {
        if (perfectPass >= perfectPassCount && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * superSpeed, ForceMode.Impulse);
        }
        
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
