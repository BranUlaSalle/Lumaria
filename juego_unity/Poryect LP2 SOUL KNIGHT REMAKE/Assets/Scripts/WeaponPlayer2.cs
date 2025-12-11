using UnityEngine;

public class WeaponPlayer2 : MonoBehaviour
{
    [SerializeField] private proyective projectilePrefab;
    [SerializeField] private Transform shootPosition;
    
    private SpriteRenderer spriteRenderer;
    private Vector2 currentDirection = Vector2.right;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("WeaponPlayer iniciado - Controles: Teclado numérico (4,8,5,6) y Espacio para disparar");
    }

    void Update()
    {
        RotateWithKeypad();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Espacio presionado - Disparando");
            if (projectilePrefab != null && shootPosition != null)
            {
                Shoot();
            }
            else
            {
                Debug.LogError("Faltan asignaciones: projectilePrefab = " + (projectilePrefab != null) + ", shootPosition = " + (shootPosition != null));
            }
        }
    }
    
    private void Shoot()
    {
        Debug.Log("Disparando desde posición: " + shootPosition.position);
        proyective projectile = Instantiate(projectilePrefab, shootPosition.position, transform.rotation);
        projectile.LaunchProjectile(transform.right);
        Debug.Log("Proyectil instanciado: " + (projectile != null));
    }
    
    private void RotateWithKeypad()
    {
        Vector2 inputDirection = GetKeypadDirection();
        
        // Actualizar dirección si hay input
        if (inputDirection != Vector2.zero)
        {
            currentDirection = inputDirection.normalized;
            
            // Calcular ángulo y rotar el arma
            float angle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            
            // Flip vertical para mejor apariencia
            if (spriteRenderer != null)
            {
                spriteRenderer.flipY = (angle > 90f && angle < 270f);
            }
        }
    }
    
    private Vector2 GetKeypadDirection()
    {
        Vector2 direction = Vector2.zero;
        
        // Teclado numérico - 4 direcciones principales
        if (Input.GetKey(KeyCode.Keypad8)) // Arriba
        {
            direction.y = 1f;
        }
        if (Input.GetKey(KeyCode.Keypad5)) // Abajo
        {
            direction.y = -1f;
        }
        if (Input.GetKey(KeyCode.Keypad4)) // Izquierda
        {
            direction.x = -1f;
        }
        if (Input.GetKey(KeyCode.Keypad6)) // Derecha
        {
            direction.x = 1f;
        }
        
        return direction;
    }
}