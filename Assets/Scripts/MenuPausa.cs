using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    
    private bool juegoPausado = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(juegoPausado){
                Reanudar();
            }else{
                Pausa();
            }
        }
    }
    public void Pausa(){
        juegoPausado = true;
        Time.timeScale = 0f; 
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void Reanudar(){
        juegoPausado = false;
        Time.timeScale = 1f; 
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Cerrar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
         Time.timeScale = 1f; // Reanuda por si estaba pausado

    if (GameManager.Instance != null)
    {
        GameManager.Instance.ReiniciarPuntos(); // Reinicia los puntos al salir
    }

    // Elimina el HUD persistente para que se cree de nuevo en la próxima partida
    if (HUD.Instance != null)
    {
        Destroy(HUD.Instance.gameObject);
    }

    SceneManager.LoadScene(0); // Asumiendo que la escena 0 es el menú principal
    }
}
