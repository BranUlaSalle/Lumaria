using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;
    
    void Start()
    {
        vidaActual = vidaMaxima;
    }
    
    public void RecibirDaño(int cantidadDaño)
    {
        vidaActual -= cantidadDaño;
        
        // Verificar si el enemigo murió
        if (vidaActual <= 0)
        {
            Morir();
        }
    }
    
    void Morir()
    {
        Debug.Log("¡Enemigo eliminado!");
        Destroy(gameObject); // Esto destruye al enemigo
    }
}