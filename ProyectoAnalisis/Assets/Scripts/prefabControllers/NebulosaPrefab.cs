using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class NebulosaPrefab : MonoBehaviour
{

    public Nebulosa nebulosa;
    public string escena = "EditorNebulosa";

    private GameObject canvasNebulosa;
    private GameObject infoNebulosa;
    private bool activo = false;
    private Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
        infoNebulosa = transform.Find("InfoNebulosa").gameObject;
        infoNebulosa.SetActive(activo);

        canvasNebulosa = transform.Find("Nebulosa").gameObject;
        Button btn = canvasNebulosa.transform.Find("Button").GetComponent<Button>();
        btn.onClick.AddListener(abrirInfo);
        refrescarInfo();


        cambiarAPeligrosa();
        
    }

    public void cambiarAPeligrosa()
    {
    if (nebulosa.danger)
    {
        Image imagen = canvasNebulosa.transform.Find("Button").GetComponent<Image>();
        Image imagen2 = canvasNebulosa.transform.Find("Circle").GetComponent<Image>();

        imagen.color = Color.red;
        imagen2.color = Color.red;
    }

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

            NebulosaService.PutNebulosa(nebulosa);
        }
    }

    public void setNebulosa(Nebulosa _nebulosa)
    {
        nebulosa = _nebulosa;
    }
    /// <summary>
    /// Actualiza la información de la nebulosa con los datos de la escena
    /// Este metodo es utilizado en la escena editor
    /// Este método se utiliza cuando se crea una nebulosa
    /// </summary>
    public void actualizarDatos()
    {
        nebulosa.x = transform.position.x;
        nebulosa.y = transform.position.y;
        nebulosa.z = transform.position.z;
        if (nebulosa.ViaLacteaFK == 0)
        {
            CargarViaLactea cargar = GameObject.FindGameObjectWithTag("ViaLactea").GetComponent<CargarViaLactea>();
            nebulosa.ViaLacteaFK = cargar.viaLactea.id;
        }

    }
    public void abrirInfo()
    {

        activo = !activo;
        infoNebulosa.SetActive(activo);
    }

    public void refrescarInfo()
    {
        TextMeshProUGUI nombre = infoNebulosa.transform.Find("Nombre").GetComponent<TextMeshProUGUI>();

        Button button = infoNebulosa.transform.Find("Button").GetComponent<Button>();

        button.onClick.AddListener(irANebulosa);

        //if (nebulosa.nombre == "" && nebulosa.id != 0)
        //{
        //    StartCoroutine(getNebulosa());
        //}

        nombre.text = nebulosa.nombre;


    }


    public void irANebulosa()
    {
        NebulosaSingleton ns = GameObject.FindGameObjectWithTag("Nebulosa").GetComponent<NebulosaSingleton>();
        ns.nebulosa = nebulosa;
        StartCoroutine(animacionIrANebulosa(new Vector3(nebulosa.x, nebulosa.y, nebulosa.z)));

    }
    IEnumerator animacionIrANebulosa(Vector3 pos)
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("CameraAnimation");
        canvas.GetComponentInChildren<Canvas>().enabled = true;
        Animator animator = canvas.GetComponent<Animator>();
        animator.SetTrigger("Exit");
        Transform trCamera = Camera.main.GetComponent<Transform>();


        while ((pos - trCamera.position).magnitude > 10)
        {
            trCamera.position = Vector3.Lerp(trCamera.position, pos, 3f * Time.deltaTime);
            yield return new WaitForSeconds(0.016f);
        }
        SceneManager.LoadScene(escena, LoadSceneMode.Single);
        // SceneManager.LoadSceneAsync("EditorNebulosa", LoadSceneMode.Additive);
    }


    IEnumerator getNebulosa()
    {
        string accion = "Api/nebulosas/" + nebulosa.id;
        UnityWebRequest wr = UnityWebRequest.Get(ApiCalls.url + accion);

        yield return wr.SendWebRequest();

        if (wr.isNetworkError || wr.isHttpError)
        {
            Debug.Log("ERROR: " + wr.error);
        }
        else
        {
            string json = wr.downloadHandler.text;
            Nebulosa nebulosa = JsonUtility.FromJson<Nebulosa>(json);
            TextMeshProUGUI nombre = infoNebulosa.transform.Find("Nombre").GetComponent<TextMeshProUGUI>();
            nombre.text = nebulosa.nombre;
        }
    }
}
