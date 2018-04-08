using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BotonViaLactea : MonoBehaviour {

    public ViaLactea viaLactea;
    public Animator animatorMenuPpal;


    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        StartCoroutine(animacionSalir());
    }

    IEnumerator animacionSalir()
    {
        animatorMenuPpal.SetTrigger("exit");
        GameObject particulas = Camera.main.transform.GetChild(0).gameObject;
        particulas.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.6f);
        Camera.main.GetComponent<Animator>().SetTrigger("exit");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Editor", LoadSceneMode.Single);

    }
}
