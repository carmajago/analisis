using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mejora:MonoBehaviour  {

    NaveEspacial nave;





    public void escudoMultinucleo()
    {
        
        nave.vida += 400;
    }
    public void canonTanix()
    {

    } 
    
    public void blindaje()
    {

    }
    public void propulsorOnix()
    {

    }
    public void plasma()
    {

    }
    public void capacidadDepositos()
    {

    }
    public void vida()
    {
        float porcentaje = nave.vida*100 / Constantes.LIMITE_VIDA;
        if (nave.vida < 50)
        {

        }

    }
    public void capacidadCombustible()
    {
            
    }

}
