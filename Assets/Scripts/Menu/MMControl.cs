using UnityEngine;

public class MMControl : MonoBehaviour
{
	public MainMenu mmc;

	public void Start()
	{
		mmc.menuPrincipal.SetActive(true);
		mmc.opciones.SetActive(false);
	}
}
