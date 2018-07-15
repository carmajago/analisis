using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecorridoSistemas : MonoBehaviour {

	public NaveEspacial nave;
    public double relacionGananciaGlobal;
    public double gasolinaGlobal;
    public List<SistemaPlanetario> caminoGlobal;
    private SistemaPlanetario nodoInicial=null;

    private Nebulosa nebulosaG;

    void Start () {
		
	}
    public void iniciarAlgoritmo(Nebulosa nebulosa)
    {

        //Duplicar nebulosa

        nebulosaG = nebulosa;
        // buscar el nodo inicial
        foreach (var item in nebulosa.sistemasPlanetarios)
        {
            if (item.tieneTeletransportador)
            {
                nodoInicial = item;
            }
        }
        List<SistemaPlanetario> camino=new List<SistemaPlanetario>();
        nave = GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>();
        double[] materiales = new double[4];
        materiales[0] = nave.iridio;
        materiales[1] = nave.paladio;
        materiales[2] = nave.platino;
        materiales[3] = nave.elementoZero;
        if (nodoInicial != null)
        {
           

            buscarCamino(nodoInicial, 0, nebulosa.grafo, nave.combustible, camino, 0f, nave.sondas, materiales);
        }

           

        foreach (var item in caminoGlobal)
        {
            Debug.Log(item.nombre + " - "+caminoGlobal.Count);
        }
        Debug.Log(gasolinaGlobal + "Gasolina");
        Debug.Log(relacionGananciaGlobal + "Relacion Ganancia");
       



    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inicio"></param>
    /// <param name="relacionGanancia"></param>
    /// <param name="grafo"></param>
    /// <param name="gasolina">La gasolina que riene la nave al comenzar el método</param>
    /// <param name="camino"></param>
    /// <param name="distancia"></param>
    private void buscarCamino(SistemaPlanetario inicio,double relacionGanancia, List<AristaSistema> grafo,float gasolina,List<SistemaPlanetario> camino,float distancia,int sondas,double[] materiales){
        inicio.visitado = true;


        recargarGasolinaYsondas(inicio,ref gasolina,ref sondas,ref materiales,ref relacionGanancia);
        camino.Add(inicio);

        List<Planeta> planetasTemp = new List<Planeta>();
        planetasTemp= clonarPlanetas(inicio.nodos);
        double[] materialesTemp = clonarMateriales(materiales);


        //int sondasTemp = inicio.recorrido.sondasGlobal;
        double rg = calcularRelacionGanancia(inicio,relacionGanancia,distancia,ref sondas,ref materiales);
        
        foreach (var item in buscarAdyacentes(inicio,grafo))
        {
            if (!item.visitado)
            {
             
                float dis = ((new Vector3(inicio.x, inicio.y, inicio.z)) - (new Vector3(item.x, item.y, item.z))).magnitude;
                if (!caminoImposible(dis, gasolina))
                {
                    buscarCamino(item, rg, grafo, gasolina - (dis / Constantes.GASTO_COMBUSTIBLE), camino, dis, sondas, materiales);
                }
            } 
        }
        SistemaPlanetario inicioAux = inicio;
        List<AristaSistema> grafoDuplicado = clonarGrafo(grafo,ref inicioAux);

        inicio.visitado = false;
        camino.Remove(inicio);

       
        
        buscarOrigen(inicioAux,rg,grafoDuplicado,gasolina,sondas,0,camino,materiales);
        devolverMateriales(inicio.nodos, planetasTemp,ref materiales);
        devolverMaterialesNave(ref materiales,materialesTemp);

    }
    private double[] clonarMateriales(double[] materiales)
    {
        double[] copia=new double[4];
        for (int i = 0; i < materiales.Length; i++)
        {
            copia[i] = materiales[i];
        }
        return copia;
    }

    /// <summary>
    /// Devuelve los materiales a la nave
    /// </summary>
    /// <param name="original"></param>
    /// <param name="copia"></param>
    private void devolverMaterialesNave(ref double[] original,double[] copia)
    {
        for (int i = 0; i < copia.Length; i++)
        {
            original[i] = copia[i];
        }
    }

    public List<Planeta> clonarPlanetas(List<Planeta> original)
    {
        List<Planeta> copia = new List<Planeta>();
        foreach (var item in original)
        {
            Planeta planetaCopia = new Planeta();
            planetaCopia.id = item.id;
            planetaCopia.iridio = item.iridio;
            planetaCopia.paladio = item.paladio;
            planetaCopia.platino = item.platino;
            planetaCopia.elementoZero = item.elementoZero;

            copia.Add(planetaCopia);
        }
        return copia;
    }

    /// <summary>
    /// devuelve los materiales a los planetas
    /// </summary>
    /// <param name="original"></param>
    /// <param name="copia"></param>
    /// <param name="materiales"></param>
    public void devolverMateriales(List<Planeta> original, List<Planeta> copia,ref double[] materiales)
    {
        foreach (var item in copia)
        {
            foreach (var itemOriginal in original)
            {
                if(itemOriginal.id == item.id)
                {
                    itemOriginal.iridio = item.iridio;
                    itemOriginal.paladio = item.paladio;
                    itemOriginal.platino = item.platino;
                    itemOriginal.elementoZero = item.elementoZero;

                    materiales[0] -= item.iridio;
                    materiales[1] -= item.paladio;
                    materiales[2] -= item.platino;
                    materiales[3] -= item.elementoZero;
                }
            }
        }
    }

    /// <summary>
    /// La nave recarga gasolina y compra sondas
    /// </summary>
    /// <param name="inicio"></param>
    public void recargarGasolinaYsondas(SistemaPlanetario inicio,ref float gasolina ,ref int sondas,ref double[] materiales,ref double rg)
    {

        if (inicio.tieneDeposito)
        {
            float gasolinaPorcentaje = gasolina / Constantes.LIMITE_COMBUSTIBLE;
            float sondasPorcentaje = sondas / Constantes.CAPACIDAD_SONDAS;


            if (gasolinaPorcentaje < sondasPorcentaje)
            {
                while ( ((gasolinaPorcentaje < sondasPorcentaje) && puedeComprarCombustible(materiales)) && gasolinaPorcentaje*100<99)
                {
                   
                        gasolina += comprarCombustible(ref materiales, ref rg);

                     gasolinaPorcentaje = gasolina / Constantes.LIMITE_COMBUSTIBLE;
                }
            }
            if (sondasPorcentaje < gasolinaPorcentaje)
            {
                while (((sondasPorcentaje < gasolinaPorcentaje) && puedeComprarSondas(materiales)) && sondasPorcentaje * 100 < 99)
                {

                    sondas += comprarSondas(ref materiales,ref rg);
                   
                    sondasPorcentaje = sondas / Constantes.CAPACIDAD_SONDAS;
                }
            }
            while ((puedeComprarCombustible(materiales) && gasolinaPorcentaje * 100 < 99) || (puedeComprarSondas(materiales) && sondasPorcentaje * 100 < 99) )
            {
                if (puedeComprarCombustible(materiales) && gasolinaPorcentaje * 100 < 99)
                {
                   gasolina += comprarCombustible(ref materiales, ref rg);
                    gasolinaPorcentaje = gasolina / Constantes.LIMITE_COMBUSTIBLE;
                    
                }
                if (puedeComprarSondas(materiales) && sondasPorcentaje * 100 < 99)
                {
                    sondas += comprarSondas(ref materiales,ref rg);
                    sondasPorcentaje = sondas / Constantes.CAPACIDAD_SONDAS;
                }
            }


         

        }
        
    }
    bool puedeComprarCombustible(double[] materiales)
    {
        float litrosCobustible = Constantes.LIMITE_COMBUSTIBLE / 100; //total litros por 1%

        float iridio = litrosCobustible * Constantes.IRIDIO_VC;
        float paladio = litrosCobustible * Constantes.PALADIO_VC;
        float platino = litrosCobustible * Constantes.PLATINO_VC;
        float elementoZero = litrosCobustible * Constantes.ELEMENTO_ZERO_VC;

        return (materiales[0] >= iridio && materiales[1] >= paladio && materiales[2] >= platino && materiales[3]>elementoZero);

    }
    bool puedeComprarSondas(double[] materiales)
    {
     //   int cantidadSondas =(int) Constantes.CAPACIDAD_SONDAS/ 100; //total litros por 1%

        float iridio =  Constantes.IRIDIO_VS;
        float paladio =  Constantes.PALADIO_VS;
        float platino =  Constantes.PLATINO_VS;
        float elementoZero =  Constantes.ELEMENTO_ZERO_VS;

        return (materiales[0] >= iridio && materiales[1] >= paladio && materiales[2] >= platino && materiales[3] > elementoZero);

    }
      public int comprarSondas(ref double[] materiales,ref double rg )
    {
        //int cantidadSondas =(int) Constantes.LIMITE_COMBUSTIBLE / 100; //total litros por 1%

        materiales[0] -=  Constantes.IRIDIO_VS;
        materiales[1] -=  Constantes.PALADIO_VS;
        materiales[2] -=  Constantes.PLATINO_VS;
        materiales[3] -=  Constantes.ELEMENTO_ZERO_VS;

        rg -= Constantes.IRIDIO_VS;
        rg -= Constantes.PALADIO_VS;
        rg -= Constantes.PLATINO_VS;
        rg -= Constantes.ELEMENTO_ZERO_VS;

        return 1;
    }


    public float comprarCombustible(ref double[] materiales ,ref double rg)
    {
        float litrosCombustible = Constantes.LIMITE_COMBUSTIBLE / 100; //total litros por 1%

        materiales[0] -= litrosCombustible * Constantes.IRIDIO_VC;
        materiales[1] -= litrosCombustible * Constantes.PALADIO_VC;
        materiales[2] -= litrosCombustible * Constantes.PLATINO_VC;
        materiales[3] -= litrosCombustible * Constantes.ELEMENTO_ZERO_VC;

        rg -= litrosCombustible * Constantes.IRIDIO_VC * Constantes.IRIDIO_VALOR;
        rg -= litrosCombustible * Constantes.PALADIO_VC * Constantes.PALADIO_VALOR;
        rg -= litrosCombustible * Constantes.PLATINO_VC * Constantes.PLATINO_VALOR;
        rg -= litrosCombustible * Constantes.ELEMENTO_ZERO_VC * Constantes.ELEMENTO_ZERO_VALOR;
        return litrosCombustible;

    }


    /// <summary>
    /// Calcula si con la gasolina actual es suficiente para ir a ese sistema 
    /// </summary>
    /// <param name="distancia">La distancia a recorrer</param>
    /// <param name="gasolina">La gasolina actual de la nave</param>
    /// <returns>True si es imposible y False si es posible</returns>
    public bool caminoImposible(float distancia,float gasolina)
    {
        return (gasolina < (distancia / Constantes.GASTO_COMBUSTIBLE));
    }

    private void buscarOrigen(SistemaPlanetario inicio,double relacionGanancia,List<AristaSistema> grafo, float gasolina,int sondas,float distancia,List<SistemaPlanetario> camino,double[] materiales)
    {
        inicio.visitado = true;
       // recargarGasolinaYsondas(inicio, ref gasolina, ref sondas, ref materiales);
        camino.Add(inicio);


        List<Planeta> planetasTemp = new List<Planeta>();
        planetasTemp = clonarPlanetas(inicio.nodos);
        double[] materialesTemp = clonarMateriales(materiales);

        double rg = calcularRelacionGanancia(inicio,relacionGanancia,distancia,ref sondas,ref materiales);


        if (inicio.id != nodoInicial.id)
        {
            foreach (var item in buscarAdyacentes(inicio, grafo))
            {
               
                if (!item.visitado)
                {
                    float dis = ((new Vector3(inicio.x, inicio.y, inicio.z)) - (new Vector3(item.x, item.y, item.z))).magnitude;
                    if (!caminoImposible(dis, gasolina))
                    {

                        buscarOrigen(item, rg, grafo, gasolina - (dis / Constantes.GASTO_COMBUSTIBLE), sondas, dis, camino, materiales);
                    }
                }
            }
        }
        else
        {
            Debug.Log(rg + " RGL");
            Debug.Log(gasolina + " GASL");
            Debug.Log(sondas + " RGL");
            string salida="";
            foreach (var item in camino)
            {
                salida += item.nombre+"-";
            }
            Debug.Log(salida);

            if (mejorCamino(ref rg, gasolina, sondas))
            {
                clonarCamino(camino);
                relacionGananciaGlobal = rg;
                gasolinaGlobal = gasolina;
            }
        }

        ////replantear condicional

        devolverMateriales(inicio.nodos, planetasTemp, ref materiales);
        devolverMaterialesNave(ref materiales, materialesTemp);
        inicio.visitado = false;
        camino.Remove(inicio);
    }

    /// <summary>
    /// Establece la mejor relacion entre el camino global y el actual-
    /// </summary>
    /// <param name="relacionGanancia"></param>
    /// <param name="combustible"></param>
    /// <returns></returns>
    private bool mejorCamino(ref double relacionGanancia,double gasolina,int sondas)
    {
        double iridio = ((gasolina * Constantes.IRIDIO_VC) + (sondas * Constantes.IRIDIO_VS)) * Constantes.IRIDIO_VALOR;
        double paladio = ((gasolina * Constantes.PALADIO_VC) + (sondas * Constantes.PALADIO_VS))*Constantes.PALADIO_VALOR;
        double platino = ((gasolina * Constantes.PLATINO_VC) + (sondas * Constantes.PLATINO_VS))*Constantes.PLATINO_VALOR;
        double elementoZero = ((gasolina * Constantes.ELEMENTO_ZERO_VC) + (sondas * Constantes.ELEMENTO_ZERO_VS))* Constantes.ELEMENTO_ZERO_VALOR;


        relacionGanancia+=(iridio + paladio + platino + elementoZero);

        
        
        return relacionGananciaGlobal < relacionGanancia;
    }

    public List<SistemaPlanetario> buscarAdyacentes(SistemaPlanetario sistema, List<AristaSistema> grafo)
    {
        List<SistemaPlanetario> adyacentes = new List<SistemaPlanetario>();
        foreach (var item in grafo)
        {
            if (item.origen.id == sistema.id)
            {
                adyacentes.Add(item.destino);
            }
            if (item.destino.id == sistema.id)
            {
                adyacentes.Add(item.origen);
            }
        }
        return adyacentes;
    }
   
    /// <summary>
    /// este métoto duplica un grafo y desmarca los nodos como no visitados.
    /// </summary>
    /// <returns></returns>
    public List<AristaSistema> clonarGrafo(List<AristaSistema> grafo, ref SistemaPlanetario inicioAux)
    {
        List<AristaSistema> grafoCopia=new List<AristaSistema>();

        List<SistemaPlanetario> sistemasAux=new List<SistemaPlanetario>();


        foreach (var item in grafo)
        {
            AristaSistema arCopia = new AristaSistema();
            bool origen=false;
            bool destino = false;
            foreach (var sistema in sistemasAux)
            {
                if (item.origen.id == sistema.id)
                {
                    arCopia.origen = sistema;
                    origen = true;
                }
                if (item.destino.id == sistema.id)
                {
                    arCopia.destino = sistema;
                    destino = true;
                }
            }
            if (!origen)
            {
                SistemaPlanetario orCopia = new SistemaPlanetario();
                orCopia.recorrido = item.origen.recorrido;
                orCopia.nombre = item.origen.nombre;
                orCopia.x = item.origen.x;
                orCopia.y = item.origen.y;
                orCopia.z = item.origen.z;
                orCopia.id = item.origen.id;
                orCopia.nodos = item.origen.nodos;
                sistemasAux.Add(orCopia);
                arCopia.origen = orCopia;
            }
            if (!destino)
            {
                SistemaPlanetario desCopia = new SistemaPlanetario();
                desCopia.recorrido = item.destino.recorrido;
                desCopia.nombre = item.destino.nombre;
                desCopia.x = item.destino.x;
                desCopia.y = item.destino.y;
                desCopia.z = item.destino.z;
                desCopia.id = item.destino.id;
                desCopia.nodos = item.destino.nodos;
                arCopia.destino = desCopia;

            }

            
            
            if (item.origen.id == inicioAux.id)
            {
                inicioAux = arCopia.origen ;
            }
            if(item.destino.id == inicioAux.id)
            {
                inicioAux = arCopia.destino; ;
            }

            grafoCopia.Add(arCopia);
        }

        return grafoCopia;

    }

    /// <summary>
    /// Calcula la relacion ganancia real que se puede obtener con un numero de sondas determinado teniendo en cuenta la gasolina que gasta
    /// SE SIMULA QUE SE ESTAN RESTANDO MATERIALES
    /// </summary>
    /// <param name="sistema"></param>
    /// <returns></returns>
    public double calcularRelacionGanancia(SistemaPlanetario inicio, double relacionGanancia,float distancia,ref int sondas,ref double[] materiales)
    {
        double iridio = 0;
        double paladio = 0;
        double platino = 0;
        double elementoZero = 0;

        foreach (var item in inicio.recorrido.caminoGlobal)
        {
            if (sondas >= 2 && valeLaPenaGastarSondas(item))
            {

                iridio += item.iridio;
                paladio += item.paladio;
                platino += item.platino;
                elementoZero += item.elementoZero;

                //La nave quedo con los materiales del planeta
                materiales[0] +=item.iridio;
                materiales[1] += item.paladio;
                materiales[2] += item.platino;
                materiales[3] += item.elementoZero;
                sondas -= 2;

                ///El plantera quedó sin materiales 
                item.iridio = 0;
                item.paladio = 0;
                item.platino = 0;
                item.elementoZero = 0;


                ///Limita materiales
                materiales[0] = Mathf.Clamp((float)materiales[0],0,Constantes.LIMITE_IRIDIO);
                materiales[1] = Mathf.Clamp((float)materiales[1], 0, Constantes.LIMITE_PALADIO);
                materiales[2] = Mathf.Clamp((float)materiales[2], 0, Constantes.LIMITE_PLATINO);
                materiales[3] = Mathf.Clamp((float)materiales[3], 0, Constantes.LIMITE_ELEMENTOZERO);
            }
        }
            
        iridio = (iridio*Constantes.IRIDIO_VALOR - (Constantes.IRIDIO_VC * Constantes.IRIDIO_VALOR * (distancia / Constantes.GASTO_COMBUSTIBLE)));
        paladio = (paladio * Constantes.PALADIO_VALOR - (Constantes.PALADIO_VC * Constantes.PALADIO_VALOR * (distancia / Constantes.GASTO_COMBUSTIBLE)));
        platino = (platino * Constantes.PLATINO_VALOR - (Constantes.PLATINO_VC * Constantes.PLATINO_VALOR * (distancia / Constantes.GASTO_COMBUSTIBLE)));
        elementoZero = (elementoZero * Constantes.ELEMENTO_ZERO_VALOR - (Constantes.ELEMENTO_ZERO_VC * Constantes.ELEMENTO_ZERO_VALOR * (distancia / Constantes.GASTO_COMBUSTIBLE)));

        return (iridio + paladio + platino + elementoZero) + relacionGanancia;
    }

    public void clonarCamino(List<SistemaPlanetario> lista)
    {
        caminoGlobal = new List<SistemaPlanetario>();

        foreach (var item in lista)
        {
            caminoGlobal.Add(item);
        }

    }

    /// <summary>
    /// Calcula si vale la pena gastar sondas en un planeta
    /// </summary>
    /// <param name="planeta"></param>
    public bool valeLaPenaGastarSondas(Planeta planeta)
    {
        double iridio = planeta.iridio * Constantes.IRIDIO_VALOR;
        double paladio = planeta.paladio * Constantes.PALADIO_VALOR;
        double platino = planeta.platino * Constantes.PLATINO_VALOR;
        double elementoZero = planeta.elementoZero * Constantes.ELEMENTO_ZERO_VALOR;
        double total = iridio + paladio + platino + elementoZero;


        double iridioValor =Constantes.IRIDIO_VS * Constantes.IRIDIO_VALOR;
        double paladioValor = Constantes.IRIDIO_VS * Constantes.PALADIO_VALOR;
        double platinoValor = Constantes.IRIDIO_VS * Constantes.PLATINO_VALOR;
        double elementoZeroValor = Constantes.IRIDIO_VS * Constantes.ELEMENTO_ZERO_VALOR;
        double totalValor = iridioValor + paladioValor + platinoValor + elementoZeroValor;

        return total > totalValor;
    }
}
