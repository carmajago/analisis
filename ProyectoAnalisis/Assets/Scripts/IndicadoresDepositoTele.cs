using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Esta clase se encarga de indicar si existe deposito y teletransportador y evita que se cree mas de un deposito y en teletransportador
/// </summary>
public class IndicadoresDepositoTele : MonoBehaviour {

    public GameObject deposito;
    public GameObject teletransportador;

    public Toggle depositoT;
    public Toggle teletransportadorT;

    public Button depositoB;
    public Button teletransportadorB;


    private void LateUpdate()
    {
        if (deposito == null)
        {
            depositoT.isOn = false;
            if (depositoB != null)
                depositoB.interactable = true;
        }
        else
        {
            depositoT.isOn = true;
            if (depositoB != null)
                depositoB.interactable = false;
        }

        if (teletransportador == null)
        {
            teletransportadorT.isOn = false;
            if(teletransportadorB!=null)
            teletransportadorB.interactable = true;
        }
        else
        {
            teletransportadorT.isOn = true;
            if (teletransportadorB != null)
                teletransportadorB.interactable = false;
        }
    }


}
