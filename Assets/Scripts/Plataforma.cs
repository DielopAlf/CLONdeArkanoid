using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public GameObject [] powerups;
    public int puntos = 10;
    public int GolpesparaRomperse = 1; 
    private int GolpesDados = 0; 
    [Range(0,1)]
    public float probabilidad=0.1f;
    public void HitByBall(bool instantDestroy)
    {
        GolpesDados++;
        if (GolpesDados >= GolpesparaRomperse || instantDestroy == true)
        {
            DestroyPlatform();
        }
    }

   private void DestroyPlatform()
{
    PuntuacionController.Instance.AgregarPuntos(puntos);
    ControladorVictoria.Instance.PlataformaDestruida();
    
    if(powerups.Length>0)
    {

        if(Random.value<probabilidad)
        {
            int powerrandom = Random.Range(0,powerups.Length);

            Instantiate(powerups[powerrandom],transform.position,Quaternion.identity);

        }

    }

    
    Destroy(gameObject);
}
}