﻿using System.Collections;
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
    /// </summary>
    public void cargar()
    {
        foreach (var item in viaLactea.Nebulosas)
        {
            GameObject prebabNebulosa= Instantiate(prefabNebulosa, new Vector3(item.x, item.y, item.z),Quaternion.identity);
            prebabNebulosa.GetComponent<NebulosaPrefab>().setNebulosa(item);

        }
    }

}