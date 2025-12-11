using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public int maxHealth = 100;
    public int currentHealth;
    
    [Header("Efectos")]
    public GameObject deathEffect;
    public AudioClip hitSound;
    public AudioClip deathSound;
    
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        // Inicializar componentes
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Guardar color original para efectos de daño
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Método para recibir daño
    public void TakeDamage(int damageAmount)
    {
        // Reducir vida
        currentHealth -= damageAmount;
        
        // Efecto visual de daño
        StartCoroutine(DamageFlash());
        
        // Sonido de golpe
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        
        // Verificar si el enemigo murió
        if (currentHealth <= 0)
        {
            Die();
        }
        
        Debug.Log($"Vida restante: {currentHealth}");
    }

    // Efecto visual cuando recibe daño
    private System.Collections.IEnumerator DamageFlash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
        }
    }

    // Método que se ejecuta cuando el enemigo muere
    private void Die()
    {
        // Efecto de muerte
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        // Sonido de muerte
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        
        // Destruir el objeto
        Destroy(gameObject);
        
        Debug.Log("¡Enemigo eliminado!");
    }
}