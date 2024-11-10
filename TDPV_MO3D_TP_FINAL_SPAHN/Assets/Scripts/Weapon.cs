using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    public Camera fpsCam;// Camara para calcular la punteria


    public float bulletSpeed = 20f;// Velocidad de la bala

    public GameObject Bullet;//Prefab de la bala

    public int max_ammo = 20; //Cantidad maxima de balas
    private int current_ammo = -1;
    public float reload_time = 1f; // Tiempo de espera de recarga
    private bool is_reloading = false; //bool para saber si estoy recargando

    public TextMeshProUGUI balas; // Texto de balas

    public Transform FirePoint; //punto desde donde saldran las balas

    private void Start()
    {
        current_ammo=max_ammo;
        actualizar_balas();
    }

    private void Update()
    {
        if (is_reloading)
        {
            return;
        }

        if ((current_ammo <= 0|| Input.GetKey(KeyCode.R))&& current_ammo<max_ammo)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {

            Disparo();
        }

    }
    // Configuro la recarga y el tiempo que tarda
    IEnumerator Reload()
    {
        is_reloading = true;
        balas.text = "Recargando";
        yield return new WaitForSeconds(reload_time);
        current_ammo = max_ammo;
        is_reloading=false;
        actualizar_balas();
    }


    // funcion para disparar las balas desde el arma
    void Disparo()
    {


        GameObject bullet = Instantiate(Bullet, FirePoint.position, FirePoint.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.jugadorDisparador = this.gameObject; // Asigno al jugador que disparó el proyectil
        }

        // Calculo un punto objetivo hacia adelante desde la cámara, simulando el centro de la pantalla
        Vector3 targetPoint = fpsCam.transform.position + fpsCam.transform.forward * 1000f;

        // Dirección de la bala hacia el punto objetivo
        Vector3 shootDirection = (targetPoint - FirePoint.position).normalized;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootDirection * bulletSpeed;
        }
            current_ammo--;
        actualizar_balas();
    }

    // Funcion para actualizar el texto de las balas
    void actualizar_balas()
    {
        balas.text = "Balas: " + current_ammo.ToString("F0");
    }

}
