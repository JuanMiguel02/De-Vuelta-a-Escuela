using UnityEngine;
using TMPro;

public class ResultadosFinal : MonoBehaviour
{
    public TextMeshProUGUI textoPuntosFinales;
    public TextMeshProUGUI tiempoFinal;

    private void Start()
    {
        if (textoPuntosFinales != null)
        {
            textoPuntosFinales.text = "Puntos totales: " + GameManager.Instance.PuntosTotales;

        }
        else
        {
            Debug.LogWarning("No se ha asignado un TextMeshProUGUI en ResultadosFinal");
        }
        if (tiempoFinal != null)
        {
            float tiempoTotal= GameManager.Instance.TiempoAcumulado;
            tiempoFinal.text = "Tiempo jugado: " + FormatearTiempo(tiempoTotal);
        }
    }
     private string FormatearTiempo(float tiempoEnSegundos)
    {
        int minutos = Mathf.FloorToInt(tiempoEnSegundos / 60);
        int segundos = Mathf.FloorToInt(tiempoEnSegundos % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}