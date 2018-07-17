using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pausar : MonoBehaviour {

    public static Pausar pausar;
    public GameObject menuPausa;


    private void Awake()
    {
        if (pausar == null)
        {
            pausar = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            menuPausa.SetActive(true);
        }
    }

   public void desPausar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
    }
    public void irAHome()
    {
        Time.timeScale = 1;
            menuPausa.SetActive(false);
        LevelLoader lv= GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        lv.loadLevel("Home");   
    }
    public void mejoras()
    {

    }

}
