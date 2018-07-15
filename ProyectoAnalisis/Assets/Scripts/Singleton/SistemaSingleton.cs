using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Se utiliza singleton para llevar el orden pero este no es necesario guradarlo entre escenas por que es la ultima escena
/// </summary>
public class SistemaSingleton : MonoBehaviour {

    public GameObject prebabSistema; //se utiliza este objeto por que el conoce el sistema planetario y el objeto sistema donde se cargan los planetas
    public NebulosaSingleton nebulosaSingleton;
    public void setSistema(GameObject _sistema)
    {
        prebabSistema = _sistema;
    }
   
}
