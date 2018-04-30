using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    public float velocidad = 0.5f;

    private float x;
    private float y;


    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
        Cursor.lockState = CursorLockMode.Locked;   
        x += Input.GetAxis("Mouse X")* velocidad;
        y -= Input.GetAxis("Mouse Y")* velocidad;

        transform.eulerAngles = new Vector3(y,0,0);
	}
}
