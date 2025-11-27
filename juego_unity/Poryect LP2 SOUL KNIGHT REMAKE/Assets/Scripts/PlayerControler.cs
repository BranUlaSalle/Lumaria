//bibliotecas de herramientas basicas de Unity
using System.Collections;
//bibliotecas de herramientas especificas y avanzadas de Unity
using System.Collections.Generic;
//bibliotecas de herramientas graficas y de motor de Unity
using UnityEngine;

///Clase principal del controlador del jugador, monoBehaviour es la clase base de todos los scripts de Unity
public class PlayerController : MonoBehaviour
{
    //velocidad del jugador
    public float velocidad = 5f;
    //variable de tipo booleana para controlar la orientacion del jugador
    private bool mirandoDerecha = true;
    //funcion para darle posicion inicial al jugador
    void Start()
    {
    }
    //funcion que se ejecuta una vez por frame para actualizar la logica del juego
    void Update()
    {
        // pregunta al teclado si esta presionando las teclas de arriba/abajo, izquierda/derecha
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");
        
        // Crear vector de movimiento, z es 0 porque es un juego 2D, movientoHorizontal es x y movimientoVertical es y
        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0);
        
        // Aplicar movimiento al personaje
        transform.position += movimiento * velocidad * Time.deltaTime;
        //funcion para que el personaje mire hacia la direccion que se mueve
        GestionarOrientacion(movimientoHorizontal);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        //condicion para saber si estaba mirando a la derecha y se mueve a la izquierda o viceversa
        if (mirandoDerecha == true && inputMovimiento < 0)
        {
            //si mirandoDerecha es true y se mueve a la izquierda, cambiar la escala local en x a negativa para voltear el personaje
            mirandoDerecha = false;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (mirandoDerecha == false && inputMovimiento > 0)
        {   
            //si mirandoDerecha es false y se mueve a la derecha, cambiar la escala local en x a positiva para voltear el personaje
            mirandoDerecha = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }   
}