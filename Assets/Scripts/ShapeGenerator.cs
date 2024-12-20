using System.Collections.Generic;
using UnityEngine;

public class GenerarObjetosDesdeJSON : MonoBehaviour
{
    // Clase para deserializar el JSON
    [System.Serializable]
    public class Formas
    {
        public Dictionary<string, List<Vector2>> formas;
    }

    public TextAsset jsonFile; // Archivo JSON cargado como recurso
    public GameObject prefabBase; // Prefab base para los objetos (debe tener RigidBody2D y PolygonCollider2D configurados)
    public float escala = 0.01f; // Factor de escala para ajustar las coordenadas al tamaño de la cámara

    void Start()
    {
        if (jsonFile == null)
        {
            Debug.LogError("Por favor asigna el archivo JSON.");
            return;
        }

        if (prefabBase == null)
        {
            Debug.LogError("Por favor asigna el prefab base.");
            return;
        }

        // Leer y deserializar el JSON
        Formas formas = JsonUtility.FromJson<Formas>("{\"formas\":" + jsonFile.text + "}");
        if (formas.formas == null)
        {
            Debug.LogError("No se pudieron cargar las formas del archivo JSON.");
            return;
        }

        // Crear objetos a partir de las formas
        foreach (var forma in formas.formas)
        {
            CrearObjeto(forma.Value);
        }
    }

    void CrearObjeto(List<Vector2> puntos)
    {
        // Convertir las coordenadas de píxeles a unidades del mundo (ajustar escala)
        Vector2[] puntosEscalados = new Vector2[puntos.Count];
        for (int i = 0; i < puntos.Count; i++)
        {
            puntosEscalados[i] = new Vector2(
                puntos[i].x * escala,
                puntos[i].y * escala * -1 // Invertir el eje Y para adaptarse al sistema de Unity
            );
        }

        // Calcular el centro del objeto
        Vector2 centro = CalcularCentro(puntosEscalados);

        // Crear el objeto
        GameObject objeto = Instantiate(prefabBase, centro, Quaternion.identity);
        objeto.name = "Forma";
        objeto.layer = LayerMask.NameToLayer("Ground");

        // Configurar el PolygonCollider2D
        PolygonCollider2D collider = objeto.GetComponent<PolygonCollider2D>();
        if (collider != null)
        {
            collider.points = puntosEscalados;
        }
    }

    Vector2 CalcularCentro(Vector2[] puntos)
    {
        // Calcular el centroide del polígono
        Vector2 centro = Vector2.zero;
        foreach (var punto in puntos)
        {
            centro += punto;
        }
        return centro / puntos.Length;
    }
}
