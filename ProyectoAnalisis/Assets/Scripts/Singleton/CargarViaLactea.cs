using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// En esta clase esta la información de toda la via lactea
/// 
/// </summary>
public class CargarViaLactea : MonoBehaviour {
    public static CargarViaLactea cargarViaLactea;

    public GameObject prefabNebulosa;

    public ViaLactea viaLactea;
    

    void Awake()
    {
        if (cargarViaLactea == null)
        {
            cargarViaLactea = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (cargarViaLactea != this)
        {
            Destroy(gameObject);
        }

    }



    public void setViaLactea(ViaLactea via)
    {
        viaLactea = via;
    }
    /// <summary>
    /// Este metodo  instancia en la escena todas las nebulosas que hay en la via lactea
    /// <paramref name="escena"/>Nombre de la escena a la cual se va a dirigir, se asigna pero no quiere decir que en este llamado nos dirijamos a esa escena
    /// </summary>
    public void cargar(string escena)
    {
        foreach (var item in viaLactea.Nebulosas)
        { Vector3 posicion = new Vector3(item.x, item.y, item.z);
            GameObject prebabNebulosa= Instantiate(prefabNebulosa,posicion ,Quaternion.identity);
            NebulosaPrefab np = prebabNebulosa.GetComponent<NebulosaPrefab>();
            np.setNebulosa(item);
            np.escena = escena;
        }
    }

}
