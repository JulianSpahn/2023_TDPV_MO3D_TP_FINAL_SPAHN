using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int totalEnemigos = 0;
    public TextMeshProUGUI contadorEnemigosText;


    void Start()
    {
        //leo cuantos enemigos hay y actualizo
        totalEnemigos = FindObjectsOfType<enemy>().Length;
        ActualizarContadorEnemigos();
    }
    //metodo para restar al enemigo que fue eliminado
    public void EnemigoEliminado()
    {
        totalEnemigos--;
        ActualizarContadorEnemigos();
        VerificarVictoria();
    }
    //actualizo la cantidad de enemigos restantes
    void ActualizarContadorEnemigos()
    {
        contadorEnemigosText.text = "Enemigos restantes: " + totalEnemigos;
    }
    //verifico si no hay mas enemigos y voy a la pantalla de victoria
    void VerificarVictoria()
    {
        if (totalEnemigos <= 0)
        {
            Debug.Log("¡Has ganado el juego!");
            SceneManager.LoadScene(3);
        }
    }


}
