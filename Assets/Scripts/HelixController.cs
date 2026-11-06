
using UnityEngine;
using System.Collections.Generic;

public class HelixController : MonoBehaviour
{
    private Vector2 lastMousePosition;
    private Vector3 startRotation;
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
        startRotation = transform.localEulerAngles;

        if (topTransform == null || goalTransform == null)
        {
            Debug.LogError("Asigna HelixTop y HelixGoal en el Inspector.");
            return;
        }

        helixDistance = topTransform.position.y - goalTransform.position.y + .1f;

        LoadStage(0);
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
        Debug.Log("Iniciando carga del Stage: " + stageNumber);

        if (allStages == null || allStages.Count == 0)
        {
            Debug.LogError("No hay stages configurados.");
            return;
        }

        if (helixLevelPrefab == null)
        {
            Debug.LogError("No se ha asignado HelixLevel Prefab.");
            return;
        }

        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count - 1)];

        if (stage == null || stage.levels == null || stage.levels.Count == 0)
        {
            Debug.LogError("El Stage no tiene niveles configurados.");
            return;
        }

        if (Camera.main != null)
        {
            Camera.main.backgroundColor = stage.stageBackgroundColor;
        }

        BallController ball = FindAnyObjectByType<BallController>();

        if (ball != null)
        {
            Renderer ballRenderer = ball.GetComponent<Renderer>();

            if (ballRenderer != null)
            {
                ballRenderer.material.color = stage.stageBallColor;
            }
        }

        transform.localEulerAngles = startRotation;

   
        foreach (GameObject go in spawnedLevels)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }

        spawnedLevels.Clear();

         
        float topY = transform.InverseTransformPoint(topTransform.position).y;
        float goalY = transform.InverseTransformPoint(goalTransform.position).y;

        helixDistance = topY - goalY;

        if (helixDistance <= 0)
        {
            Debug.LogError("La distancia entre HelixTop y HelixGoal es incorrecta.");
            return;
        }

        float levelDistance = helixDistance / stage.levels.Count;

        
        for (int i = 0; i < stage.levels.Count; i++)
        {
            float spawnPosY = topY - (levelDistance * (i + 1));

            GameObject level = Instantiate(helixLevelPrefab, transform, false);

            level.transform.localPosition = new Vector3(0, spawnPosY, 0);
            level.transform.localRotation = Quaternion.identity;

            spawnedLevels.Add(level);

         
            int totalParts = level.transform.childCount;

            
            int partsToKeep = Mathf.Clamp(
                stage.levels[i].perCount,
                0,
                totalParts
            );

          
            int partsToDisable = totalParts - partsToKeep;

     
            List<GameObject> parts = new List<GameObject>();

            for (int j = 0; j < totalParts; j++)
            {
                parts.Add(level.transform.GetChild(j).gameObject);
            }

      
            for (int j = 0; j < parts.Count; j++)
            {
                int randomIndex = Random.Range(j, parts.Count);

                GameObject temp = parts[j];
                parts[j] = parts[randomIndex];
                parts[randomIndex] = temp;
            }

     
            for (int j = 0; j < partsToDisable; j++)
            {
                parts[j].SetActive(false);
            }

            Debug.Log(
                "Nivel " + i +
                " | Plataformas totales: " + totalParts +
                " | Activas: " + partsToKeep +
                " | Desactivadas: " + partsToDisable
            );
        }

        Debug.Log("Total de niveles creados: " + spawnedLevels.Count);
    }
}
