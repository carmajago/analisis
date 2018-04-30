using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CameraAnimations  {
    

   public static IEnumerator animacionSalirMenuCrear()
    {
        GameObject particulas = Camera.main.transform.GetChild(0).gameObject;
        particulas.GetComponent<ParticleSystem>().Play();
        Camera.main.GetComponent<Animator>().SetTrigger("exit");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Editor", LoadSceneMode.Single);

    }

}
