﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrearLineasNodo : MonoBehaviour
{
    public LayerMask capa;
    public GameObject lineaPrefab;


    public Toggle activo;
    void Start()
    {

    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && activo.isOn)
        {
            GameObject origen = null;

            origen = buscarObjeto();
            if (origen != null)
            {

                StartCoroutine(unirNodos(origen));
            }
        }
    }


    IEnumerator unirNodos(GameObject origen)
    {
        GameObject lineaObject = Instantiate(lineaPrefab);
        LineRenderer linea = lineaObject.GetComponent<LineRenderer>();
        AristaPrefab arista = lineaObject.GetComponent<AristaPrefab>();
        arista.origen = origen;

        arista.aristaNodo.origenFK = buscarIdNodo(origen);



        linea.SetPosition(0, origen.transform.position);
        while (!Input.GetMouseButtonUp(0))
        {

            Vector3 posMouse;
            Vector3 pos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(pos);
            Plane xy = new Plane(Vector3.up, new Vector3(0, -40, 0));
            float distance;
            xy.Raycast(ray, out distance);
            posMouse = ray.GetPoint(distance);
            linea.SetPosition(1, posMouse);
            yield return new WaitForSeconds(0.015f);
        }
        GameObject destino = buscarObjeto();

        if (destino == null || destino == origen)
        {
            Destroy(lineaObject);
        }
        else
        {
            linea.SetPosition(1, destino.transform.position);
            arista.destino = destino;
            arista.terminado = true;
            arista.aristaNodo.destinoFK = buscarIdNodo(destino);
            arista.aristaNodo.sistemaFK =buscarIdSistema(destino);
            ApiCalls.PostAristaNodo(arista.aristaNodo);

            GameObject.FindGameObjectWithTag("Sistema").GetComponent<SistemaSingleton>().prebabSistema.GetComponent<SistemaplanetarioPrefab>().sistemaPlanetario.grafo.Add(arista.aristaNodo);
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, capa))
        {

            temp = hit.transform.gameObject;

        }
        return temp;
    }

    public int buscarIdNodo(GameObject nodo)
    {
        PlanetaPrebab pp = nodo.GetComponent<PlanetaPrebab>();
        

       
            return pp.planeta.id;
        
       

    }

    public int buscarIdSistema(GameObject nodo)
    {
        PlanetaPrebab pp = nodo.GetComponent<PlanetaPrebab>();
        DepositoPrefab dp = nodo.GetComponent<DepositoPrefab>();
        TeletransportadorPrefab tp = nodo.GetComponent<TeletransportadorPrefab>();

       
            return pp.planeta.sistemaPlanetarioFK;
        
       

    }
}
