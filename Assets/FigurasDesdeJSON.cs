using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; // Asegúrate de tener Newtonsoft.Json en tu proyecto.

public class FigurasDesdeJSON : MonoBehaviour
{
    [System.Serializable]
    public class Figura
    {
        public List<List<float>> forma_1;
        public List<List<float>> forma_2;
        public List<List<float>> forma_3;
        public List<List<float>> forma_4;
        public List<List<float>> forma_5;
    }

    public TextAsset jsonFile;

    // Ajustes para generar las figuras en relación a la cámara.
    public Camera mainCamera; 
    public float escala = 0.01f; // Escala para ajustar las coordenadas JSON al mundo.
    public Vector2 offset = new Vector2(-10f, 5f); // Desplazamiento fijo: X = -10, Y = -5.

    void Start()
    {
        if (jsonFile != null)
        {
            if (mainCamera == null)
                mainCamera = Camera.main; // Usar la cámara principal si no se asignó una.

            var figuras = JsonConvert.DeserializeObject<Figura>(jsonFile.text);

            CrearFigura(ConvertirAListaVector2(figuras.forma_1), "Forma 1");
            CrearFigura(ConvertirAListaVector2(figuras.forma_2), "Forma 2");
            CrearFigura(ConvertirAListaVector2(figuras.forma_3), "Forma 3");
            CrearFigura(ConvertirAListaVector2(figuras.forma_4), "Forma 4");
            CrearFigura(ConvertirAListaVector2(figuras.forma_5), "Forma 5");
        }
    }

    List<Vector2> ConvertirAListaVector2(List<List<float>> coordenadas)
    {
        List<Vector2> lista = new List<Vector2>();

        foreach (var punto in coordenadas)
        {
            if (punto.Count == 2) // Asegurarse de que hay exactamente dos valores: x e y.
            {
                // Convertir las coordenadas aplicando la escala, el offset y corrigiendo el eje Y.
                lista.Add(new Vector2(
                    punto[0] * escala + offset.x,
                    -punto[1] * escala + offset.y // Invertir el eje Y
                ));
            }
        }

        return lista;
    }

    void CrearFigura(List<Vector2> puntos, string nombre)
    {
        // Crear el GameObject
        GameObject figura = new GameObject(nombre);

        // Posicionar la figura en el espacio del mundo relativo a la cámara.
        figura.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);

        // Asignar el layer "Ground" (asegúrate de que el layer "Ground" existe)
        figura.layer = LayerMask.NameToLayer("ground");

        // Crear el Mesh
        MeshFilter meshFilter = figura.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = figura.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default")); // Usar un shader estándar.

        // Crear el Mesh
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        // Convertir puntos a Vector3
        Vector3[] vertices = new Vector3[puntos.Count];
        for (int i = 0; i < puntos.Count; i++)
        {
            vertices[i] = new Vector3(puntos[i].x, puntos[i].y, 0);
        }

        // Generar triángulos (Triangulación simple)
        int[] triangles = Triangulate(puntos);

        // Asignar datos al Mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Crear y configurar el RigidBody2D con BodyType.Static
        Rigidbody2D rb2d = figura.AddComponent<Rigidbody2D>(); // Corregido el nombre
        rb2d.bodyType = RigidbodyType2D.Static;

        // Configurar Collider
        PolygonCollider2D collider = figura.AddComponent<PolygonCollider2D>();
        collider.points = puntos.ToArray();
    }

    int[] Triangulate(List<Vector2> puntos)
    {
        // Usa una librería de triangulación como "Triangle.NET" para casos complejos.
        // Para este ejemplo simple, generamos triángulos en orden.
        List<int> triangles = new List<int>();

        for (int i = 1; i < puntos.Count - 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        return triangles.ToArray();
    }
}
