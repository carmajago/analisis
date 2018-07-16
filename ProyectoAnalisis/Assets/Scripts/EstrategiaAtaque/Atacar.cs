using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacar : MonoBehaviour {


    public int enemigosTipoA;
    public int enemigosTipoB;
    public int enemigosTipoC;

    public NaveEspacial nave;

    private void Start()
    {
        nave = GameObject.FindGameObjectWithTag("Nave").GetComponent<NaveEspacial>();
    }


    public void calcular()
    {
        if (valeLaPenaAtacar())
        {
            nave.vida -= EnemigoTipoA.DANO_ATAQUE*enemigosTipoA;
            nave.vida -= EnemigoTipoB.DANO_ATAQUE * enemigosTipoB;
            nave.vida -= EnemigoTipoC.DANO_ATAQUE * enemigosTipoC;
        }
    }

    private bool valeLaPenaAtacar()
    {
        double disparosNave = 0;
        disparosNave =(EnemigoTipoA.VIDA / nave.danoBase)*enemigosTipoA;
        disparosNave += (EnemigoTipoB.VIDA / nave.danoBase) * enemigosTipoB;
        disparosNave += (EnemigoTipoC.VIDA / nave.danoBase) * enemigosTipoC;

        double disparosEnemigo = 0;
        disparosEnemigo += (nave.vida / EnemigoTipoA.DANO_ATAQUE)*enemigosTipoA;
        disparosEnemigo += (nave.vida / EnemigoTipoB.DANO_ATAQUE) * enemigosTipoB;
        disparosEnemigo += (nave.vida / EnemigoTipoC.DANO_ATAQUE) * enemigosTipoC;

        return disparosEnemigo < disparosNave;
    }
}
