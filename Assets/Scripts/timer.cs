using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Asocia aquí el componente TMP del objeto Text
    private float elapsedTime;

    void Start()
    {
        elapsedTime = 0f; // Inicia el tiempo en cero
    }

    void Update()
    {
        elapsedTime += Time.deltaTime; // Incrementa el tiempo transcurrido
        UpdateTimerUI(); // Actualiza el texto
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000F) % 1000F);

        // Si el tiempo es menor a 1 minuto, muestra solo segundos y milisegundos
        if (minutes > 0)
        {
            timerText.text = $"{minutes}:{seconds:00}:{milliseconds:000}";
        }
        else
        {
            timerText.text = $"{seconds}:{milliseconds:000}";
        }
    }
}
