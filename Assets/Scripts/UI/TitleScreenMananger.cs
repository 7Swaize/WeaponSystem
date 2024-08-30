using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreenMananger : MonoBehaviour
{
	[Header("Refernces")]
	[SerializeField] private TMP_Text Slide1Text; 
	[SerializeField] private TMP_Text Slide2Text;
	[SerializeField] private TMP_Text Slide3Text;
	[SerializeField] private TMP_Text Slide4Text;
	
	private int currentSlide = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return) && currentSlide == 0)
		{
			Slide1Text.gameObject.SetActive(false);
			Slide2Text.gameObject.SetActive(true);
			currentSlide++;
		}

		else if (Input.GetKeyDown(KeyCode.Return) && currentSlide == 1)
		{
			Slide2Text.gameObject.SetActive(false);
			Slide3Text.gameObject.SetActive(true);
			currentSlide++;
		}

		else if (Input.GetKeyDown(KeyCode.Return) && currentSlide == 2)
		{
			Slide3Text.gameObject.SetActive(false);
			Slide4Text.gameObject.SetActive(true);
			currentSlide++;
		}

		else if (Input.GetKeyDown(KeyCode.Return) && currentSlide == 3)
		{
			Slide4Text.gameObject.SetActive(false);
			SceneManager.LoadScene(1);
			currentSlide++;
		}
	}
}