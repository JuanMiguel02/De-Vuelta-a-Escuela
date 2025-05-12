using UnityEngine;
using TMPro;
public class Tiempo : MonoBehaviour
{
     private static Tiempo instancia;
    void Awake()
    {
        // Asegura que solo haya una instancia y que no se destruya
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

   [SerializeField] TextMeshProUGUI tiempoText;
   float cicloTiempo = 240f;
    void Update()
    {
        cicloTiempo += Time.deltaTime;
        int minutos = Mathf.FloorToInt(cicloTiempo / 60);
        int segundos = Mathf.FloorToInt(cicloTiempo % 60);
        tiempoText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
