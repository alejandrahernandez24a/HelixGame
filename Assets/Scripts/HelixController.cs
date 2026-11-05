
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastMousePosition;
    private bool isDragging = false;

    public float rotationSpeed = 0.5f;

    void Update()
    {
        // Cuando presionamos el botón izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }

        // Mientras arrastramos el mouse
        if (Input.GetMouseButton(0) && isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;

            float distance = currentMousePosition.x - lastMousePosition.x;

            transform.Rotate(Vector3.up, distance * rotationSpeed, Space.World);

            lastMousePosition = currentMousePosition;
        }

        // Cuando soltamos el botón
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
