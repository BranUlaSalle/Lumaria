using UnityEngine;

public class Bala : MonoBehaviour
{
    public int daño = 10;
    
    void OnTriggerEnter(Collider other)
    {
        // Verificar si chocó con un enemigo
        if (other.CompareTag("Enemigo"))
        {
            // Obtener el componente VidaEnemigo del objeto chocado
            VidaEnemigo vidaEnemigo = other.GetComponent<VidaEnemigo>();
            
            if (vidaEnemigo != null)
            {
                // Hacer daño al enemigo
                vidaEnemigo.RecibirDaño(daño);
            }
            
            // Destruir la bala después del impacto
            Destroy(gameObject);
        }
    }
}
