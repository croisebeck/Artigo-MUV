using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cameraSwitch : MonoBehaviour {

   public Camera[] cameras;
   public Camera returnCamera;
   public List<GameObject> renders = new List<GameObject>();
   public Text nomeaviao;
    public int i = 0;
    public GameObject game;
    public Slider slide;
    public Text textoc;
    GameObject alvo;
    public Text tempoAviao;
    
    private void Update()
    {
        //// click
        if(returnCamera==null)
        {
            returnCamera = cameras[0];
        }
        Camera cam = returnCamera;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray raio = cam.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(raio, out hit))
            {
                if(hit.collider.tag=="Player")
                {
                    nomeaviao.enabled = true;
                    nomeaviao.text = hit.collider.name;
                    game = hit.collider.gameObject;
                    slide.enabled = true;
                    slide.gameObject.SetActive(true);
                    textoc.text = hit.collider.GetComponent<aviao>().tempoDeEspera.ToString();
                    alvo = hit.collider.gameObject;
                    tempoAviao.gameObject.SetActive(false);
                }
            }
        }
        if(alvo!=null)
        {
            textoc.text = alvo.GetComponent<aviao>().tempoDeEspera.ToString();
        }

      

        ///
    }

    private void Start()
    {
        renders.AddRange(GameObject.FindGameObjectsWithTag("triggers"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("estacionamento"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("waitpoints"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("aremete"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("interact"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("lineFinal"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("conflito"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("destruidor"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("spawing"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("delimitador"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("controler"));
        renders.AddRange(GameObject.FindGameObjectsWithTag("placa inicial"));
    }
        public void Click()
    {
        if(i==0)
        {
            cameras[0].enabled = true;
            cameras[1].enabled = false;
            cameras[2].enabled = false;
            returnCamera = cameras[0];

        }
        if(i==1)
        {
            cameras[0].enabled = false;
            cameras[1].enabled = true;
            cameras[2].enabled = false;
            returnCamera = cameras[1];

        }
        if(i==2)
        {
            cameras[0].enabled = false;
            cameras[1].enabled = false;
            cameras[2].enabled = true;
            returnCamera = cameras[2];

        }
      

        
        
        
        i++;
        if(i > cameras.Length-1)
        {
            i = 0;
        }
        Debug.Log("clickando");

        

    }

    public void rotateCamDir(int i)
    {
        if(returnCamera==null)
        {
            returnCamera = cameras[0];
        }
        if(i==0)
        {
            
            returnCamera.gameObject.transform.Rotate(new Vector3(0,-30,0));
        }
        if(i==1)
        {
            returnCamera.gameObject.transform.Rotate(new Vector3(0,30,0));
        }
        
        
    }

    public void MoveZoom(float position)
    {
        
        
        if (returnCamera == null)
        {
            returnCamera = cameras[0];
        }
        

      returnCamera.gameObject.transform.Translate(new Vector3(0, 0,position));
        
       


    }
    public bool hideEl = true;
    public void shownRender()
    {

        if (hideEl)
        {
            foreach (GameObject interacts in renders)
            {
                interacts.GetComponent<MeshRenderer>().enabled = false;
                hideEl = false;
            }
        }
        else
        {
            foreach (GameObject interacts in renders)
            {
                interacts.GetComponent<MeshRenderer>().enabled = true;
                hideEl = true;
            }
        }
        
        
    }
    public void MidifyTempoEstacionar(float tempo)
    {
        if (game != null)
        {
            game.GetComponent<aviao>().tempoDeEspera = tempo;
        }
    }
  

   
 
}
