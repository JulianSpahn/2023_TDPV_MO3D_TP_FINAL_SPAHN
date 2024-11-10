using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera_movement : MonoBehaviour
{
    [SerializeField]
    private float Sensibilidad_de_la_Camara = 100f; // Configuro la sensibilidad de la camara
    [SerializeField]
    private Transform Jugador; // Obtengo el transform del jugador
    float Rotacion_en_X = 0f;


    // Start is called before the first frame update
    void Start()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        if (escenaActual == 1) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (escenaActual ==0 || escenaActual ==2 || escenaActual == 3 || escenaActual ==4) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float MouseX = Input.GetAxis("Mouse X") * Sensibilidad_de_la_Camara ;
        float MouseY = Input.GetAxis("Mouse Y") * Sensibilidad_de_la_Camara ;
        Rotacion_en_X-=MouseY;
        Rotacion_en_X= Mathf.Clamp(Rotacion_en_X, -90,90);
        transform.localRotation = Quaternion.Euler(Rotacion_en_X, 0f, 0f);
        Jugador.Rotate(Vector3.up * MouseX);
    }
}
