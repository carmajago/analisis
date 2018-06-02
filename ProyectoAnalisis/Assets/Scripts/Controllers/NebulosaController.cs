using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NebulosaController : MonoBehaviour {


    public TextMeshProUGUI nombreNebulosa;
    public TextMeshProUGUI Peligrosa;

    void Start () {
        StartCoroutine(desctivarCanvas());
        NebulosaSingleton ns = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
        ns.setNebulosa(NebulosaService.GetNebulosa(ns.nebulosa.id));
        nombreNebulosa.text = ns.nebulosa.nombre;
        Peligrosa.enabled = ns.nebulosa.danger;
        ns.cargar();
    }

    IEnumerator desctivarCanvas()
    {
        yield return new WaitForSeconds(1);
        GameObject canvas = GameObject.FindGameObjectWithTag("CameraAnimation");
        canvas.GetComponentInChildren<Canvas>().enabled = false;
    }
    public void irAViaLactea()
    {
        StartCoroutine(animacionIrAVialactea());
    }
    IEnumerator animacionIrAVialactea()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("CameraAnimation");
        canvas.GetComponentInChildren<Canvas>().enabled = true;
        Animator animator = canvas.GetComponent<Animator>();
        animator.SetTrigger("Exit");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("ViaLactea", LoadSceneMode.Single);
    }

}
