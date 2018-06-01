using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositoPrefab : MonoBehaviour {

    public Deposito deposito;
    private Transform tr;

	void Start () {
        tr = GetComponent<Transform>();
	}

    void LateUpdate()
    {
        Vector3 posicion = new Vector3(deposito.x, deposito.y, deposito.z);
        if (tr.localPosition != posicion && Input.GetMouseButtonUp(0))
        {
            deposito.x = tr.localPosition.x;
            deposito.y = tr.localPosition.y;
            deposito.z = tr.localPosition.z;

            DepositoService.PutDeposito(deposito);
        }
    }

    public void setDeposito(Deposito _deposito)
    {
        deposito = _deposito;
    }
    public void actualizarDatos(int id)
    {
        deposito.x = transform.localPosition.x;
        deposito.y = transform.localPosition.y;
        deposito.z = transform.localPosition.z;
        deposito.sistemaPlanetarioFK = id;
    }
}
