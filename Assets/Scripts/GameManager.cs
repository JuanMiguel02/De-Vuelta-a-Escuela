using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int puntosTotales;
    private int puntosRespaldo;

    public int PuntosTotales
    {
        get => puntosTotales;
        set => puntosTotales = value;
    }

    private void Awake()
    {
        // Singleton: se asegura de que solo exista un GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Conserva el objeto entre escenas
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe uno
        }
    }

    // Método público para sumar puntos desde cualquier otro script
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log("Puntos totales: " + puntosTotales);
    }

    // (Opcional) Método para resetear los puntos
    public void ReiniciarPuntos()
    {
        puntosTotales = 0;
    }
    public void GuardarPuntosDeRespaldo()
    {
        puntosRespaldo = puntosTotales;
    }
    public void RestaurarPuntosDesdeRespaldo()
    {
        puntosTotales = puntosRespaldo;
    }
}