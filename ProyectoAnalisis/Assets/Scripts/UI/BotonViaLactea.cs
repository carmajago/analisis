using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonViaLactea : MonoBehaviour {

    public ViaLactea viaLactea;
    public Animator animatorMenuPpal;

    public string escena;//El nombre de la escena  a la cual se va a cargar
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
       
    }

    void TaskOnClick()
    {
        CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
        cargar.setViaLactea(viaLactea);
        StartCoroutine(animacionSalir());
       
    }

    IEnumerator animacionSalir()
    {
        animatorMenuPpal.SetTrigger("exit");
        
        yield return new WaitForSeconds(0.6f);
     
        StartCoroutine(CameraAnimations.animacionSalirMenuCrear(escena));
        yield return new WaitForSeconds(2f);

        LevelLoader LevelL= GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

        LevelL.loadLevel(escena);
       // SceneManager.LoadScene(escena, LoadSceneMode.Single);

    }

  
   


}
