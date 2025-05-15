using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject menuGameOver;
    [SerializeField] private AudioClip clipAlerta; // Clip de beep

    private float tiempoActual;
    private bool tiempoActivado = false;

    private bool sonidoAlertaIniciado = false;
    private float contadorSegundo = 0f;

    private void Start()
    {
        Time.timeScale = 1f;
        ActivarTemporizador();

        if (menuGameOver != null)
        {
            menuGameOver.SetActive(false);
        }
    }

    private void Update()
    {
        if (tiempoActivado)
        {
            CambiarContador();
        }
    }

    private void CambiarContador()
    {
        tiempoActual -= Time.deltaTime;

        if (tiempoActual >= 0)
        {
            slider.value = tiempoActual;

            // Iniciar el sonido de alerta cuando queden 10 segundos o menos
            if (tiempoActual <= 10f)
            {
                if (!sonidoAlertaIniciado)
                {
                    sonidoAlertaIniciado = true;
                    contadorSegundo = 0f; // Inicializar contador de segundo
                }

                contadorSegundo -= Time.deltaTime;
                if (contadorSegundo <= 0f)
                {
                    if (ControladorSonidos.Instance != null && clipAlerta != null)
                    {
                        ControladorSonidos.Instance.EjecutarSonido(clipAlerta);
                    }
                    contadorSegundo = 1f; // Reiniciar para el siguiente segundo
                }
            }
        }

        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            Debug.Log("Derrota");
            CambiarTemporizador(false);

            if (menuGameOver != null)
            {
                Time.timeScale = 0f;
                menuGameOver.SetActive(true);
            }

            SonidoGameOver sonidoGO = Object.FindAnyObjectByType<SonidoGameOver>();
            if (sonidoGO != null)
            {
                sonidoGO.EjecutarGameOver();
            }
        }
    }

    private void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        sonidoAlertaIniciado = false;
        CambiarTemporizador(true);
    }

    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }
}