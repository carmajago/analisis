using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {

    public float yPos;
    public Camera camara;
    private GameObject cursor;
    private Vector3 posMouse;
    private Transform tr;

	void Start () {
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        //  Cursor.visible = false;

        Vector3 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Plane xy = new Plane(Vector3.up, new Vector3(0, yPos, 0));
        float distance;
        xy.Raycast(ray, out distance);




        posMouse = ray.GetPoint(distance);

        tr.position = posMouse;
    }
}
