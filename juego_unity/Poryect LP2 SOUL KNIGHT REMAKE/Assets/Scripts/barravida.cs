using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private PlayerController playerController;
    private float vidaMaxima;

    void Start()
    {
        playerController = GameObject.Find("personaje_0").GetComponent<PlayerController>();
        vidaMaxima = playerController.vida;
    }

    void Update()
    {
        rellenoBarraVida.fillAmount = playerController.vida / vidaMaxima;
    }
}
