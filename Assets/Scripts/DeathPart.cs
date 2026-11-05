using UnityEngine;

public class DeathPart : MonoBehaviour
{
    private void OnEnable()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            Material material = rend.material;

            if (material.HasProperty("_Color"))
            {
                material.color = Color.red;
                Debug.Log("Color rojo aplicado a: " + rend.gameObject.name);
            }
            else
            {
                Debug.LogWarning("El material no tiene propiedad _Color: " + rend.gameObject.name);
            }
        }

        Debug.Log("Plataforma peligrosa: " + gameObject.name);
    }
}