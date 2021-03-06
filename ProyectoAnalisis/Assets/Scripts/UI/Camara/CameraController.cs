﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Este script se ocupa de la camara en la escena Editar
/// </summary>
public class CameraController : MonoBehaviour {


    public float panSpeed=20f;
    public float panBorderThickness=10f;
    public Vector2 panlimitX;
    public Vector2 panlimitZ;
  
 
   


    private Vector3 posicionInicial = Vector3.zero;
    private Quaternion rotacionInicial;
    private bool dosD = false;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float poin = (target.position - Camera.main.transform.position).magnitude*0.5f;
        //transform.LookAt(ray.origin+ray.direction*poin);
        moverCamara();
    }


    private void moverCamara()
    {
        Vector3 posicion = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            posicion.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            posicion.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            posicion.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            posicion.x -= panSpeed * Time.deltaTime;
        }

       
       
        posicion.x = Mathf.Clamp(posicion.x, panlimitX.x, panlimitX.y);
  
        posicion.z = Mathf.Clamp(posicion.z, panlimitZ.x, panlimitZ.y);

        transform.position = posicion;
    }

    public void set2D()
    {

        if (dosD)
        {
            dosD = false;
            Camera.main.transform.position = posicionInicial;
            Camera.main.transform.rotation = rotacionInicial;


        }
        else
        {
            dosD = true;
            posicionInicial = Camera.main.transform.position;
            rotacionInicial = Camera.main.transform.rotation;
            Camera.main.transform.eulerAngles = new Vector3(90f, 0, 0);
            Camera.main.transform.position = new Vector3(0, 130, 0);
        }


    }
}
