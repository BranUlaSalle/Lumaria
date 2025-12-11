using UnityEngine;

public class EnemyAI_03 : MonoBehaviour
{
    [Header("Jugador")]
    public Transform player;
    public float detectionRadius = 5f;
    public float stopDistance = 1.3f;

    [Header("Movimiento")]
    public float speed = 2f;
    public float changeDirectionTime = 3f;

    private Vector2 movement;
    private float timer;

    void Start()
    {
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

        // Movimiento
        transform.position += (Vector3)(movement * speed * Time.deltaTime);

        // --- VOLTEAR SPRITE ---
        if (movement.x > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement.x < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void ChangeRandomDirection()
    {
        movement = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }
}
