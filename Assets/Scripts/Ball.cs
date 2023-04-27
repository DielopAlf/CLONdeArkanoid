using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector3 direccion;
    [SerializeField] float velocidad = 2f;
    public int vidas = 3;
   
    public float anguloMaximo = 0.7f;
    public Vector2 posicionInicial;
    public bool activada;
    public float esperaInicial = 2f;
    float VelocidadInicial;
    public bool destructora;
    private float velocidadprevia;
    public AudioClip powerUp;
    public PuntuacionController puntuacionController;
    public AudioClip reboteAudioClip;
    public AudioClip loselifeAudioClip;
    public AudioClip choqueplataformaAudioClip;
    public AudioSource audioSource;
    private bool pelotaLentaActiva;
    
    void Start()
    {
        StartCoroutine(ResetPelota());
        InterfazController.instance.setvidas(vidas);
       
        puntuacionController = PuntuacionController.Instance;
        destructora = false;
        pelotaLentaActiva = false;
        VelocidadInicial = velocidad;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (activada)
        {
            transform.position += direccion * Time.deltaTime * velocidad;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            direccion.y = direccion.y * -1f;
            direccion.x = Random.Range(-anguloMaximo, anguloMaximo);
            audioSource.PlayOneShot(reboteAudioClip);
        }
        else if (collision.gameObject.tag == "Muro")
        {
            direccion.x = direccion.x * -1f;
            audioSource.PlayOneShot(reboteAudioClip);
        }
        else if (collision.gameObject.tag == "Techo")
        {
            direccion.y = direccion.y * -1f;
            direccion.x = Random.Range(-anguloMaximo, anguloMaximo);
            audioSource.PlayOneShot(reboteAudioClip);
        }
        else if (collision.gameObject.tag == "Plataforma")
        {
	    
	    if(destructora == false)
            {
                direccion.y = direccion.y * -1f;
                direccion.x = Random.Range(-anguloMaximo, anguloMaximo);
 	    }

            collision.gameObject.GetComponent<Plataforma>().HitByBall(destructora);
             audioSource.PlayOneShot(choqueplataformaAudioClip);
        }
        else if (collision.gameObject.CompareTag("Zona Muerte"))
        {
            Muerte();
            audioSource.PlayOneShot(loselifeAudioClip);
        }
    }
      
    

    public IEnumerator ResetPelota()
    {
        activada = false;
        direccion = new Vector2(0, 0);
        transform.position = posicionInicial;
        yield return new WaitForSeconds(esperaInicial);
        DireccionInicial();
        activada = true;
    }
    public void DireccionInicial()
    {

        float dirx = Random.Range(-anguloMaximo, anguloMaximo);
        direccion = new Vector2(dirx, 1);
    }

    public void Muerte()
    {
        if (vidas > 1)
        {
            vidas -= 1;
            InterfazController.instance.perdervida();
            StartCoroutine(ResetPelota());
        }
        else
        {
            PuntuacionController.Instance.GuardarRecord();

            InterfazController.instance.perdervida();
            activada = false;
            gameObject.SetActive(false);
             pelotaLentaActiva = false;
             destructora = false;
           
        }

    }

    public IEnumerator pelotalenta()
{
    audioSource.PlayOneShot(powerUp);
    if (pelotaLentaActiva)
    {
        yield break; 
    }
    
    pelotaLentaActiva = true;
    float velocidadprevia = velocidad;
    velocidad = velocidadprevia / 2;
    
    yield return new WaitForSeconds(10f);
    
    velocidad = velocidadprevia;
    pelotaLentaActiva = false;
}


    public  IEnumerator pelotadestructora()
    {
     audioSource.PlayOneShot(powerUp);
     if (destructora)
    {
        yield break; 
    }
       

        destructora = true;
        yield return new WaitForSeconds(10f);
	    destructora = false;
    }
}