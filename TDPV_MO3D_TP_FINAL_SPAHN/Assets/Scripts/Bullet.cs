using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float tiempoDeVida = 5f; // Tiempo en segundos antes de que se destruya autom�ticamente
    public float da�o = 10f; // Da�o que causa el proyectil al jugador
    public AudioClip bulletSound; // Clip de sonido para el proyectil
    private AudioSource audioSource;
    public float volumenSonido = 1.5f; // Control del volumen de sonido

    public GameObject enemigoDisparador; // Referencia al enemigo que dispar� el proyectil
    public GameObject jugadorDisparador; // Referencia al jugador que dispar� el proyectil


    void Start()
    {
        // Agrego el componente AudioSource al proyectil y configura el sonido
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bulletSound;
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f; // Haco que el sonido sea 3D 
        audioSource.volume = volumenSonido; // Ajusto el volumen del sonido
        audioSource.Play(); // Reproduzco el sonido al instanciar
        
        Destroy(gameObject, tiempoDeVida);// Destruyo el proyectil despu�s de un tiempo si no impacta
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifico si el proyectil golpea al jugador
        if (collision.gameObject.CompareTag("Player") && collision.gameObject != jugadorDisparador)
        {
            // Aplicoa da�o al jugador
            collision.gameObject.GetComponent<player_movement>().TakeDamage(da�o);

            // Destruiyo el proyectil inmediatamente despu�s de impactar
            Destroy(gameObject);
        }
        // Verifico si el proyectil golpea al enemigo
        else if (collision.gameObject.CompareTag("Enemy") && collision.gameObject != enemigoDisparador)
        {
            collision.gameObject.GetComponent<enemy>().Damage(da�o);
            Destroy(gameObject);
        }
        else
        {
            // Destruyo el proyectil si golpea cualquier otro objeto
            Destroy(gameObject);
        }
    }
}
