using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Esta Clase se encarga de Encontrar el mejor camino que tiene la nave espacial para recorrer un sistema planetario.
/// </summary>
public class RecorridoPlanetas : MonoBehaviour {

    public NaveEspacial nave;

    public double[] costoGlobal=new double[4];//0.iridio ,1.paladio, 2.platino , 3.eZero
    public List<Planeta> caminoGlobal=new List<Planeta>();
    public int sondasGlobal;
    public int RelacionGanancia;
    private void Start()
    {
        nave = GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>();
    }


    /// <summary>
    /// Este es un algoritmo voraz que se encarga de buscar el nodo inicial mas adecuado.
    /// </summary>
    /// <param name="planetas"></param>
    /// <param name="grafo"></param>
    public void buscarNodoInicial(List<Planeta> planetas,List<AristaNodo> grafo)
    {
        nave = GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>();
        double mayor = 0;
        Planeta planetaInicial=null;
        foreach (var item in planetas)
        {
            double iridio = nave.iridio + item.iridio;
            double paladio = nave.paladio + item.paladio;
            double platino = nave.platino + item.platino;
            double elementoZero = nave.elementoZero + item.elementoZero;

            iridio = Mathf.Clamp((float)iridio, 0, Constantes.IRIDIO_MAX); 
            platino = Mathf.Clamp((float)platino, 0, Constantes.PLATINO_MAX);
            paladio = Mathf.Clamp((float)paladio, 0, Constantes.PALADIO_MAX);
            elementoZero = Mathf.Clamp((float)elementoZero, 0, Constantes.ELEMENTOZERO_MAX);

            double total = iridio + paladio + platino + elementoZero;

            if (total > mayor)
            {
                mayor = total;
                planetaInicial = item;
            }
            
            if(item.teletransportador!=null || item.deposito != null)
            {
                planetaInicial = item;
                break;
            }
        }

    double[] costo= new double[4];
    List<Planeta> camino=new List<Planeta>();
        for (int i = 0; i < costo.Length; i++)
        {
            costo[i] = 0;
        }

        if(planetaInicial!=null)
    buscarCaminoPlanetas(planetaInicial,grafo,costo,camino,0);

        //foreach (var item in caminoGlobal)
        //{
        //    Debug.Log(caminoGlobal.Count+"-"+item.nombre);
        //}

    }
    /// <summary>
    /// Este método busca el camino  más eficiente entre planetas teniendo en cuenta las restricciones de
    /// número de sondas y cantidad de materiales que puede cargar la nave.
    /// El método es llamado por un algoritmo voraz que busca el mejor nodo inicial 
    /// </summary>
    /// <param name="inicial"></param> Este es el nodo donde actual que esta evaluando 
    /// <param name="grafo"></param> El grafo no varia y es una lista de aristas
    /// <param name="costo"></param> Costo es una variable auxiliar para llevar la cuenta del costo que se tiene con las diferentes rutas
    /// <param name="camino"></param> El camino temporal más eficiente
    /// <param name="sondas"></param>El numero de sondas que se van gastando según los planetas visitados  
    private void buscarCaminoPlanetas(Planeta inicial,List<AristaNodo> grafo,double[] costo,List<Planeta> camino,int sondas)
    {
        inicial.visitado = true;
        List<Planeta> adyacentes = buscarAdyacentes(inicial, grafo);

         camino.Add(inicial);
        double[] temporal = new double[4];
        temporal[0] = costo[0] + inicial.iridio;
        temporal[1] = costo[1] + inicial.paladio;
        temporal[2] = costo[2] + inicial.platino;
        temporal[3] = costo[3] + inicial.elementoZero;

        ///Con
        temporal[0] = Mathf.Clamp((float)temporal[0], 0, Constantes.IRIDIO_MAX);
        temporal[1] = Mathf.Clamp((float)temporal[1], 0, Constantes.PALADIO_MAX);
        temporal[2] = Mathf.Clamp((float)temporal[2], 0, Constantes.PLATINO_MIN);
        temporal[3] = Mathf.Clamp((float)temporal[3], 0, Constantes.ELEMENTOZERO_MAX);

        int s = sondas + 2;


        foreach (var item in adyacentes)
        {
            if (!item.visitado)
            {
                buscarCaminoPlanetas(item,grafo,temporal,camino,s);
            }
        }
        
        

        if((caminoGlobal.Count == 0 || camino.Count<=caminoGlobal.Count) && MejorCosto(temporal) && nave.sondas>=s){
            ClonarCamino(camino);
            costoGlobal = temporal;
            sondasGlobal = s;
        }

        inicial.visitado = false;
        camino.Remove(inicial);
      
    }
    /// <summary>
    /// Compara los costos de un camino global y un camino local
    /// </summary>
    /// <param name="costo"></param>
    /// <returns></returns>
    public bool MejorCosto(double[] costo)
    {
        double costoL=0;
        double costoG=0;

        for (int i = 0; i < 4; i++)
        {
            costoG += costoGlobal[i];
            costoL += costo[i];
        }
 
        return costoL >= costoG;

    }

    public List<Planeta> buscarAdyacentes(Planeta planeta,List<AristaNodo> grafo)
    {
        List<Planeta> adyacentes = new List<Planeta>();
        foreach (var item in grafo)
        {
            if(item.origen==planeta)
            {
                adyacentes.Add(item.destino);
            }
            if (item.destino == planeta)
            {
                adyacentes.Add(item.origen);
            }
        }


        return adyacentes;
    }

    public void ClonarCamino(List<Planeta> lista)
    {
        caminoGlobal = new List<Planeta>();

        foreach (var item in lista)
        {
            caminoGlobal.Add(item);
        }

    }
}
