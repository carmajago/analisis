using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecargarCombustible : MonoBehaviour {

    /// <summary>
    /// La nave recarga gasolina y compra sondas
    /// </summary>
    /// <param name="inicio"></param>
    public void recargarGasolinaYsondas(SistemaPlanetario inicio, ref float gasolina, ref int sondas, ref double[] materiales, ref double rg)
    {

        if (inicio.tieneDeposito)
        {
            float gasolinaPorcentaje = gasolina / Constantes.LIMITE_COMBUSTIBLE;
            float sondasPorcentaje = sondas / Constantes.CAPACIDAD_SONDAS;


            if (gasolinaPorcentaje < sondasPorcentaje)
            {
                while (((gasolinaPorcentaje < sondasPorcentaje) && puedeComprarCombustible(materiales)) && gasolinaPorcentaje * 100 < 99)
                {

                    gasolina += comprarCombustible(ref materiales, ref rg);

                    gasolinaPorcentaje = gasolina / Constantes.LIMITE_COMBUSTIBLE;
                }
            }
            if (sondasPorcentaje < gasolinaPorcentaje)
            {
                while (((sondasPorcentaje < gasolinaPorcentaje) && puedeComprarSondas(materiales)) && sondasPorcentaje * 100 < 99)
                {

                    sondas += comprarSondas(ref materiales, ref rg);

                    sondasPorcentaje = sondas / Constantes.CAPACIDAD_SONDAS;
                }
            }
            while ((puedeComprarCombustible(materiales) && gasolinaPorcentaje * 100 < 99) || (puedeComprarSondas(materiales) && sondasPorcentaje * 100 < 99))
            {
                if (puedeComprarCombustible(materiales) && gasolinaPorcentaje * 100 < 99)
                {
                    gasolina += comprarCombustible(ref materiales, ref rg);
                    gasolinaPorcentaje = gasolina / Constantes.LIMITE_COMBUSTIBLE;

                }
                if (puedeComprarSondas(materiales) && sondasPorcentaje * 100 < 99)
                {
                    sondas += comprarSondas(ref materiales, ref rg);
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

        return (materiales[0] >= iridio && materiales[1] >= paladio && materiales[2] >= platino && materiales[3] >= elementoZero);

    }
    bool puedeComprarSondas(double[] materiales)
    {
        //   int cantidadSondas =(int) Constantes.CAPACIDAD_SONDAS/ 100; //total litros por 1%

        float iridio = Constantes.IRIDIO_VS;
        float paladio = Constantes.PALADIO_VS;
        float platino = Constantes.PLATINO_VS;
        float elementoZero = Constantes.ELEMENTO_ZERO_VS;

        return (materiales[0] >= iridio && materiales[1] >= paladio && materiales[2] >= platino && materiales[3] > elementoZero);

    }
    public int comprarSondas(ref double[] materiales, ref double rg)
    {
        //int cantidadSondas =(int) Constantes.LIMITE_COMBUSTIBLE / 100; //total litros por 1%

        materiales[0] -= Constantes.IRIDIO_VS;
        materiales[1] -= Constantes.PALADIO_VS;
        materiales[2] -= Constantes.PLATINO_VS;
        materiales[3] -= Constantes.ELEMENTO_ZERO_VS;

        rg -= Constantes.IRIDIO_VS*Constantes.IRIDIO_VALOR;
        rg -= Constantes.PALADIO_VS*Constantes.PALADIO_VALOR;
        rg -= Constantes.PLATINO_VS*Constantes.PLATINO_VALOR;
        rg -= Constantes.ELEMENTO_ZERO_VS*Constantes.ELEMENTO_ZERO_VALOR;

        return 1;
    }


    public float comprarCombustible(ref double[] materiales, ref double rg)
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

}
