using UnityEngine;

public class EnemyAI_01 : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;
    public float detectionRadius = 5f;
    public float stopDistance = 1.3f;

    [Header("Movimiento")]
    public float speed = 2f;
    public float changeDirectionTime = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private float timer;
    private Vector3 originalScale;

    // -------------------------
    //    VIDA DEL ENEMIGO
    // -------------------------
    [Header("Vida")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Barra de Vida (UI)")]
    public GameObject healthBarCanvas;     // Canvas encima del enemigo
    public UnityEngine.UI.Image healthFill; // Imagen roja de la barra
    // -------------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        // Vida inicial
        currentHealth = maxHealth;

        // Activar barra si existe
        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(true);

        // Evitar que empuje al jugador
        Collider2D enemyCol = GetComponent<Collider2D>();
        Collider2D playerCol = player.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(enemyCol, playerCol);

        ChangeRandomDirection();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < detectionRadius && distance > stopDistance)
        {
            movement = (player.position - transform.position).normalized;
        }
        else if (distance <= stopDistance)
        {
            movement = Vector2.zero;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                ChangeRandomDirection();
        }

        // Voltear sprite
        if (movement.x > 0f)
            transform.localScale = originalScale;
        else if (movement.x < 0f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void ChangeRandomDirection()
    {
        movement = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }

    // -------------------------
    //   RECIBIR DAÑO
    // -------------------------
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Actualiza barra
        if (healthFill != null)
        {
            healthFill.fillAmount = (float)currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
