using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ControladorJuego : MonoBehaviour
{
    [SerializeField] private float tiempoMaximo;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject menuGameOver;
    private float tiempoActual;
    private bool tiempoActivado = false;

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
        CambiarTemporizador(true);
    }

    public void DesactivarTemporizador()
    {
        CambiarTemporizador(false);
    }
}
