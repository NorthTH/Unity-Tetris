using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour {

	public void PlayAgian()
	{
		SceneManager.LoadScene ("Level");
	}
}
