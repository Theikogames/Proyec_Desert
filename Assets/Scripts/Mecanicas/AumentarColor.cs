using Unity.VisualScripting;
using UnityEngine;

public class AumentarColor : MonoBehaviour
{
    public Monocromatico _color;
    public bool reclicar = false;
    public float gradienteColor = 0f;
    public float objetoGradienteColor = 0f;
	public float velocidadPorSegundo = 1f;

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Polera")) 
        {
            reclicar = true;
            Destroy(other.gameObject);
        }
    }

	private void Update()
	{
		if (reclicar) 
        {
            objetoGradienteColor += 3f;
            reclicar = false;
        }
        gradienteColor = Mathf.MoveTowards(gradienteColor, objetoGradienteColor, velocidadPorSegundo * Time.deltaTime);
        _color._RadioColor = gradienteColor;
	}
}
