using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
         
            GameManager.singleton.NextLevel();
     
    }
}
