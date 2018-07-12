using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delimitadorAeroporto : MonoBehaviour
{

    // Use this for initialization
    public List<GameObject> waitEsta = new List<GameObject>();
    public GameObject destino;
    public GameObject sorteio;
    public int tamSizeVector = 0;
    public List<GameObject> ordemDeChegada = new List<GameObject>();
    public GameObject[] avioes;
    public bool estaTragico = false;
    



    void Start()
    {
        //waitEsta = GameObject.FindGameObjectsWithTag ("estacionamento");
        waitEsta.AddRange(GameObject.FindGameObjectsWithTag("estacionamento"));
        tamSizeVector = waitEsta.Count;
    }

    // Update is called once per frame

    void Update()
    {
        avioes = GameObject.FindGameObjectsWithTag("Player");
        if (estaTragico)
        {
            foreach (GameObject interator in avioes)
            {
                interator.SendMessage("taCritico");
            }
        }

        


    }
    GameObject sorteioPos()
    {
        GameObject sorteioInterno;

        int numero = Random.Range(0, waitEsta.Count);
        sorteioInterno = waitEsta[numero].gameObject;
        waitEsta.RemoveAt(numero);
        tamSizeVector--;


        return sorteioInterno;
    }


    void OnTriggerEnter(Collider cool)
    {


        if (cool.gameObject.tag == "Player")
        {
            destino = cool.gameObject;
            if (cool.GetComponent<aviao>().jaPassou == false)
            {
                cool.GetComponent<aviao>().jaPassou = true;
                if (waitEsta.Count > 0)
                {
                    if (ordemDeChegada.Count < 1)
                    {
                        sorteio = sorteioPos();
                        
                        cool.SendMessage("sorteio", sorteio);
                    }
                    else
                    {
                        ordemDeChegada.Add(cool.gameObject);
                        cool.SendMessage("vaiProCritico");

                    }
                   
                    
                }
                else
                {
                    ordemDeChegada.Add(cool.gameObject);
                    cool.SendMessage("vaiProCritico");

                }
                if (ordemDeChegada.Count>=5)
                {
                    foreach(GameObject interacts in avioes)
                    {
                        interacts.SendMessage("taCritico");
                    }
                }
              
            }
            

            else if(cool.GetComponent<aviao>().jaPassou == true)
            {
                if(!waitEsta.Contains(cool.GetComponent<aviao>().sorteado))
                {
                    if(cool.GetComponent<aviao>().sorteado !=null)
                    {
                        waitEsta.Add(cool.GetComponent<aviao>().sorteado);
                        ordemDeChegada[0].SendMessage("sorteio", sorteioPos());
                        
                    }
                }
                

                //ordemDeChegada[0].SendMessage("sorteio", sorteioPos());
                if(ordemDeChegada[0].GetComponent<aviao>().podeRemover==true)
                {
                    ordemDeChegada.RemoveAt(0);
                }
            }
        }

              
    }
   
}
