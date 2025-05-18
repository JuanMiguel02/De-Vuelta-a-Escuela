using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControladorJuego : MonoBehaviour
{
    // Tiempo total disponible para el jugador
    [SerializeField] private float tiempoMaximo;

    // Barra de progreso que representa el tiempo restante
    [SerializeField] private Slider slider;

    // Menú que aparece cuando se acaba el tiempo (Game Over)
    [SerializeField] private GameObject menuGameOver;

    // Clip de audio para alerta (beep)
    [SerializeField] private AudioClip clipAlerta;

    // Tiempo acumulado por el jugador durante la partida
    private float tiempoJugado = 0f;

    // Tiempo restante actual
    private float tiempoActual;

    // Estado que indica si el temporizador está activo
    private bool tiempoActivado = false;

    // Controla si ya se ha iniciado la alerta sonora
    private bool sonidoAlertaIniciado = false;

    // Control para emitir un sonido de alerta cada segundo
    private float contadorSegundo = 0f;

    // Patrón Singleton para acceder a esta clase desde otros scripts
    public static ControladorJuego Instance { get; internal set; }

    private void Start()
    {
        Time.timeScale = 1f; // Asegura que el juego inicie a velocidad normal
        ActivarTemporizador(); // Comienza el temporizador

        // Asegura que el menú de Game Over no esté visible al inicio
        if (menuGameOver != null)
        {
            menuGameOver.SetActive(false);
        }
    }

    private void Update()
    {
        // Si el temporizador está activo, incrementa el tiempo jugado y actualiza la cuenta regresiva
        if (tiempoActivado)
        {
            tiempoJugado += Time.deltaTime;
            CambiarContador();
        }
    }

    // Lógica que actualiza el temporizador y controla alertas y Game Over
    private void CambiarContador()
    {
        // Resta tiempo según el tiempo real transcurrido
        tiempoActual -= Time.deltaTime;

        if (tiempoActual >= 0)
        {
            // Actualiza el valor visual del slider (barra de tiempo)
            slider.value = tiempoActual;

            // Si quedan 10 segundos o menos, activa sonido de alerta cada segundo
            if (tiempoActual <= 10f)
            {
                if (!sonidoAlertaIniciado)
                {
                    sonidoAlertaIniciado = true;
                    contadorSegundo = 0f;
                }

                contadorSegundo -= Time.deltaTime;

                if (contadorSegundo <= 0f)
                {
                    // Ejecuta el sonido de alerta si hay sonido y controlador
                    if (ControladorSonidos.Instance != null && clipAlerta != null)
                    {
                        ControladorSonidos.Instance.EjecutarSonido(clipAlerta);
                    }
                    contadorSegundo = 1f; // Reinicia el contador para repetir cada segundo
                }
            }
        }

        // Si el tiempo llegó a cero, termina el juego
        if (tiempoActual <= 0)
        {
            tiempoActual = 0;
            Debug.Log("Derrota");
            CambiarTemporizador(false); // Detiene el temporizador

            // Guarda el tiempo total jugado en el GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.TiempoTotalJugado = tiempoJugado;
            }

            // Muestra el menú de Game Over y pausa el juego
            if (menuGameOver != null)
            {
                Time.timeScale = 0f;
                menuGameOver.SetActive(true);
            }

            // Ejecuta el sonido o efecto de Game Over si existe
            SonidoGameOver sonidoGO = Object.FindAnyObjectByType<SonidoGameOver>();
            if (sonidoGO != null)
            {
                sonidoGO.EjecutarGameOver();
            }
        }
    }

    // Cambia el estado del temporizador (activar o desactivar)
    private void CambiarTemporizador(bool estado)
    {
        tiempoActivado = estado;
    }

    // Inicia y configura el temporizador
    public void ActivarTemporizador()
    {
        tiempoActual = tiempoMaximo;
        slider.maxValue = tiempoMaximo;
        sonidoAlertaIniciado = false;
        CambiarTemporizador(true);
    }

    // Detiene el temporizador
    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }

    // Devuelve el tiempo jugado hasta el momento
    public float ObtenerTiempoJugado()
    {
        return tiempoJugado;
    }
}
