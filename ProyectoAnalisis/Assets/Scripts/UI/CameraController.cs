using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public float panSpeed=20f;
    public float panBorderThickness=10f;
    public Vector2 panlimitX;
    public Vector2 panlimitY;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        if (Input.GetKey("a") || Input.mousePosition.x <=  panBorderThickness)
        {
            posicion.x -= panSpeed * Time.deltaTime;
        }

        posicion.x = Mathf.Clamp(posicion.x, panlimitX.x, panlimitX.y);
        posicion.z = Mathf.Clamp(posicion.z, panlimitY.x, panlimitY.y);

        transform.position = posicion;
    }
}
