using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales{ get { return puntosTotales;}}
    private int puntosTotales ;
    public void SumarPuntos(int puntosASumar){
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
