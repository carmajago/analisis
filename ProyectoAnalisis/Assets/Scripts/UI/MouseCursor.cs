using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {

    public float magnitudLinea=10f;
    public float yPos;
    public Camera camara;
    public bool MouseOff = false;
    private GameObject cursor;
    private Vector3 posMouse;
    private Transform tr;
    private LineRenderer [] lineas;
	void Start () {
        tr = GetComponent<Transform>();
        lineas = GetComponentsInChildren<LineRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if(MouseOff)
          Cursor.visible = false;

        Vector3 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Plane xy = new Plane(Vector3.up, new Vector3(0, yPos, 0));
        float distance;
        xy.Raycast(ray, out distance);


     

        posMouse = ray.GetPoint(distance);

        tr.position = posMouse;

        lineas[0].SetPosition(0, posMouse+new Vector3(5,0,0));
        lineas[1].SetPosition(0, posMouse-new Vector3(5, 0, 0));
        lineas[2].SetPosition(0, posMouse + new Vector3(0, 0, 5));
        lineas[3].SetPosition(0, posMouse - new Vector3(0, 0, 5));


        Vector3 linea1 = posMouse + new Vector3(magnitudLinea, 0, 0);
        Vector3 linea2 = posMouse + new Vector3(-magnitudLinea, 0, 0);
        Vector3 linea3 = posMouse + new Vector3(0, 0, magnitudLinea);
        Vector3 linea4 = posMouse + new Vector3(0, 0, -magnitudLinea);


        lineas[0].SetPosition(1, linea1);
        lineas[1].SetPosition(1, linea2);
        lineas[2].SetPosition(1, linea3);
        lineas[3].SetPosition(1, linea4);

    }
    public void dibujarLineas()
    {
       
    }
}
