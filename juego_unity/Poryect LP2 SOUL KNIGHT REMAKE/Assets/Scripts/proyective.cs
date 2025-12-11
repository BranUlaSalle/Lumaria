using UnityEngine;

public class proyective : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private float destroyDelay = 2f;
    private Rigidbody2D projectileRb;
    
    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }
    
    public void LaunchProjectile(Vector2 direction)
    {
        if (projectileRb != null)
        {
            // Usar velocity en lugar de linearVelocity
            projectileRb.linearVelocity = direction.normalized * speed;
        }
        
        Destroy(gameObject, destroyDelay);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}