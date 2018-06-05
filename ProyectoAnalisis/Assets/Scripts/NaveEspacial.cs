using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEspacial : MonoBehaviour {

    public CanvasNaveEspacial canvasNave;

    public float iridio;
    public float platino;
    public float paladio;
    public float elementoZero;
    public List<Sonda> sondas;
    public float combustible;
    public float velocidad = 200;

    public static NaveEspacial naveEspacial;

    private void Awake()
    {
        if (naveEspacial == null)
        {
            naveEspacial = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (naveEspacial != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Limita el numero maximo de materiales que puede almacenar la nave
    /// </summary>
    public void limiteMateriales()
    {
        iridio = Mathf.Clamp(iridio, 0, Constantes.LIMITE_IRIDIO);
        platino = Mathf.Clamp(platino, 0, Constantes.LIMITE_PLATINO);
        paladio = Mathf.Clamp(paladio, 0, Constantes.LIMITE_PALADIO);
        elementoZero = Mathf.Clamp(elementoZero, 0, Constantes.LIMITE_ELEMENTOZERO);

        combustible = Mathf.Clamp(combustible, 0, Constantes.LIMITE_COMBUSTIBLE);

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        sistemaDeNavegacion();
    }

    public void sistemaDeNavegacion()
    {
        Vector3 posMouse;
        Vector3 pos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(pos);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, 0));
        float distance;
        xy.Raycast(ray, out distance);
        posMouse = ray.GetPoint(distance);
        StopAllCoroutines();
        StartCoroutine(moverAunpunto(posMouse));

        
        
    }

    IEnumerator moverAunpunto(Vector3 target)
    {
        while ((transform.position - target).magnitude != 0.1f)
        {
            float step = velocidad * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            Vector3 targetDir = target - transform.position;


            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);
            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
            yield return null;
        }
       
    }

}
