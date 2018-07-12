using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class aviao : MonoBehaviour
{

	// Use this for initialization
	public GameObject objetoAtual;
	public GameObject sorteado=null;

	GameObject[] objectsdesvio;
	GameObject[] point;
	public GameObject[] pointsToDesvio;
	public GameObject[] objs;
	public GameObject[] listpoint;

	public GameObject[] pontosCriticos;
	public GameObject[] lineFinal;

    public List<GameObject> pointsAremete = new List<GameObject>();

	NavMeshAgent agent;


	public bool plausivel = false;
	public bool mudaCaminho = false;
	public float agentDistance;
	public int pos = 0;
	public int posi = 0;

	public int posCritico =0;
	public int posLineFinal = 0;

    public int aremateqtd = 0;

	public bool estacionamento=false;
	public bool comecaAContagem=false;
	public float tempoDeEspera = 2;
	public bool caminhiCritico = false;
	public bool vaiParaOCaminho = false;
    public bool sobreVoa = false;

    public bool jaPassou = false;
    public bool podeRemover = false;
    public bool para = false;

    



	void Start ()
	{
		//linha de baixo certeza otima
		agent = GetComponent<NavMeshAgent> ();
		point = GameObject.FindGameObjectsWithTag ("waitpoints");
		objectsdesvio = GameObject.FindGameObjectsWithTag ("interact");

		listpoint = reorganiza (point);
		pointsToDesvio = reorganiza (objectsdesvio);


		lineFinal = reorganiza(GameObject.FindGameObjectsWithTag("lineFinal"));

		pontosCriticos = reorganiza (GameObject.FindGameObjectsWithTag ("conflito"));

        pointsAremete.AddRange(reorganiza(GameObject.FindGameObjectsWithTag("aremete")));
        

	}
	
	// Update is called once per frame
	void Update ()
	{
      
        objs = GameObject.FindGameObjectsWithTag ("Player");
			agentDistance = agent.remainingDistance;

        if (sobreVoa == true)
        {
            objetoAtual = pointsAremete[aremateqtd];

            agent.SetDestination(pointsAremete[aremateqtd].transform.position);
            agent.speed = 15;
            //for loop and cut

            if (!agent.pathPending && agent.remainingDistance <= 4)
            {
                if (aremateqtd < pointsAremete.Count - 1)
                {
                    aremateqtd++;
                }

                if (aremateqtd >= pointsAremete.Count - 1)
                {
                    aremateqtd = 4;
                }

            }
        }
        else{

            if (vaiParaOCaminho == true)
            {
                objetoAtual = lineFinal[posLineFinal];

                agent.SetDestination(lineFinal[posLineFinal].transform.position);


                if (!agent.pathPending && agent.remainingDistance <= 4)
                {
                    if (posLineFinal < lineFinal.Length - 1)
                    {
                        posLineFinal++;
                    }

                }
            }
            else
            {

                if (caminhiCritico == true)
                {
                    
                    objetoAtual = pontosCriticos[posCritico];

                    if (para == true)
                    {
                        agent.SetDestination(pontosCriticos[posCritico].transform.position);
                    }

                    if (!agent.pathPending && agent.remainingDistance <= 4)
                    {
                        if (posCritico < pontosCriticos.Length - 1)
                        {
                            posCritico++;
                        }
                        if (posCritico >= pontosCriticos.Length - 1)
                        {
                            para = true;
                        }

                    }

                }
                else
                {
                    if (estacionamento == true)
                    {
                        agent.SetDestination(sorteado.transform.position);
                        tempoDeEspera -= Time.deltaTime;
                        if (tempoDeEspera <= 0)
                        {
                            estacionamento = false;
                            //tempoDeEspera = 2;
                            vaiParaOCaminho = true;
                        }
                    }
                    else
                    {
                        if (mudaCaminho == true)
                        {


                            agent.ResetPath();

                            objetoAtual = pointsToDesvio[posi];

                            agent.SetDestination(pointsToDesvio[posi].transform.position);


                            if (!agent.pathPending && agent.remainingDistance <= 4)
                            {
                                if (posi < pointsToDesvio.Length - 1)
                                {
                                    posi++;
                                }

                            }


                        }
                        else
                        {
                            objetoAtual = listpoint[pos];

                            agent.destination = listpoint[pos].transform.position;

                            if (!agent.pathPending && agent.remainingDistance <= 0)
                            {
                                if (pos < listpoint.Length - 1)
                                {
                                    pos++;
                                }

                            }


                        }
                    }
                }
            }


        }

    }

   


	void OnTriggerEnter (Collider cool)
	{

        //if (cool.gameObject.tag == "desvio") {
        if (cool.gameObject.name == "semaforoDesvio") { 
			if (plausivel == true) {
				
				mudaCaminho = true;
			}
			

		}

		
		if (cool.gameObject.name == "semaforo") {
			foreach (GameObject interator in objs) {
				interator.GetComponent<aviao> ().plausivel = true;
			}
		}

		if (cool.gameObject.tag == "estacionamento")
		{
			comecaAContagem = true;
            podeRemover = true;
            //GameObject.FindGameObjectWithTag("delimitador").GetComponent<delimitadorAeroporto>().ordemDeChegada.RemoveAt(0);
            
		}
		if (cool.gameObject.name == "destruidor")
		{
			if (this.gameObject != null) {
				Destroy (this.gameObject);
			}
		}
		if (cool.gameObject.name=="delimitador") 
		{
			agent.speed = 10;
           
		}
		if (cool.gameObject.name == "waitpoints final") 
		{
			agent.speed = 30;
		}
        if(cool.gameObject.tag=="placa inicial")
        {
            if(aremata)
            {
                sobreVoa = true;
            }
        }
	
	


	}

    public bool aremata = false;
    void taCritico()
    {
        aremata = true;
    }
    void sorteio(GameObject obj)
	{
		estacionamento = true;
		caminhiCritico = false;
		sorteado = obj;
	}
  
	void vaiProCritico()
	{
		
		estacionamento = false;
		caminhiCritico = true;
	}


	void OnTriggerExit (Collider cool)
	{
		if (cool.gameObject.name == "semaforo") {
			foreach (GameObject interator in objs) {
				interator.GetComponent<aviao> ().plausivel = false;
			}
		}

       
    }

	GameObject[] reorganiza (GameObject[] points)
	{
		GameObject[] vector = new GameObject[points.Length];
		int j = 0;
		for (int i = 0; i < points.Length; i++) {
			string nome = points [i].name;

			if (!nome.Contains ("(")) {
				vector [0] = points [i].gameObject;
				j++;
			} else {
				int posl = nome.IndexOf ("(");
                int posv = 0;
                string num = nome[posl + 1].ToString();
                    
                try
                {
                    num.Insert(1, nome[posl + 2].ToString());
                    posv = int.Parse(num);
                  
                }
                catch
                {
                    posv = int.Parse(num);
                    
                }

                //posv = int.Parse(num);
				vector [posv] = points [j].gameObject;
				j++;


			}
		}
					
		return vector;
	}

  



}
