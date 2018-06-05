using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraNave : MonoBehaviour {


   public  Vector3 offset;
    public float distancia;
    public float distanciaY;
    public float alturaCamara;


    // Update is called once per frame
    void Update () {
        
        Vector3 posicion = transform.position + offset;
        Camera.main.transform.position=posicion;

        
    }
}
