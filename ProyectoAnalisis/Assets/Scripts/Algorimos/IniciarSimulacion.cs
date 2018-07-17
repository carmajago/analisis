using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IniciarSimulacion : MonoBehaviour {

    public Transform trCamera;


    CargarViaLactea cargar;



	void Start () {
        cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();

        foreach (var item in cargar.viaLactea.Nebulosas)
        {
            if (!item.visitado)
            {
                item.visitado = true;
                NebulosaSingleton ns = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
                ns.nebulosa = item;

                foreach (var sistemas in item.sistemasPlanetarios)
                {
                    foreach (var planeta in sistemas.nodos)
                    {
                        if (planeta.teletransportador.planetaFK != 0)
                        {
                            item.tieneTeletransportador = true;
                        }
                    }
                }

                if(item.tieneTeletransportador)
                StartCoroutine(animacionIrANebulosa(new Vector3(item.x, item.y, item.z)));
                break;
            }
            if (item == cargar.viaLactea.Nebulosas[cargar.viaLactea.Nebulosas.Count - 1])
            {
                NaveEspacial.naveEspacial.finSimulacion = true;
            }
        }

      
        //volver a tierra

    }

    IEnumerator animacionIrANebulosa(Vector3 pos)
    {
        yield return new WaitForSeconds(4f);
        GetComponent<PlayableDirector>().enabled = false;
        //GameObject canvas = GameObject.FindGameObjectWithTag("CameraAnimation");
        //canvas.GetComponentInChildren<Canvas>().enabled = true;
        //Animator animator = canvas.GetComponent<Animator>();
        //animator.SetTrigger("Exit");
       


        while ((pos - trCamera.position).magnitude > 10)
        {
           
            trCamera.position = Vector3.Lerp(trCamera.position, pos, 3f * Time.deltaTime);
            yield return new WaitForSeconds(0.016f);
        }
        LevelLoader levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        levelLoader.loadLevel("Nebulosa"); 
       // SceneManager.LoadSceneAsync("EditorNebulosa", LoadSceneMode.Additive);
    }

}
