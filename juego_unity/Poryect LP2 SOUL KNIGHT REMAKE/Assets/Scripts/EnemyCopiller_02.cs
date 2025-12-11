using UnityEngine;

public class EnemyAI_02 : MonoBehaviour
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
    private Vector3 originalScale; // scale real del enemigo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale; // guardar escala real
        ChangeRandomDirection();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Persecución
        if (distance < detectionRadius && distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction;
        }
        else if (distance <= stopDistance)
        {
            movement = Vector2.zero;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ChangeRandomDirection();
            }
        }

        // Voltear sprite sin deformar
        if (movement.x > 0f)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (movement.x < 0f)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    void FixedUpdate()
    {
        // Movimiento con física (NO atraviesa paredes)
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void ChangeRandomDirection()
    {
        movement = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }
}
