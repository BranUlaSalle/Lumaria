using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    // Velocidad del jugador
    public float velocidad = 5f;
    // Variable de tipo booleana para controlar la orientación del jugador
    private bool mirandoDerecha = true;

    // Función para darle posicion inicial al jugador
    void Start()
    {
    }

    // Función que se ejecuta una vez por frame para actualizar la lógica del juego
    void Update()
    {
        // Variables para el movimiento
        float movimientoHorizontal = 0f;
        float movimientoVertical = 0f;

        // Detectar teclas para movimiento horizontal (J y L)
        if (Input.GetKey(KeyCode.J))
        {
            movimientoHorizontal = -1f; // Izquierda
        }
        else if (Input.GetKey(KeyCode.L))
        {
            movimientoHorizontal = 1f; // Derecha
        }

        // Detectar teclas para movimiento vertical (I y K)
        if (Input.GetKey(KeyCode.I))
        {
            movimientoVertical = 1f; // Arriba
        }
        else if (Input.GetKey(KeyCode.K))
        {
            movimientoVertical = -1f; // Abajo
        }

        // Crear vector de movimiento, z es 0 porque es un juego 2D
        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0);

        // Aplicar movimiento al personaje
        transform.position += movimiento * velocidad * Time.deltaTime;

        // Función para que el personaje mire hacia la dirección que se mueve
        GestionarOrientacion(movimientoHorizontal);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        // Condición para saber si estaba mirando a la derecha y se mueve a la izquierda o viceversa
        if (mirandoDerecha == true && inputMovimiento < 0)
        {
            // Si mirandoDerecha es true y se mueve a la izquierda, cambiar la escala local en x a negativa
            mirandoDerecha = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (mirandoDerecha == false && inputMovimiento > 0)
        {   
            // Si mirandoDerecha es false y se mueve a la derecha, cambiar la escala local en x a positiva
            mirandoDerecha = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}