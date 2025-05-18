using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject); // Elimina duplicados si ya existe uno
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int index = scene.buildIndex;

        if (index > 4)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
        else
        {

            gameObject.SetActive(index != 0);
        }
        if (index == 1)
        {
            GameManager.Instance.ReiniciarPuntos();
        }

    }

    void Start()
    {
       gameManager = GameManager.Instance;
    }

    void Update()
    {
        if (gameManager != null)
        {
            puntos.text = gameManager.PuntosTotales.ToString();
        }

    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
}