using UnityEngine;

public class MaterialInteraction : MonoBehaviour
{
    public GameObject targetObject; // The GameObject whose material color we want to change
    public WhiteboardMarker2 marker; // Reference to the WhiteboardMarker script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "mat"
        if (other.CompareTag("mat"))
        {
            Renderer matRenderer = other.GetComponent<Renderer>();

            if (matRenderer != null)
            {
                Color newColor = matRenderer.material.color;
                Renderer targetRenderer = targetObject.GetComponent<Renderer>();
                
                if (targetRenderer != null)
                {
                    Material targetMaterialInstance = targetRenderer.material;
                    targetMaterialInstance.color = newColor;

                    // Update the marker's color to match the new color
                    marker.UpdatePenColor();
                }
            }
        }
    }
}
