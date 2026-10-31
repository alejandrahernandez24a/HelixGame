using UnityEngine;

public class BallController : MonoBehaviour
{
    
    public Rigidbody rb;

    public float impulseForce = 3f;
    private bool ignoreNextCollision; 

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
        {
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

}
