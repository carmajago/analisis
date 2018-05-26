using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AristaPrefab : MonoBehaviour {

    public GameObject origen;
    public GameObject destino;

    public AristaSistema aristaSistema;
    public AristaNodo aristaNodo;


    public bool terminado=false;
    private TextMeshProUGUI InfoDistancia;
    private GameObject canvasDistancia;

    private LineRenderer linea;

    void Start () {
        linea = GetComponent<LineRenderer>();
        canvasDistancia = transform.Find("InfoDistancia").gameObject;
        InfoDistancia = canvasDistancia.transform.Find("Distancia").GetComponent<TextMeshProUGUI>();
        canvasDistancia.SetActive(false);
	}
	
	
	void FixedUpdate () {
        if (terminado)
        {
            canvasDistancia.SetActive(true);
            if (origen != null && destino != null)
            {
                linea.SetPosition(0, origen.transform.position);
                linea.SetPosition(1, destino.transform.position);
                canvasDistancia.transform.position = (origen.transform.position + destino.transform.position) / 2;
                float distancia = (origen.transform.position - destino.transform.position).magnitude;
                decimal num = decimal.Round(decimal.Parse(distancia.ToString()), 2);
                InfoDistancia.text =""+ num;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}
