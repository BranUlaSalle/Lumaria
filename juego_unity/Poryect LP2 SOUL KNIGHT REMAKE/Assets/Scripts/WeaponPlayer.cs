using UnityEngine;

public class WeaponPlayer : MonoBehaviour
{
    [SerializeField] private proyective projectilePrefab;
    [SerializeField] private Transform shootPosition;
    
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;
        Debug.Log("WeaponPlayer iniciado. Cámara: " + (mainCamera != null));
    }

    void Update()
    {
        RotateTowardsMouse();
        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click detectado");
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
    
    private void RotateTowardsMouse()
    {
        float angle = GetAngleTowardsMouse();
        transform.rotation = Quaternion.Euler(0, 0, angle);
        spriteRenderer.flipY = (angle > 90f && angle < 270f);
    }
    
    private float GetAngleTowardsMouse()
    {
        if (mainCamera == null) return 0f;
        
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseDirection = mouseWorldPosition - transform.position;
        mouseDirection.z = 0;

        return Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
    }
}