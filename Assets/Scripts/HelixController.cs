using UnityEngine;

public class HelixController : MonoBehaviour
{
     private Vector2 lastTapPosition;
     private Vector3 startPosition;
    void Awake()
    {
        startPosition = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
        
            Vector2 currentTapPosition = Input.mousePosition;

            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
                return;
            }

            float distance = lastTapPosition.x - currentTapPosition.x;

            transform.Rotate(Vector3.up * distance);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPosition = Vector2.zero;
        }
    }
}
