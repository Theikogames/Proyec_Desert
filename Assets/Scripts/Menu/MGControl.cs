using UnityEngine;
using static UnityEditor.ShaderData;

public class MGControl : MonoBehaviour
{
	public MainMenu interaccion;
	public MainMenu msc;

	private void Start()
	{
		Time.timeScale = 1;
		msc.ui.SetActive(true);
		msc.aviso.SetActive(true);
		msc.controles.SetActive(false);
		msc.pausa.SetActive(false);
		msc.StartCoroutine(msc.TutorialUnoAyuda());
	}

	private void Update()
	{
		msc._MenuControles();
		msc._MenuPausa();

		if (interaccion.agarroObjeto == true && interaccion.controlarMetodo2 == false)
		{
			interaccion.StopCoroutine(interaccion.TutorialUnoAyuda());
			interaccion.StartCoroutine(interaccion.TutorialDos());
			interaccion.controlarMetodo2 = true;
		}

		if (interaccion.agarroObjeto == true && interaccion.ultimoTutorial == true && interaccion.terminarTutorial == false && interaccion.controlarMetodo1 == false)
		{
			interaccion.TerminarTutorial();
			interaccion.controlarMetodo1 = true;
		}
	}

}
