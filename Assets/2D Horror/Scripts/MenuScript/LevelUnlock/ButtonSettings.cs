using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


//Here we have Buttons and ChooseCharacter Scripts

public class ButtonSettings : MonoBehaviour {
	public static int releasedLevelStatic = 1;
	public int releasedLevel;
	public string nextLevel;

	public GameObject Character1; //First Object (Player)
	public GameObject Character2; //Second Character to choose
	public GameObject CameraFlying; //Camera for first character
	public GameObject CameraWalking; //camera for second character
	public GameObject ButtonFlying; //Button to choose first character
	public GameObject ButtonWalking; //button to choose second character
	public GameObject ChooseCharacter; //Panel with character select



	void Awake()
	{

		//we have Level 1 active on start
		if (PlayerPrefs.HasKey ("Level")) {
			releasedLevelStatic = PlayerPrefs.GetInt ("Level", releasedLevelStatic);

		}
	}

	public void ButtonNextLevel()
	{
		//When you pass the level, show panel and write name to scene to load
		//and write LEVEL that will be unlocked after seeing that
		SceneManager.LoadScene (nextLevel);
		if(releasedLevelStatic <= releasedLevel){
			releasedLevelStatic = releasedLevel;
			PlayerPrefs.SetInt ("Level", releasedLevelStatic);

	}
   }

	public void ButtonMenu()
	{
		SceneManager.LoadScene ("MenuLevel");
	}

	//Character Select (Simple, but works only that way)
	public void CharacterSelect(){

		//When you will click left mouse on: left Image
		if (Input.GetMouseButtonDown (0) == ButtonFlying) {
			//it will active Character1, Camera of Character 1 and close the Select Character Panel
			Character1.SetActive (true);
			CameraFlying.SetActive (true);
			ChooseCharacter.SetActive (false);
		} else {


			Character2.SetActive (true);
			CameraWalking.SetActive (true);
			ChooseCharacter.SetActive (false);
		}
	}

	public void CharacterSelect2(){


		if (Input.GetMouseButtonDown (0) == ButtonWalking) {
			//it will active Character2, Camera of Character2 and close the Select Character Panel
			Character2.SetActive (true);
			CameraWalking.SetActive (true);
			ChooseCharacter.SetActive (false);
		} else {


			Character1.SetActive (true);
			CameraFlying.SetActive (true);
			ChooseCharacter.SetActive (false);
		}
	}


}