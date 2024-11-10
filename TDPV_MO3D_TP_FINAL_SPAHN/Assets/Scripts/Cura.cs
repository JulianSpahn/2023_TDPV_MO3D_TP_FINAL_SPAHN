using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cura : MonoBehaviour
{
    public float healingAmount = 20f; // Cantidad de vida que restaura el objeto

    private void OnTriggerEnter(Collider other)
    {
        // Verifico si el objeto que toca el ítem es el jugador
        if (other.CompareTag("Player"))
        {
            // Obtengo el componente del jugador
            player_movement player = other.GetComponent<player_movement>();
            if (player != null)
            {
                player.Heal(healingAmount); // Curo al jugador
                Destroy(gameObject); // Destruyo el objeto curativo
            }
        }
    }
}
