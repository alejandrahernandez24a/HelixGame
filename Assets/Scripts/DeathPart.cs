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
           
            }
            else
            {
                Debug.LogWarning("El material no tiene propiedad _Color: " + rend.gameObject.name);
            }
        }

      
    }
}