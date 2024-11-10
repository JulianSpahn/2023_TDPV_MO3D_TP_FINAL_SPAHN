using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{

    // Atributos de vida y daño
    [SerializeField] private float health = 100f;

    public Transform[] puntosDePatrulla; // Puntos por los que patrulla el enemigo
    public float rangoDeteccion = 10f; // Rango para detectar al jugador
    public float rangoAtaque = 3f; // Rango para lanzar proyectiles
    public Transform jugador; // Referencia al transform del jugador
    public GameObject proyectilPrefab; // Prefab del proyectil
    public float velocidadProyectil = 10f; // Velocidad del proyectil
    public float tiempoEntreDisparos = 2f; // Tiempo entre cada disparo
    public Transform puntoDisparo; // Punto en la punta del arma
    public Animator animator; // Objeto para animacion
    private NavMeshAgent agente; // Ia de Navegacion
    private int puntoActual = 0; // Contador para saber por que punto del camino va
    private bool jugadorDetectado = false; // Booleano para saber si detecté al jugador
    private float tiempoUltimoDisparo; // Tiempo entre disparos

    // Inicio algunas variables
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        tiempoUltimoDisparo = Time.time;
        IrAlSiguientePuntoDePatrulla();
    }

    void Update()
    {

        float distanciaJugador = Vector3.Distance(transform.position, jugador.position);

        if (distanciaJugador <= rangoDeteccion)
        {
            jugadorDetectado = true;
            animator.SetBool("found_player", true);
        }
        else
        {
            jugadorDetectado = false;
            animator.SetBool("found_player", false);
        }

        if (jugadorDetectado)
        {
            if (distanciaJugador <= rangoAtaque)
            {
                // Lanza proyectiles al jugador
                animator.SetTrigger("Fire");
                AtacarJugador();
            }
            else
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
                {
                    agente.isStopped = false;
                    // Persigue al jugador
                    agente.SetDestination(jugador.position);
                }

            }
        }
        else
        {
            // Patrulla por la zona
            Patrullar();
        }
    }
    // Funcion que se encarga de la patrulla del enemigo
    void Patrullar()
    {
        if (!agente.pathPending && agente.remainingDistance < 0.5f)
        {
            IrAlSiguientePuntoDePatrulla();
        }
    }
    // Funcion para que el enemigo vaya de punto A a punto B
    void IrAlSiguientePuntoDePatrulla()
    {
        if (puntosDePatrulla.Length == 0)
            return;

        agente.destination = puntosDePatrulla[puntoActual].position;
        puntoActual = (puntoActual + 1) % puntosDePatrulla.Length;
    }
    // Funcion para saber cuando se debe atacar al jugador
    void AtacarJugador()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
        {
            agente.isStopped=true;
            if (Time.time >= tiempoUltimoDisparo + tiempoEntreDisparos)
            {
                tiempoUltimoDisparo = Time.time;
                DispararProyectil();
            }
        }
        else
        {
            agente.isStopped = false;
        }

    }

    // Funcion para disparar desde el arma
    void DispararProyectil()
    {
        if (puntoDisparo != null)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);

            Bullet bulletScript = proyectil.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.enemigoDisparador = this.gameObject; // Asignamos el enemigo que disparó el proyectil
            }

            Rigidbody rb = proyectil.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direccion = (jugador.position - puntoDisparo.position).normalized;
                rb.velocity = direccion * velocidadProyectil;
            }
        }
        else
        {
            Debug.LogWarning("no asignaste el punto de disparo");
        }



    }

    // Funcion para recibir daño
    public void Damage(float amount_of_damage)
    {
        health -= amount_of_damage;
        if (health <= 0)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.EnemigoEliminado();
            }
            Die();
        }
    }

    // Funcion para destruir al enemigo si muere
    void Die()
    {
        Destroy(gameObject);
    }


}
