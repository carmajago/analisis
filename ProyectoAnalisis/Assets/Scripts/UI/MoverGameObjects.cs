using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoverGameObjects : MonoBehaviour {

    public LayerMask layerMover;
    public Toggle toggleMover;

    private GameObject gameObjectMover;


    private void Update()
    {
        if (toggleMover.isOn)
        {


            if (gameObjectMover != null)
            {
                Vector3 posMouse;
                Vector3 pos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(pos);
                Plane xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
                float distance;
                xy.Raycast(ray, out distance);
                posMouse = ray.GetPoint(distance);
                gameObjectMover.transform.position = posMouse;

            }

            if (Input.GetMouseButtonUp(0))
            {
                gameObjectMover = null;
            }

            if (Input.GetMouseButton(0))
            {
                gameObjectMover = buscarObjeto();
                Debug.Log(gameObjectMover);

            }
        }

    }


    public GameObject buscarObjeto()
    {
        GameObject temp = null;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMover))
        {
            temp = hit.transform.gameObject;
            
        }
        return temp;
    }


}
