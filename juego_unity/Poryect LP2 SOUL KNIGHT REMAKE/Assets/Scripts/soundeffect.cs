using UnityEngine;

public class soundeffect : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0; // Botón izquierdo por defecto
    [SerializeField] private bool useMouseRight = true; // Cambiar a true para usar botón derecho
    
    [Header("Componentes de Audio")]
    [SerializeField] private AudioClip shootSound; // Arrastra tu archivo de sonido aquí
    [SerializeField] private AudioSource audioSource;
    
    [Header("Configuración de Sonido")]
    [SerializeField] private float volume = 1.0f;
    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private bool loop = false;
    
    private void Start()
    {
        // Configurar AudioSource si no está asignado
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            
            // Si no hay AudioSource, crear uno
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Configurar propiedades del AudioSource
        audioSource.playOnAwake = playOnAwake;
        audioSource.loop = loop;
        audioSource.volume = volume;
        
        // Asignar el clip de sonido si está configurado
        if (shootSound != null)
        {
            audioSource.clip = shootSound;
        }
    }
    
    private void Update()
    {
        // Verificar input de disparo
        if (CheckShootInput())
        {
            Shoot();
        }
    }
    
    private bool CheckShootInput()
    {
        if (useMouseRight)
        {
            // Usar botón derecho del mouse
            return Input.GetMouseButtonDown(1); // 0 = izquierdo, 1 = derecho, 2 = medio
        }
        else
        {
            // Usar la tecla configurada
            return Input.GetKeyDown(shootKey);
        }
    }
    
    private void Shoot()
    {
        // Reproducir sonido de disparo
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound, volume);
            Debug.Log("¡Disparo! Sonido reproducido.");
        }
        else
        {
            Debug.LogWarning("AudioClip o AudioSource no asignado en GunShootSound");
        }
        
        // Aquí puedes agregar más lógica de disparo:
        // - Lógica de balas
        // - Animaciones
        // - Efectos de partículas
        // - Recoil, etc.
    }
    
    // Métodos públicos para control desde otros scripts
    public void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound, volume);
        }
    }
    
    public void SetShootSound(AudioClip newSound)
    {
        shootSound = newSound;
    }
    
    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp01(newVolume);
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}