using UnityEngine;

public class CameraFollow : MonoBehaviour
{  
    //personaje al que la camara seguira
    public Transform target;
    //velocidad a la que la camara seguira al personaje
    public float smoothSpeed = 0.125f;
    //distancia entre la camara y el personaje
    public Vector3 offset;

    void LateUpdate()
    {
        //confirma si hay un personaje asignado para seguir
        if (target != null)
        {
            //calcula la posicion de la camara
            Vector3 desiredPosition = target.position + offset;
            //la camra se mueve suevemente
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            //actualiza la posicion de la camara
            transform.position = smoothedPosition;
        }
    }
}