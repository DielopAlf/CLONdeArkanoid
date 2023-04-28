using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject menuInicial;
    public AudioSource audioSource;
    public AudioClip interfazaudio;


    void Start()
    {
        menuInicial.SetActive(true);

       
        audioSource = GameObject.Find("controllador").GetComponent<AudioSource>();
    }

    public void cerrarJuego()
    {
        Application.Quit();

        
       audioSource.PlayOneShot(interfazaudio);
    }

    public void botonplay()
    {
       
        menuInicial.SetActive(false);
        audioSource.PlayOneShot(interfazaudio);
        

        
    }

    public void volver()
    {
        menuInicial.SetActive(true);

        
         audioSource.PlayOneShot(interfazaudio);
    }
}