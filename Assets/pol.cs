using UnityEngine;

public class CameraPixelSize : MonoBehaviour
{
    void Start()
    {
        // Obtener la cámara principal
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            // Obtener el tamaño en píxeles
            Rect pixelRect = mainCamera.pixelRect;

            Debug.Log($"Ancho en píxeles: {pixelRect.width}");
            Debug.Log($"Alto en píxeles: {pixelRect.height}");
        }
        else
        {
            Debug.LogError("No se encontró la cámara principal.");
        }
    }
}
