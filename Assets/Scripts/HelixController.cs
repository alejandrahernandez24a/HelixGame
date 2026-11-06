
using UnityEngine;
using System.Collections.Generic;
 

public class HelixController : MonoBehaviour
{
    private Vector2 lastMousePosition;
    private bool isDragging = false;
    public float rotationSpeed = 0.5f;
    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helixLevelPrefab;
    public List<Stage> allStages = new List<Stage>();
    public float helixDistance; 
    private List<GameObject> spawnedLevels = new List<GameObject>();


    public void Awake()
    {
        helixDistance = topTransform.position.y - goalTransform.position.y + .1f;
        //LoadStage(0);
    }

    void Update()
    {
   
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }

 
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;

            float distance = currentMousePosition.x - lastMousePosition.x;

            transform.Rotate(Vector3.up, distance * rotationSpeed, Space.World);

            lastMousePosition = currentMousePosition;
        }

 
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    public void LoadStage(int stageNumber)
    {
        
    }
}
