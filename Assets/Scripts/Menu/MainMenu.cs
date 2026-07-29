using System;
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

	public bool agarroObjeto = false;
	public bool ultimoTutorial = false;
	public bool terminarTutorial = false;
	public bool controlarMetodo1 = false;
	public bool controlarMetodo2 = false;

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

	public IEnumerator TutorialUnoAyuda() 
	{
		yield return new WaitForSeconds(0f);
		Debug.Log("Inicio TutorialUnoAyuda");
		yield return new WaitForSeconds(30f);

		if(agarroObjeto == false) 
		{
			tutorial1.SetActive(true);
			agarroObjeto = true;
			StartCoroutine(TutorialDos());
		}
		yield break;
	}

	public IEnumerator TutorialDos() 
	{
		yield return new WaitForSeconds(0f);
		Debug.Log("TutorialDos()");
		if (agarroObjeto == true && ultimoTutorial == false)
		{
			yield return new WaitForSeconds(30f);

			tutorial1.SetActive(false);
			tutorial2.SetActive(true);
			ultimoTutorial = true;
		}
		yield break;
	}

	public void TerminarTutorial()
	{
		if (agarroObjeto == true && ultimoTutorial == true && terminarTutorial == false)
		{
			tutorial1.SetActive(false);
			tutorial2.SetActive(false);
			StopCoroutine(TutorialDos());
			terminarTutorial = true;
			Debug.Log("tutorial terminado");
		}
	}
}