using UnityEngine;


public class movimiento_camara : MonoBehaviour
{
   public Transform objetivo;
   public float velocidadcamara = 0.025f ;
   public Vector3 desplazamiento ;

    private void LateUpdate()
    {
      Vector3 posiciondeseada = objetivo.position + desplazamiento;
      Vector3 posicionsuavisada = Vector3.Lerp(transform.position, posiciondeseada, velocidadcamara );

      transform.position = posicionsuavisada;

    }
}
