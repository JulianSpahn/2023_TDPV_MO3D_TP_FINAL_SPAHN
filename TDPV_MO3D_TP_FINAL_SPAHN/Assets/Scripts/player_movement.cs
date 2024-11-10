using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player_movement : MonoBehaviour
{

    public CharacterController control_del_jugador;

    public float speed = 5f; // Velocidad de movimiento
    public float jumpHeight = 2f; // Altura del salto
    public float gravity = -9.81f; // Gravedad aplicada
    private Vector3 velocity; // Velocidad del personaje en el eje Y
    public float sprintSpeed = 10f; // Velocidad al correr
    private bool isGrounded; // Verifico si el personaje est� en el suelo


    public float maxHealth = 100f; // Vida maxima del personaje
    private float currentHealth; // Vida actual
    public TextMeshProUGUI Vida; // Texto de vida

    public Image da�oEfecto; // Imagen del Canvas para el efecto de da�o
    public float duracionParpadeo = 0.2f; // Duraci�n del efecto


    // Start is called before the first frame update
    // Instancio los componentes y recursos necesarios para el personaje
    void Start()
    {
        control_del_jugador = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        actualizar_vida();
        da�oEfecto.color = new Color(da�oEfecto.color.r, da�oEfecto.color.g, da�oEfecto.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Detecto si el personaje est� en el suelo
        isGrounded = control_del_jugador.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reseteo la velocidad al tocar el suelo
        }

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        // Movimiento horizontal
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        control_del_jugador.Move(move * currentSpeed * Time.deltaTime);

        // Salto
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calcula la velocidad inicial para el salto
        }

        // Aplica la gravedad de forma progresiva
        velocity.y += gravity * Time.deltaTime;

        // Mueve al personaje en el eje Y
        control_del_jugador.Move(velocity * Time.deltaTime);

        actualizar_vida();
    }
    // funcion para saber cuando el jugador es da�ado y comprobar si este muere
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        actualizar_vida();
        StartCoroutine(ParpadeoRojo());
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }
    // Configuro el parpadeo de cuando el jugador es herido
    IEnumerator ParpadeoRojo()
    {
        da�oEfecto.color = new Color(da�oEfecto.color.r, da�oEfecto.color.g, da�oEfecto.color.b, 0.5f);
        yield return new WaitForSeconds(duracionParpadeo);
        da�oEfecto.color = new Color(da�oEfecto.color.r, da�oEfecto.color.g, da�oEfecto.color.b, 0);
    }

    // Funcion para la cura
    public void Heal(float amount)
    {
        currentHealth+=amount;
        if(currentHealth > maxHealth)
        {
            currentHealth=maxHealth;
        }
    }
    // Actualizo el texto del canvas 
    void actualizar_vida()
    {
        Vida.text = "Vida: " + currentHealth.ToString("F0");
    }

}
