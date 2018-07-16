using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GastoSondas
{

    /// <summary>
    /// Calcula si vale la pena gastar sondas en un planeta
    /// </summary>
    /// <param name="planeta"></param>
    public static bool valeLaPenaGastarSondas(Planeta planeta)
    {
        double iridio = planeta.iridio * Constantes.IRIDIO_VALOR;
        double paladio = planeta.paladio * Constantes.PALADIO_VALOR;
        double platino = planeta.platino * Constantes.PLATINO_VALOR;
        double elementoZero = planeta.elementoZero * Constantes.ELEMENTO_ZERO_VALOR;
        double total = iridio + paladio + platino + elementoZero;


        double iridioValor = Constantes.IRIDIO_VS * Constantes.IRIDIO_VALOR;
        double paladioValor = Constantes.PALADIO_VS * Constantes.PALADIO_VALOR;
        double platinoValor = Constantes.PLATINO_VS * Constantes.PLATINO_VALOR;
        double elementoZeroValor = Constantes.ELEMENTO_ZERO_VS * Constantes.ELEMENTO_ZERO_VALOR;
        double totalValor = iridioValor + paladioValor + platinoValor + elementoZeroValor;

        return total > totalValor;
    }
}
