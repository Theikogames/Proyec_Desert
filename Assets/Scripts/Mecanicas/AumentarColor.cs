using Unity.VisualScripting;
using UnityEngine;

public class AumentarColor : MonoBehaviour
{
    public MainMenu interaccion;
    public Monocromatico _color;
    public bool reclicar = false;
    public float gradienteColor = 0f;
    public float objetoGradienteColor = 0f;
	public float velocidadPorSegundo = 5f;

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Polera")) 
        {
            reclicar = true;
            Destroy(other.gameObject);
            interaccion.ultimoTutorial = true;
            if (interaccion.agarroObjeto == true && interaccion.ultimoTutorial == true && interaccion.terminarTutorial == false) 
            { 
                interaccion.tutorial1.SetActive(false);
                interaccion.tutorial2.SetActive(false);
                interaccion.StopCoroutine(interaccion.TutorialDos());
                Debug.Log("tutorial terminado");
            }
        }
    }

	private void Update()
	{
		if (reclicar) 
        {
            objetoGradienteColor += 10f;
            reclicar = false;
        }
        gradienteColor = Mathf.MoveTowards(gradienteColor, objetoGradienteColor, velocidadPorSegundo * Time.deltaTime);
        _color._RadioColor = gradienteColor;
	}
}
