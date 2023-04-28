using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InterfazController : MonoBehaviour
{

    
    public static InterfazController instance;

   public TextMeshProUGUI PuntosfinalesVictoria;

   public TextMeshProUGUI RecordVictoria;


    List<GameObject> spritevidas = new List<GameObject>();

    public Vector2 posprimeravida;

    public GameObject spritevida;

    public GameObject pantalladerrota;

    public GameObject pantallaVictoria;

    public int plataformasRestantes;

    public TextMeshProUGUI PuntosfinalesDerrota;

   public TextMeshProUGUI RecordDerrota;

    public AudioClip victory;
    public AudioSource audioSource;
     
    public AudioClip gameoverAudioClip;

    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Awake()
    {
        if (instance !=  null && instance != this)
        {
            Destroy(this);

        }

        else

        {
            instance=this;
        }
    }

    public void setvidas(int vidas)
    {
        if (vidas>1)
        {
            Vector2 pos = posprimeravida;

            for (int i = 1; i<vidas; i++)
            {

                GameObject sprite = Instantiate(spritevida, pos, Quaternion.identity);

                spritevidas.Add(sprite);
                pos=new Vector2(pos.x +0.75f, pos.y);
            }

        }

    }
    public void perdervida()
    {

        if (spritevidas.Count>0)
        {
            spritevidas[spritevidas.Count-1].SetActive(false);
            spritevidas.RemoveAt(spritevidas.Count-1);

        }
        else
        {

            pantalladerrota.SetActive(true);
            PuntosfinalesDerrota.text = "puntuacion: "+ PuntuacionController.Instance.puntosTotales.ToString();
            RecordDerrota.text="record: "+ PlayerPrefs.GetInt("record"+ SceneManager.GetActiveScene().name,0).ToString();
            
            audioSource.Stop();
            audioSource.PlayOneShot(gameoverAudioClip); 
        }
    }
    public void reiniciarlevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    public void VolverAlMenuPrincipal()
    {
        
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1; 
    }
   

    public void MostrarPantallaVictoria()
    {
       pantallaVictoria.SetActive(true);
        PuntosfinalesVictoria.text = "puntuacion: "+ PuntuacionController.Instance.puntosTotales.ToString();
        RecordVictoria.text="record: "+ PlayerPrefs.GetInt("record"+ SceneManager.GetActiveScene().name,0).ToString();
         
         audioSource.Stop();
         audioSource.PlayOneShot(victory);
    }
  
}
