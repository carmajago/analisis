using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoverGameObjects : MonoBehaviour {

    public LayerMask layerMover;
    public Toggle toggleMover;

    private GameObject gameObjectMover;
    private bool isMover = false;

    private void Update()
    {
        if (toggleMover.isOn)
        {

            if (Input.GetMouseButton(0) && !isMover)
            {
                isMover = true;
                gameObjectMover = buscarObjeto();
                if (gameObjectMover != null)
                {
                    StartCoroutine(MoverCOR(gameObjectMover));
                }
                else
                {
                    isMover = false;
                }
            }
        }

    }


    IEnumerator MoverCOR(GameObject objeto)
    {
          


        while (!Input.GetMouseButtonUp(0))
        {
            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, objeto.transform.position.y, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            objeto.transform.position = posMouse;
            yield return new WaitForSeconds(0.01f);
        }
        isMover = false;
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
