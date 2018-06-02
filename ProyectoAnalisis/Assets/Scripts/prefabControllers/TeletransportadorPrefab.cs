﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletransportadorPrefab : MonoBehaviour {

    public Teletransportador teletransportador;
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
        GameObject.FindGameObjectWithTag("Controlador").GetComponent<IndicadoresDepositoTele>().teletransportador = this.gameObject;
    }

    void LateUpdate()
    {
        Vector3 posicion = new Vector3(teletransportador.x, teletransportador.y, teletransportador.z);
        if (tr.localPosition != posicion && Input.GetMouseButtonUp(0))
        {
            teletransportador.x = tr.localPosition.x;
            teletransportador.y = tr.localPosition.y;
            teletransportador.z = tr.localPosition.z;

            TeletransportadorService.PutTeletransportador(teletransportador);
        }
    }
    public void setTeletransportador(Teletransportador tele)
    {
        teletransportador = tele;
    }
    public void actualizarDatos(int id)
    {
        teletransportador.x = transform.localPosition.x;
        teletransportador.y = transform.localPosition.y;
        teletransportador.z = transform.localPosition.z;
        teletransportador.sistemaPlanetarioFK = id;
    }

}
