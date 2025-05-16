using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    private AudioSource musica;

    private bool juegoPausado = false;

    private void Start()
    {
        // Intenta obtener el AudioSource de la cámara principal automáticamente
        if (musica == null && Camera.main != null)
        {
            musica = Camera.main.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);

        if (musica != null)
        {
            musica.Pause();
        }
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);

        if (musica != null)
        {
            musica.UnPause();
        }
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        int indiceActual = SceneManager.GetActiveScene().buildIndex;

        if (GameManager.Instance != null && indiceActual == 2)
        {
            GameManager.Instance.ReiniciarPuntos();
        }
        if (HUD.Instance != null) {
            Destroy(HUD.Instance.gameObject);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Cerrar()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReiniciarPuntos();
        }

        if (HUD.Instance != null)
        {
            Destroy(HUD.Instance.gameObject);
        }
        
        SceneManager.LoadScene(0); // Cargar el menú principal
    }
}