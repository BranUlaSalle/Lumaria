using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Efectos y UI")]
    public GameObject damageEffect;
    public AudioClip damageSound;
    public AudioClip deathSound;

    [Header("Invencibilidad")]
    public float invincibilityTime = 2f;
    public float flashInterval = 0.1f;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;
    private Color originalColor;

    // Evento para actualizar UI
    public System.Action<int> OnHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        Debug.Log($"Vida inicial: {currentHealth}/{maxHealth}");
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        OnHealthChanged?.Invoke(currentHealth);

        // Sonido
        if (damageSound && audioSource)
            audioSource.PlayOneShot(damageSound);

        // Efecto
        if (damageEffect)
            Instantiate(damageEffect, transform.position, Quaternion.identity);

        // Activar invencibilidad
        StartCoroutine(InvincibilityFlash());

        Debug.Log($"Daño recibido. Vida: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private System.Collections.IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        float end = Time.time + invincibilityTime;

        while (Time.time < end)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);
                yield return new WaitForSeconds(flashInterval);

                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(flashInterval);
            }
            else
                yield return null;
        }

        if (spriteRenderer != null)
            spriteRenderer.color = originalColor;

        isInvincible = false;
    }

    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
        Debug.Log($"Curado. Vida actual: {currentHealth}/{maxHealth}");
    }

    private void Die()
    {
        Debug.Log("GAME OVER — jugador sin vida.");

        if (deathSound && audioSource)
            audioSource.PlayOneShot(deathSound);

        if (damageEffect)
            Instantiate(damageEffect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);

        Invoke(nameof(RestartLevel), 2f);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
