using System.Collections;
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

    public void setTeletransportador(Teletransportador tele)
    {
        teletransportador = tele;
    }
  

}
