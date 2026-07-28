using System.Collections;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject aviso, controles, pausa, ui, tutorial1, tutorial2;

	public GameObject opciones, menuPrincipal;

	public bool interaccion = false;

	public void _MenuControles() 
	{
		if (Input.GetKeyDown(KeyCode.C) && aviso.activeSelf == true && pausa.activeSelf == false) 
		{
			aviso.SetActive(false);
			controles.SetActive(true);
		}
		else if (Input.GetKeyDown(KeyCode.C) && aviso.activeSelf == false && pausa.activeSelf == false) 
		{
			aviso.SetActive(true);
			controles.SetActive(false);
		}
	}

	public void _MenuPausa() 
	{
		if (Input.GetKeyDown(KeyCode.P) && pausa.activeSelf == false) 
		{
			pausa.SetActive(true);
			ui.SetActive(false);
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;
		}
		else if (Input.GetKeyDown(KeyCode.P) && pausa.activeSelf == true)
		{
			pausa.SetActive(false);
			ui.SetActive(true);
			aviso.SetActive(true);
			controles.SetActive(false);
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void _VolverAlJuego()
	{
		pausa.SetActive(false);
		ui.SetActive(true);
		aviso.SetActive(true);
		controles.SetActive(false);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	public void _SalirAlMenu() 
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void _IrAlJuego()
	{
		SceneManager.LoadScene("MainGame");
	}

	public void _SalirDelJuego() 
	{
		Application.Quit();
	}

	public void _Opciones()
	{
		if (opciones.activeSelf == false && menuPrincipal.activeSelf == true)
		{
			opciones.SetActive(true);
			menuPrincipal.SetActive(false);
		}
		else if (opciones.activeSelf == true && menuPrincipal.activeSelf == false)
		{
			opciones.SetActive(false);
			menuPrincipal.SetActive(true);
		}
	}

	public IEnumerator TextoTutorial() 
	{
		yield return new WaitForSeconds(10f);

		if(interaccion == false) 
		{
			tutorial1.SetActive(true);
			interaccion = true;
		}
	}
}
