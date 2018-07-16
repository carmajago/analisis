using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveEspacial : MonoBehaviour
{

    [Header("Variables de simulación")]
    public float iridio;
    public float platino;
    public float paladio;
    public float elementoZero;
    public int sondas;
    public float combustible;
    public float vida=1200;
    public float danoBase=60;

    [Header("")]
    public Vector3 offsetSistema;
    public Vector3 offsetPlanetas;

    public float tiempoExtraccion = 5;
    public CanvasNaveEspacial canvasNave;
    public LayerMask capaDeNavegacion;
    public float velocidadSistemas = 200f;
    public float velocidadplanetas = 0.5f;

    public float posY = 0;


   
    public static NaveEspacial naveEspacial;
    public LineRenderer lineaPaso;

    #region planetaTemporal

    [HideInInspector]
    public float iridioPlanetaTemp;
    [HideInInspector]
    public float paladioPlanetaTemp;
    [HideInInspector]
    public float platinoPlanetaTemp;
    [HideInInspector]
    public float elementoZeroPlanetaTemp;
    [HideInInspector]
    public string nombrePlanetaTemp;
    [HideInInspector]
    public bool inPlaneta;
    #endregion planetaTemporal

    bool escapar = false;

    #region mejoras
    [HideInInspector]
    public int canonTanix;
    [HideInInspector]
    public int escudoMultinucleo;
    [HideInInspector]
    public int blindaje;
    [HideInInspector]
    public int propulsorOnix;
    [HideInInspector]
    public int canonPlanma;
    [HideInInspector]
    public int capacidadDeposito;
    [HideInInspector]
    public int vidaInfinity;
    [HideInInspector]
    public int capacidaCombustible;

    #endregion mejoras

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


    public void navegacionPlanetas()
    {
        //velocidad = velTemp/2;

        // posY = -40;
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

    }
    public void navegacionSistema()
    {
        //  velocidad = velTemp;

        // posY = 0;
        transform.localScale = new Vector3(5, 5, 5);
    }



    public IEnumerator sistemaDeNavegacion(List<SistemaPlanetario> MejorCamino)
    {

        transform.position = new Vector3(MejorCamino[0].x, 0, MejorCamino[0].z);
        
        lineaPaso.SetPosition(0,new Vector3(MejorCamino[0].x,0,MejorCamino[0].z));
        int i = 0;
        foreach (var sistema in MejorCamino)
        {

            lineaPaso.positionCount = i+1;

            navegacionSistema();


            GameObject circulo = new GameObject();
            circulo.transform.position = new Vector3(sistema.x, 0, sistema.z);
            circulo.transform.localScale = new Vector3(20, 20, 20);
            GameObject sistemaTemp = new GameObject();
            sistemaTemp.transform.parent = circulo.transform;
            sistemaTemp.transform.localPosition = new Vector3(0, -2, 0);
            sistemaTemp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            Vector3 target = new Vector3(sistema.x, 0, sistema.z);


            float combustibleTemp = ((transform.position - target).magnitude) * Constantes.GASTO_COMBUSTIBLE;
            float combustubleRestante = combustible - ((transform.position - target).magnitude) * Constantes.GASTO_COMBUSTIBLE;

            float gastoGasolina = 0;
            int entra = 0;
            while ((transform.position - target).magnitude != 0)
            {
                lineaPaso.SetPosition(i, new Vector3(transform.position.x, 0,transform.position.z));
                if (entra<=1)
                {
                    entra++;
                   
                    gastoGasolina = combustibleTemp * Constantes.GASTO_COMBUSTIBLE - (((transform.position - target).magnitude) * Constantes.GASTO_COMBUSTIBLE);
                }
                   combustible -=gastoGasolina;

                float step = velocidadSistemas * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);

                Vector3 targetDir = target - transform.position;


                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                Debug.DrawRay(transform.position, newDir, Color.red);
                // Move our position a step closer to the target.
                transform.rotation = Quaternion.LookRotation(newDir);

                Vector3 posicion = transform.position + offsetSistema;
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, posicion, 0.1f);

                yield return null;

            }
            i++;//esta es para las lineas
            combustible = combustubleRestante;

            #region RecargarCombustible
            ///Recarga combustible si es posible
            RecargarCombustible rc = new RecargarCombustible();
            double[] materiales = new double[4];
            materiales[0] = iridio;
            materiales[1] = paladio;
            materiales[2] = platino;
            materiales[3] = elementoZero;
            Debug.Log("Esto tenía:" + iridio);
            double rg = 0;
            rc.recargarGasolinaYsondas(sistema, ref combustible, ref sondas, ref materiales, ref rg);
        
            iridio = (float)materiales[0];
            paladio = (float)materiales[1];
            platino = (float)materiales[2];
            elementoZero = (float)materiales[3];

            Debug.Log("Esto tengo:" + iridio);
            #endregion RecargarCombustible


            GameObject planetaTemp = new GameObject();
            planetaTemp.transform.parent = sistemaTemp.transform;

            if (sistema.recorrido.caminoGlobal.Count > 0)
            {
                planetaTemp.transform.localPosition = new Vector3(sistema.recorrido.caminoGlobal[0].x, 0, sistema.recorrido.caminoGlobal[0].z);
                transform.position = planetaTemp.transform.position;
            }
            foreach (var planeta in sistema.recorrido.caminoGlobal)
            {


                navegacionPlanetas();
                planetaTemp.transform.localPosition = (new Vector3(planeta.x, 0, planeta.z));

                target = planetaTemp.transform.position;

                Vector3 posicion = transform.position + offsetPlanetas;
                Camera.main.transform.position = posicion;

                while ((transform.position - target).magnitude >= 0.1f)
                {


                  
                    float step = velocidadplanetas * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target, step);

                    Vector3 targetDir = target - transform.position;


                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                    Debug.DrawRay(transform.position, newDir, Color.red);
                    // Move our position a step closer to the target.
                    transform.rotation = Quaternion.LookRotation(newDir);

                    posicion = transform.position + offsetPlanetas;
                    Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, posicion, 0.1f); ;

                    yield return null;

                }


                #region EntrarAPlaneta


                Vector3 posiciont = new Vector3(-7, 0, 0);
                Vector3 rotacion = new Vector3(0, 90, 0);
                Vector3 escala = new Vector3(0.1f, 0.1f, 0.1f);

                transform.position += posiciont;
                transform.eulerAngles = rotacion;
                transform.localScale -= escala;


                Vector3 offsetExtraccion = new Vector3(5f, 19, 22);
                Camera.main.transform.position -= offsetExtraccion;

                if (sondas >= 2 && GastoSondas.valeLaPenaGastarSondas(planeta))
                {


                    sondas -= 2;
                    double iridioTemp = planeta.iridio / tiempoExtraccion;
                    double paladioTemp = planeta.paladio / tiempoExtraccion;
                    double platinoTemp = planeta.platino / tiempoExtraccion;
                    double elementoZeroTemp = planeta.elementoZero / tiempoExtraccion;


                    inPlaneta = true;
                    int contador = 0;
                    nombrePlanetaTemp = planeta.nombre;
                    while (contador < tiempoExtraccion)
                    {
                        if (escapar)
                        {
                            escapar = false;
                            break;
                        }

                        contador++;
                        iridio += (float)iridioTemp;
                        paladio += (float)paladioTemp;
                        platino += (float)platinoTemp;
                        elementoZero += (float)elementoZeroTemp;

                        planeta.iridio -= iridioTemp;
                        planeta.paladio -= paladioTemp;
                        planeta.platino -= platinoTemp;
                        planeta.elementoZero -= elementoZeroTemp;

                        iridioPlanetaTemp = (float)planeta.iridio;
                        paladioPlanetaTemp = (float)planeta.paladio;
                        platinoPlanetaTemp = (float)planeta.platino;
                        elementoZeroPlanetaTemp = (float)planeta.elementoZero;

                        iridioPlanetaTemp = Mathf.Clamp(iridioPlanetaTemp, 0, Mathf.Infinity);
                        platinoPlanetaTemp = Mathf.Clamp(platinoPlanetaTemp, 0, Mathf.Infinity);
                        paladioPlanetaTemp = Mathf.Clamp(paladioPlanetaTemp, 0, Mathf.Infinity);
                        elementoZeroPlanetaTemp = Mathf.Clamp(elementoZeroPlanetaTemp, 0, Mathf.Infinity);

                        yield return new WaitForSeconds(1f);
                    }
                }

                inPlaneta = false;
                transform.position -= posiciont;
                transform.eulerAngles = Vector3.zero;
                transform.localScale += escala;


                Camera.main.transform.position += offsetExtraccion;
                #endregion EntrarAPlaneta

            }


        }

        GameObject.FindGameObjectWithTag("Teletransportar").GetComponent<Teletransportar>().iniciarAnimacion();
        yield return new WaitForSeconds(2.1f);
        LevelLoader levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        levelLoader.loadLevel("ViaLactea");
    }



    public void huir()
    {
        escapar = true;
        Debug.Log("Chao Papá");
        //Chao papá
    }
    public void atacar()
    {
        Debug.Log("WIN");
    }

}
