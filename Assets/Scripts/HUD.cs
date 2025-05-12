using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    private GameManager gameManager;
    public TextMeshProUGUI puntos;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No se destruye entre escenas
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe uno
        }
    }

    void Start()
    {
        gameManager = GameManager.Instance;
         if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
    {
        gameObject.SetActive(false);
    }
    }

    void Update()
    {
        puntos.text = gameManager.PuntosTotales.ToString();
    }
}