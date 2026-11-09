
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

    // Guardamos las posiciones de los puntos de referencia.
    private float topLocalY;
    private float goalLocalY;
    private bool anchorsCached = false;

    public void Awake()
    {
        startRotation = transform.localEulerAngles;

        CacheAnchorPositions();

        if (!anchorsCached)
        {
            Debug.LogError(
                "No se pudieron guardar las posiciones de HelixTop y HelixGoal."
            );
            return;
        }

        helixDistance = topLocalY - goalLocalY;

        LoadStage(0);
    }

    private void CacheAnchorPositions()
    {
        if (topTransform == null || goalTransform == null)
        {
            Debug.LogError(
                "Asigna HelixTop y HelixGoal en el Inspector."
            );

            anchorsCached = false;
            return;
        }

        // Guardamos las posiciones locales mientras los objetos existen.
        topLocalY = transform.InverseTransformPoint(
            topTransform.position
        ).y;

        goalLocalY = transform.InverseTransformPoint(
            goalTransform.position
        ).y;

        anchorsCached = true;

        Debug.Log(
            "Posiciones de HelixTop y HelixGoal guardadas."
        );
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

            float distance = currentMousePosition.x -
                             lastMousePosition.x;

            transform.Rotate(
                Vector3.up,
                distance * rotationSpeed,
                Space.World
            );

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

        if (!anchorsCached)
        {
            Debug.LogError(
                "Las posiciones de HelixTop y HelixGoal no están guardadas."
            );
            return;
        }

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

        int index = Mathf.Clamp(
            stageNumber,
            0,
            allStages.Count - 1
        );

        Stage stage = allStages[index];

        if (stage == null ||
            stage.levels == null ||
            stage.levels.Count == 0)
        {
            Debug.LogError(
                "El Stage no tiene niveles configurados."
            );
            return;
        }

        if (Camera.main != null)
        {
            Camera.main.backgroundColor =
                stage.stageBackgroundColor;
        }

        BallController ball =
            FindAnyObjectByType<BallController>();

        if (ball != null)
        {
            Renderer ballRenderer =
                ball.GetComponent<Renderer>();

            if (ballRenderer != null)
            {
                ballRenderer.material.color =
                    stage.stageBallColor;
            }
        }

        // Restablecemos la rotación de la hélice.
        transform.localEulerAngles = startRotation;

        // Destruimos los niveles generados anteriormente.
        foreach (GameObject go in spawnedLevels)
        {
            if (go != null)
            {
                Destroy(go);
            }
        }

        spawnedLevels.Clear();

        // Utilizamos las posiciones guardadas.
        // Ya no accedemos a topTransform.position ni goalTransform.position.
        float topY = topLocalY;
        float goalY = goalLocalY;

        helixDistance = topY - goalY;

        if (helixDistance <= 0)
        {
            Debug.LogError(
                "La distancia entre HelixTop y HelixGoal es incorrecta."
            );
            return;
        }

        float levelDistance =
            helixDistance / stage.levels.Count;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            float spawnPosY =
                topY - (levelDistance * (i + 1));

            GameObject level = Instantiate(
                helixLevelPrefab,
                transform,
                false
            );

            level.transform.localPosition =
                new Vector3(0, spawnPosY, 0);

            level.transform.localRotation =
                Quaternion.identity;

            spawnedLevels.Add(level);

            int totalParts =
                level.transform.childCount;

            int partsToKeep = Mathf.Clamp(
                stage.levels[i].perCount,
                0,
                totalParts
            );

            int partsToDisable =
                totalParts - partsToKeep;

            List<GameObject> parts =
                new List<GameObject>();

            for (int j = 0; j < totalParts; j++)
            {
                parts.Add(
                    level.transform.GetChild(j).gameObject
                );
            }

            // Mezclamos aleatoriamente las plataformas.
            for (int j = 0; j < parts.Count; j++)
            {
                int randomIndex =
                    Random.Range(j, parts.Count);

                GameObject temp = parts[j];

                parts[j] = parts[randomIndex];
                parts[randomIndex] = temp;
            }

            // Desactivamos las plataformas que no se utilizarán.
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

        Debug.Log(
            "Total de niveles creados: " +
            spawnedLevels.Count
        );
    }
}
