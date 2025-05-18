using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton: se utiliza para asegurar que haya una única instancia global de esta clase.
    // Esto permite acceder fácilmente a sus datos y métodos desde cualquier otro script.
    public static GameManager Instance;

    // Variable pública que puede usarse para medir el tiempo total jugado
    public float TiempoTotalJugado = 0f;

    // Puntos acumulados en el juego
    private int puntosTotales;

    // Respaldo de puntos, útil para restaurar el puntaje si se requiere
    private int puntosRespaldo;

    // Propiedad para acceder/modificar los puntos totales desde otros scripts
    public int PuntosTotales
    {
        get => puntosTotales;
        set => puntosTotales = value;
    }

    // Awake se ejecuta antes de Start.
    // Aquí se implementa el patrón Singleton.
    private void Awake()
    {
        // Singleton: si no hay una instancia activa, se asigna esta y se marca para no destruir
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
        }
        else
        {
            // Si ya hay una instancia, se destruye este objeto duplicado
            Destroy(gameObject);
        }
    }

    // Suma puntos al total actual y muestra el resultado en consola
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log("Puntos totales: " + puntosTotales);
    }

    // Reinicia los puntos totales a cero
    public void ReiniciarPuntos()
    {
        puntosTotales = 0;
    }

    // Guarda el estado actual de los puntos como respaldo
    public void GuardarPuntosDeRespaldo()
    {
        puntosRespaldo = puntosTotales;
    }

    // Restaura los puntos al valor previamente guardado en el respaldo
    public void RestaurarPuntosDesdeRespaldo()
    {
        puntosTotales = puntosRespaldo;
    }

    // Propiedad que acumula tiempo (puede usarse para estadísticas o puntuación basada en tiempo)
    public float TiempoAcumulado { get; set; } = 0f;

    // Suma tiempo al total acumulado
    public void SumarTiempo(float tiempo)
    {
        TiempoAcumulado += tiempo;
    }
}
