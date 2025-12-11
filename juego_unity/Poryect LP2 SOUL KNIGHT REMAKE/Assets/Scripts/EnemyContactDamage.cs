using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public int damageAmount = 1;  // 1 punto de daño
    public float damageCooldown = 1f;  // Tiempo entre daños
    
    [Header("Detección")]
    public float detectionRange = 2f;  // Rango para detectar al jugador
    
    private float lastDamageTime = 0f;
    private Transform player;
    private EnemyHealth enemyHealth;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        
        // Buscar al jugador automáticamente
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // Si el enemigo está muerto, no hacer nada
        if (enemyHealth != null && enemyHealth.currentHealth <= 0) return;
        
        // Verificar daño continuo por proximidad
        if (player != null && IsPlayerInRange())
        {
            TryDamagePlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Daño instantáneo al entrar en contacto
        if (collision.CompareTag("Player"))
        {
            ApplyDamageToPlayer(collision.GetComponent<PlayerHealth>());
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        // Daño continuo mientras esté en contacto
        if (collision.CompareTag("Player"))
        {
            TryDamagePlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Daño por colisión física (si usas colliders sin trigger)
        if (collision.gameObject.CompareTag("Player"))
        {
            ApplyDamageToPlayer(collision.gameObject.GetComponent<PlayerHealth>());
        }
    }

    // Verificar si el jugador está en rango
    private bool IsPlayerInRange()
    {
        if (player == null) return false;
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }

    // Intentar dañar al jugador con cooldown
    private void TryDamagePlayer()
    {
        if (Time.time >= lastDamageTime + damageCooldown && player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            ApplyDamageToPlayer(playerHealth);
        }
    }

    // Aplicar daño al jugador
    private void ApplyDamageToPlayer(PlayerHealth playerHealth)
    {
        if (playerHealth != null && playerHealth.IsAlive())
        {
            playerHealth.TakeDamage(damageAmount);
            lastDamageTime = Time.time;
            
            Debug.Log($"Fantasma hizo {damageAmount} de daño al jugador");
        }
    }

    // Dibujar rango de detección en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}