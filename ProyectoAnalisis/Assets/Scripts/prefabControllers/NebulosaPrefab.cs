using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NebulosaPrefab : MonoBehaviour {

    public Nebulosa nebulosa;

    private GameObject canvasNebulosa;
    private GameObject infoNebulosa;
    private bool activo = false;
    private Transform tr;
    void Start() {
        tr = GetComponent<Transform>();
        infoNebulosa = transform.Find("InfoNebulosa").gameObject;
        infoNebulosa.SetActive(activo);

        canvasNebulosa = transform.Find("Nebulosa").gameObject;
        Button btn = canvasNebulosa.transform.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(abrirInfo);
        refrescarInfo();

    }
    /// <summary>
    /// Verifica que no se haya editado la nebulosa en la escena y si se edita envia la nueva información al servidor.
    /// </summary>
    public void LateUpdate()
    {
        Vector3 posicion = new Vector3(nebulosa.x, nebulosa.y, nebulosa.z);
        if (tr.position != posicion && Input.GetMouseButtonUp(0))
        {
            nebulosa.x = tr.position.x;
            nebulosa.y = tr.position.y;
            nebulosa.z = tr.position.z;
           
            ApiCalls.PutNebulosa(nebulosa);
        }
    }

    public void setNebulosa(Nebulosa _nebulosa)
    {
        nebulosa = _nebulosa;
    }
    /// <summary>
    /// Actualiza la información de la nebulosa con los datos de la escena
    /// Este metodo es utilizado en la escena editor
    /// </summary>
    public void actualizarDatos()
    {
        nebulosa.x = transform.position.x;
        nebulosa.y = transform.position.y;
        nebulosa.z = transform.position.z;
        if (nebulosa.ViaLacteaFK == 0)
        {
            CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
            nebulosa.ViaLacteaFK =cargar.viaLactea.id;
        }
    }
    public void abrirInfo () {
        
        activo = !activo;
        infoNebulosa.SetActive(activo);
    }

    public void refrescarInfo()
    {
        TextMeshProUGUI nombre = infoNebulosa.transform.Find("Nombre").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI sistemas = infoNebulosa.transform.Find("Sistemas").GetComponent<TextMeshProUGUI>();
        Button button = infoNebulosa.transform.Find("Button").GetComponent<Button>();

        button.onClick.AddListener(irANebulosa);

        nombre.text = nebulosa.nombre;
        sistemas.text ="Sistemas planetarios:"+nebulosa.totalSistemas;

    }


    public void irANebulosa()
    {
        NebulosaSingleton ns = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
        ns.nebulosa = nebulosa;
        SceneManager.LoadScene("EditorNebulosa", LoadSceneMode.Single);

    }
}
