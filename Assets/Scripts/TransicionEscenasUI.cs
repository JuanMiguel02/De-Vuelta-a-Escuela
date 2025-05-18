using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscenasUI : MonoBehaviour
{
    // Singleton para que cualquier script pueda acceder a esta clase fácilmente
    public static TransicionEscenasUI Instance;

    [Header("Disolver")]
    // Grupo de Canvas que se va a desvanecer
    public CanvasGroup disolverCanvasGroup;

    // Tiempo que tarda en desvanecerse al entrar
    public float tiempoDisolverEntrada;

    // Tiempo que tarda en desvanecerse al salir
    public float tiempoDisolverSalida;

    [Header("Bloque")]
    // Objeto UI que se desliza como bloque de transición
    public RectTransform bloqueObject;

    // Duración de la animación de entrada del bloque
    public float tiempoBloqueEntrada;

    // Duración de la animación de salida del bloque
    public float tiempoBloqueSalida;

    // Tipo de interpolación para la entrada del bloque
    public LeanTweenType bloqueEaseEntrada;

    // Tipo de interpolación para la salida del bloque
    public LeanTweenType bloqueEaseSalida;

    // Posición final del bloque cuando entra
    public float posicionFinalEntrada;

    // Posición inicial del bloque cuando sale
    public float posicionInicialSalida;

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Si ya hay una instancia, se destruye esta para evitar duplicados
        }
    }

    private void Start()
    {
        // Llama a la animación de entrada del bloque cuando empieza la escena
        BloqueEntrada();
    }

    // Método privado que hace una animación de desvanecimiento de entrada
    private void DisolverEntrada()
    {
        LeanTween.alphaCanvas(disolverCanvasGroup, 0f, tiempoDisolverEntrada).setOnComplete(() =>
        {
            // Al completar la animación, desactiva interacción con el Canvas
            disolverCanvasGroup.blocksRaycasts = false;
            disolverCanvasGroup.interactable = false;
        });
    }

    // Método público que inicia la animación de desvanecimiento de salida y cambia de escena
    public void DisolverSalida(int indexEscena)
    {
        // Activa la interacción con el Canvas durante la salida
        disolverCanvasGroup.blocksRaycasts = true;
        disolverCanvasGroup.interactable = true;

        // Desvanece el Canvas y al completar, cambia de escena
        LeanTween.alphaCanvas(disolverCanvasGroup, 1f, tiempoDisolverSalida).setOnComplete(() =>
        {
            SceneManager.LoadScene(indexEscena);
        });
    }

    // Animación de entrada del bloque desde un lado de la pantalla
    private void BloqueEntrada()
    {
        LeanTween.moveX(bloqueObject, posicionFinalEntrada, tiempoBloqueEntrada).setEase(bloqueEaseEntrada).setOnComplete(() =>
        {
            // Desactiva el objeto después de completar la animación
            bloqueObject.gameObject.SetActive(false);
        });
    }

    // Animación de salida del bloque y cambio de escena
    public void BloqueSalida(int indexEscena)
    {
        // Coloca el bloque en su posición inicial antes de animar
        bloqueObject.anchoredPosition = new Vector2(posicionInicialSalida, 0f);

        // Activa el bloque para que sea visible
        bloqueObject.gameObject.SetActive(true);

        // Mueve el bloque al centro de la pantalla y luego cambia la escena
        LeanTween.moveX(bloqueObject, 0f, tiempoBloqueSalida).setEase(bloqueEaseSalida).setOnComplete(() =>
        {
            SceneManager.LoadScene(indexEscena);
        });
    }
}
