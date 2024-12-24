using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ApplyMAterialOnTouch : MonoBehaviour
{
    public Material materialToApply;   // Le matériau à appliquer au cube
    public GameObject targetCube;      // Le cube cible

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l’objet en collision est l’utilisateur ou un contrôleur
        if (other.CompareTag("LeftHand") || other.CompareTag("RightHand"))
        {
            // Applique le matériau de cette sphère au cube
            Renderer cubeRenderer = targetCube.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material = materialToApply;
            }
        }
    }
}


