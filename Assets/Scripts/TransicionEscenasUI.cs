using UnityEngine;
using UnityEngine.SceneManagement;
public class TransicionEscenasUI : MonoBehaviour
{
    public static TransicionEscenasUI Instance;

    [Header("Disolver")]
    public CanvasGroup disolverCanvasGroup;
    public float tiempoDisolverEntrada; 
    
    public float tiempoDisolverSalida;

    [Header("Bloque")]
    public RectTransform bloqueObject;

    public float tiempoBloqueEntrada;

    public float tiempoBloqueSalida;

    public LeanTweenType bloqueEaseEntrada;

    public LeanTweenType bloqueEaseSalida;

    public float posicionFinalEntrada;

    public float posicionInicialSalida;

    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else{
            Destroy(gameObject);
        }
        }

    private void Start()
    {
        BloqueEntrada();
    }

    private void DisolverEntrada()
    {
     LeanTween.alphaCanvas(disolverCanvasGroup, 0f, tiempoDisolverEntrada).setOnComplete(() =>
     {
            disolverCanvasGroup.blocksRaycasts = false; // Desactiva el canvas después de la disolución
            disolverCanvasGroup.interactable = false; // Desactiva el objeto del canvas

     });
    }

    public void DisolverSalida(int indexEscena)
    {
        disolverCanvasGroup.blocksRaycasts = true; 
        disolverCanvasGroup.interactable = true; 

        LeanTween.alphaCanvas(disolverCanvasGroup, 1f, tiempoDisolverSalida).setOnComplete(() =>
        {
            SceneManager.LoadScene(indexEscena);
        });

    }

    private void BloqueEntrada(){
        LeanTween.moveX(bloqueObject, posicionFinalEntrada, tiempoBloqueEntrada).setEase(bloqueEaseEntrada).setOnComplete(() =>
        {
            bloqueObject.gameObject.SetActive(false); // Desactiva el objeto después de la animación
        });
    }

    public void BloqueSalida(int indexEscena){
        bloqueObject.anchoredPosition = new Vector2 (posicionInicialSalida, 0f);

        bloqueObject.gameObject.SetActive(true);

        LeanTween.moveX(bloqueObject, 0f, tiempoBloqueSalida).setEase(bloqueEaseSalida).setOnComplete(() =>
        {
            SceneManager.LoadScene(indexEscena);
        });
    }
}
