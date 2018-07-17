using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Esta clase establece si vale la pena comprar mejoras
/// y cuando comprarlas
/// Esta clase esta en a nave espacial
/// </summary>
public class Mejoras : MonoBehaviour
{

    NaveEspacial nave;


    private void Start()
    {
        nave = GetComponent<NaveEspacial>();
    }

    private void LateUpdate()
    {
        escudoMultinucleo();
        canonTanix();
        blindaje();
        propulsorOnix();
        plasma();
        capacidadDepositos();
        vida();
        capacidadDepositos();
        capacidadCombustible();
    }


    public void escudoMultinucleo()
    {

        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.escudoMultinucleo < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > EscudoMultinucleo.IRIDIO
                                && nave.paladio >EscudoMultinucleo.PALADIO
                                && nave.platino > EscudoMultinucleo.PLATINO
                                && nave.elementoZero > EscudoMultinucleo.ELEMENTO_ZERO))
            {
                nave.vida += 400;
                nave.escudoMultinucleo++;
                nave.iridio -= EscudoMultinucleo.IRIDIO;
                nave.paladio -= EscudoMultinucleo.PALADIO;
                nave.platino -= EscudoMultinucleo.PLATINO;
                nave.elementoZero -= EscudoMultinucleo.ELEMENTO_ZERO;
            }
        }
       
    }
    public void canonTanix()
    {
        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.canonTanix < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > CanonTanix.IRIDIO
                                && nave.paladio > CanonTanix.PALADIO
                                && nave.platino > CanonTanix.PLATINO
                                && nave.elementoZero > CanonTanix.ELEMENTO_ZERO))
            {
                nave.canonTanix++;
                nave.iridio -= CanonTanix.IRIDIO;
                nave.paladio -= CanonTanix.PALADIO;
                nave.platino -= CanonTanix.PLATINO;
                nave.elementoZero -= CanonTanix.ELEMENTO_ZERO;
            }
        }
    }

    public void blindaje()
    {
        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.blindaje < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > Blindaje.IRIDIO
                                && nave.paladio > Blindaje.PALADIO
                                && nave.platino > Blindaje.PLATINO
                                && nave.elementoZero > Blindaje.ELEMENTO_ZERO))
            {

                nave.vida += 1200;
                nave.blindaje++;
                nave.iridio -= Blindaje.IRIDIO;
                nave.paladio -= Blindaje.PALADIO;
                nave.platino -= CanonTanix.PLATINO;
                nave.elementoZero -= Blindaje.ELEMENTO_ZERO;
            }
        }

    }

    /// <summary>
    /// incrementar la velocidad en un 20%
    /// </summary>
    public void propulsorOnix()
    {

        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.propulsorOnix < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > PropulsorOnix.IRIDIO
                                && nave.paladio > PropulsorOnix.PALADIO
                                && nave.platino > PropulsorOnix.PLATINO
                                && nave.elementoZero > PropulsorOnix.ELEMENTO_ZERO))
            {
                nave.velocidadplanetas *= 1.2f;
                nave.velocidadplanetas *= 1.2f;
                nave.propulsorOnix++;
                nave.iridio -= PropulsorOnix.IRIDIO;
                nave.paladio -= PropulsorOnix.PALADIO;
                nave.platino -= PropulsorOnix.PLATINO;
                nave.elementoZero -= PropulsorOnix.ELEMENTO_ZERO;
            }
        }
    }
    /// <summary>
    /// incrementa el daño base 100 unidades
    /// </summary>
    public void plasma()
    {
        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.canonPlanma < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > CanonPlasma.IRIDIO
                                && nave.paladio > CanonPlasma.PALADIO
                                && nave.platino > CanonPlasma.PLATINO
                                && nave.elementoZero > CanonPlasma.ELEMENTO_ZERO))
            {
                nave.danoBase += 100;
                nave.canonPlanma++;
                nave.iridio -= CanonPlasma.IRIDIO;
                nave.paladio -= CanonPlasma.PALADIO;
                nave.platino -= CanonPlasma.PLATINO;
                nave.elementoZero -= CanonPlasma.ELEMENTO_ZERO;
            }
        }
    }
    public void capacidadDepositos()
    {
        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.capacidadDeposito < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > CapacidadDepositos.IRIDIO
                                && nave.paladio > CapacidadDepositos.PALADIO
                                && nave.platino > CapacidadDepositos.PLATINO
                                && nave.elementoZero > CapacidadDepositos.ELEMENTO_ZERO))
            {
                Constantes.LIMITE_IRIDIO *=1.5f;
                Constantes.LIMITE_PALADIO *= 1.5f;
                Constantes.LIMITE_PLATINO *= 1.5f;
                Constantes.LIMITE_ELEMENTOZERO *= 1.5f;

                nave.capacidadDeposito++;
                nave.iridio -= CapacidadDepositos.IRIDIO;
                nave.paladio -= CapacidadDepositos.PALADIO;
                nave.platino -= CapacidadDepositos.PLATINO;
                nave.elementoZero -= CapacidadDepositos.ELEMENTO_ZERO;
            }
        }
    }
    public void vida()
    {
        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;


        if (nave.vida < 50 && (nave.iridio > VidaInfinity.IRIDIO
                               && nave.paladio > VidaInfinity.PALADIO
                               && nave.platino > VidaInfinity.PLATINO
                               && nave.elementoZero > VidaInfinity.ELEMENTO_ZERO))
        {
            nave.iridio -= VidaInfinity.IRIDIO;
            nave.paladio -= VidaInfinity.PALADIO;
            nave.platino -= VidaInfinity.PLATINO;
            nave.elementoZero -= VidaInfinity.ELEMENTO_ZERO;
            nave.vidaInfinity++;
        }

    }
    public void capacidadCombustible()
    {
        float porcentajeVida = nave.vida * 100 / Constantes.LIMITE_VIDA;
        float porcentajeCombustible = nave.combustible * 100 / Constantes.LIMITE_COMBUSTIBLE;
        float porcentajeSondas = nave.sondas * 100 / Constantes.CAPACIDAD_SONDAS;

        if (nave.capacidaCombustible < 1)
        {
            if ((porcentajeVida > 50) && (porcentajeSondas > 50) && (porcentajeCombustible > 50) && (nave.iridio > CapacidadCombustible.IRIDIO
                                && nave.paladio > CapacidadCombustible.PALADIO
                                && nave.platino > CapacidadCombustible.PLATINO
                                && nave.elementoZero > CapacidadCombustible.ELEMENTO_ZERO))
            {

                Constantes.LIMITE_COMBUSTIBLE *= 1.5f;
                nave.capacidaCombustible++;
                nave.iridio -= CapacidadCombustible.IRIDIO;
                nave.paladio -= CapacidadCombustible.PALADIO;
                nave.platino -= CapacidadCombustible.PLATINO;
                nave.elementoZero -= CapacidadCombustible.ELEMENTO_ZERO;
            }
        }
    }

}
