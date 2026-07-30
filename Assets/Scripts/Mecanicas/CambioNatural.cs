using Unity.VisualScripting;
using UnityEngine;

public class CambioNatural : MonoBehaviour
{
	public MainMenu interaccion;

    public Transform centroCirculo;

	[Header("Referencia al script Monochrome")]
    public Monocromatico ColorRadius;
    [Header("Valor del gradiente de color")]
    public float Gradientecolor = 0f; // valor actual del gradiente de color
    public float targetGradientecolor = 0f; // objetivo de colorRadius
    public float velocidadPorSegundo = 1f; // unidades por segundo para alcanzar el objetivo

    [Header("Generacion de plantas al reciclar")]
    public GameObject Planta;
    public GameObject PlantaEdificio;
    public int polerasParaGenerar = 2;
    public int plantasAlAzar = 3;
    public LayerMask capaTerreno;

    [Header("borde De Edificios")]
    public Transform[] bordesDeEdificios;
    public float radioSaturacionBorde = 0.5f;
    private int contadorPoleras = 0;
    public bool[] bordeConPlanta;
    void Start()
    {
        if (bordesDeEdificios != null)
        {
            bordeConPlanta = new bool[bordesDeEdificios.Length];
        }
        if(centroCirculo == null)
        {
            centroCirculo = transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Polera"))
        {
            targetGradientecolor += 2f;
            //reciclar = true; // Activamos la variable reciclar al colisionar con un objeto con el tag "Polera"
            contadorPoleras++;
            Destroy(other.gameObject);
            if(interaccion.ultimoTutorial == false) 
            {
				interaccion.ultimoTutorial = true;
			}
			if (contadorPoleras >= polerasParaGenerar)
            {
                InstanciarPlantas();
                contadorPoleras = 0; // Reiniciamos el contador para la próxima ronda
            }

		}
    }

    void Update()
    {
        // Suavizamos la transición del valor actual hacia el objetivo, ponemos el valor es de inicio, el objetivo y el valor de velocidad por segundo multiplicado por el tiempo entre frames
        // Mathf.MoveTowards es una función que nos permite mover un valor hacia otro de manera suave y controlada
        // el tercer valor es el que controla la velocidad de cambio, y se multiplica por Time.deltaTime para que sea independiente de la tasa de frames

        Gradientecolor = Mathf.MoveTowards(Gradientecolor, targetGradientecolor, velocidadPorSegundo * Time.deltaTime);
        ColorRadius._RadioColor = Gradientecolor; // asignamos el valor suavizado al valor que se le pasa al shader para extender el radio del color del objeto

	}

    void InstanciarPlantas()
    {
        Vector3 centro = (centroCirculo != null) ? centroCirculo.position : transform.position;
        for(int i = 0; i < plantasAlAzar; i++)
        {
            Vector2 Aleatorio = Random.insideUnitCircle * targetGradientecolor;

            Vector3 posicionPlanta = centroCirculo.position + new Vector3(Aleatorio.x,0, Aleatorio.y);

            GenerarPlantaEnTerreno(posicionPlanta);
        }
        if (bordesDeEdificios != null)
        {
            for (int i = 0; i < bordesDeEdificios.Length; i++)
            {
                if(bordesDeEdificios[i] == null)continue;
                // Si el borde aún no tiene planta
                if (!bordeConPlanta[i])
                {
                    Vector3 posOrigen2D = new Vector3(centroCirculo.position.x,0, centroCirculo.position.z);
                    Vector3 posBorde2D = new Vector3(bordesDeEdificios[i].position.x,0, bordesDeEdificios[i].position.z);
                    // Calculamos la distancia desde el origen del color hasta el borde del edificio
                    float distanciaAlBorde = Vector3.Distance(posOrigen2D,posBorde2D);

                    // Si el objetivo del radio de color ya alcanzó o pasó esa distancia
                    if (targetGradientecolor >= distanciaAlBorde)
                    {
                        // Leve variación para ajustar la posición en el borde
                        GenerarPlantaEnEdificio(bordesDeEdificios[i]);
                        bordeConPlanta[i] = true; // Marcamos como listo para no volver a instanciar en ese mismo edificio
                    }
                }
            }
        }

    }

    void GenerarPlantaEnTerreno(Vector3 puntoXZ)
    {
        Vector3 rayOrigen = new Vector3(puntoXZ.x, centroCirculo.position.y + 100f,puntoXZ.z);

        if(Physics.Raycast(rayOrigen,Vector3.down, out RaycastHit hit , 200f, capaTerreno))
        {
            Quaternion rotacionAlineada = Quaternion.FromToRotation(Vector3.up,hit.normal);
            rotacionAlineada *= Quaternion.Euler(0,Random.Range(0f,360f),0);
            Instantiate(Planta,hit.point,rotacionAlineada);
        }
    }
    void GenerarPlantaEnEdificio(Transform puntoBorde)
    {
        Vector2 desvio = Random.insideUnitCircle * radioSaturacionBorde;
        Vector3 posicionFinal = puntoBorde.position + new Vector3(desvio.x,0,desvio.y);

        Instantiate(PlantaEdificio,posicionFinal,puntoBorde.rotation);
    }
}
