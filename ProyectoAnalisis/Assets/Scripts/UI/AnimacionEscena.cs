using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionEscena : MonoBehaviour {

    public Animator canvasAnim;
	
	void Start () {
        canvasAnim = GetComponent<Animator>();
        //canvasAnim.SetTrigger("In");

        StartCoroutine(Inicio());
	}

    IEnumerator Inicio()
    {
        yield return new WaitForSeconds(1);
        GetComponentInChildren<Canvas>().enabled = false;
    }
	
   
	
}
