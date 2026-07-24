using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;

public class MenuSpawn : MonoBehaviour
{
	
	public GameObject aviso, controles, pausa, ui;

	private void Start()
	{
		ui.SetActive(true);
		aviso.SetActive(true);
		controles.SetActive(false);
		pausa.SetActive(false);
	}

	private void Update()
	{
		MenuControles();
		MenuPausa();
	}

	public void MenuControles() 
	{
		if (Input.GetKeyDown(KeyCode.C) && aviso.activeSelf == true && pausa.activeSelf == false) 
		{
			Debug.Log("Activado");
			aviso.SetActive(false);
			controles.SetActive(true);
		}
		else if (Input.GetKeyDown(KeyCode.C) && aviso.activeSelf == false && pausa.activeSelf == false) 
		{
			Debug.Log("Desactivado");
			aviso.SetActive(true);
			controles.SetActive(false);
		}
	}

	public void MenuPausa() 
	{
		if (Input.GetKeyDown(KeyCode.P) && pausa.activeSelf == false) 
		{
			Debug.Log("Activado");
			pausa.SetActive(true);
			ui.SetActive(false);
		}
		else if (Input.GetKeyDown(KeyCode.P) && pausa.activeSelf == true)
		{
			Debug.Log("Desactivado");
			pausa.SetActive(false);
			ui.SetActive(true);
			aviso.SetActive(true);
			controles.SetActive(false);
		}
	}
}
