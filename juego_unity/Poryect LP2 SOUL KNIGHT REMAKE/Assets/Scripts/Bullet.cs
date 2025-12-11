using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Configuración de Bala")]
    public int damage = 10;
    public float speed = 10f;
    public float lifetime = 3f;
    
    [Header("Efectos")]
    public GameObject hitEffect;
    
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Destruir bala después de un tiempo por si no choca con nada
        Destroy(gameObject, lifetime);
        
        // Mover la bala
        if (rb != null)
        {
            rb.linearVelocity = transform.right * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si chocó con un enemigo
        if (collision.CompareTag("Enemy"))
        {
            // Obtener el componente de vida del enemigo
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            
            if (enemyHealth != null)
            {
                // Aplicar daño al enemigo
                enemyHealth.TakeDamage(damage);
            }
            
            // Efecto de impacto
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            
            // Destruir la bala
            Destroy(gameObject);
        }
        // Evitar que la bala se destruya con el jugador u otros objetos
        else if (!collision.CompareTag("Player") && !collision.CompareTag("Bullet"))
        {
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}