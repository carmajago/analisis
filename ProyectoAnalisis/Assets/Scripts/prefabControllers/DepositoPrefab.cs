using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositoPrefab : MonoBehaviour {

    public Deposito deposito;
    private Transform tr;

	void Start () {
        tr = GetComponent<Transform>();
        IndicadoresDepositoTele idt= GameObject.FindGameObjectWithTag("Controlador").GetComponent<IndicadoresDepositoTele>();
        idt.deposito= this.gameObject;
    }

   
    public void setDeposito(Deposito _deposito)
    {
        deposito = _deposito;
    }
   
}
