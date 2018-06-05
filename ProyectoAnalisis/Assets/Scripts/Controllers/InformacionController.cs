using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InformacionController : MonoBehaviour {

    public void irAHome()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }
}
