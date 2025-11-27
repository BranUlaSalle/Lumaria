using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//creamos la clase MenuInicial que hereda de MonoBehaviour
public class MenuInicial : MonoBehaviour
{
    //funcion para iniciar el juego
    public void Jugar()
    {
        //cargar la siguiente escena 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //funcion para salir del juego
    public void Salir()
    {
        Application.Quit();
    }
}
