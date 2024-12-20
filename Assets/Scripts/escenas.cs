using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlEscenaYSalida : MonoBehaviour
{
    [SerializeField] private Button botonCambiarEscena; // Botón para cambiar de escena
    [SerializeField] private Button botonSalir;        // Botón para salir de la aplicación
    [SerializeField] private string nombreEscena;      // Nombre de la escena a cargar

    void Start()
    {
        // Configurar el botón para cambiar de escena
        if (botonCambiarEscena != null && !string.IsNullOrEmpty(nombreEscena))
            botonCambiarEscena.onClick.AddListener(() => CambiarEscena(nombreEscena));

        // Configurar el botón para salir de la aplicación
        if (botonSalir != null)
            botonSalir.onClick.AddListener(SalirAplicacion);
    }

    void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena); // Cargar la escena especificada
    }

    void SalirAplicacion()
    {
        Application.Quit(); // Salir de la aplicación
        Debug.Log("La aplicación se ha cerrado"); // Mensaje para verificar en el Editor
    }
}
