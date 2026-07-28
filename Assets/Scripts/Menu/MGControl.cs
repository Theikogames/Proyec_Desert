using UnityEngine;
using static UnityEditor.ShaderData;

public class MGControl : MonoBehaviour
{
	public MainMenu msc;

	private void Start()
	{
		msc.ui.SetActive(true);
		msc.aviso.SetActive(true);
		msc.controles.SetActive(false);
		msc.pausa.SetActive(false);
	}

	private void Update()
	{
		msc._MenuControles();
		msc._MenuPausa();
	}

}
