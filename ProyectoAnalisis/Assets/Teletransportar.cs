using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransportar : MonoBehaviour {

    public GameObject animacion;
	

    public void iniciarAnimacion()
    {
        animacion.SetActive(true);
    }
}
