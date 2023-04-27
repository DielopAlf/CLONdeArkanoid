using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaLenta : MonoBehaviour
{
    public float speed;
    public float tiempoVida = 10f;
    public AudioClip powerUp;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, tiempoVida);
    }

    void Update()
    {
        transform.position += new Vector3(0, Time.deltaTime * speed * -1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<MovPersonaje>().pelotalenta();
            audioSource.PlayOneShot(powerUp);
            Destroy(gameObject);
        }
    }
}