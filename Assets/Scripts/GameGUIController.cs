using UnityEngine;
using System.Collections;

public class GameGUIController : MonoBehaviour {


	public int currentLifes;
	public int maxLifes;
	public GameObject lifeon;
	public GameObject lifeoff;
	GameObject[] lifeson;
	GameObject[] lifesoff;

	public bool useCharacterHealthSettings;
	public GameObject character;



	// Use this for initialization
	void Start () {

		if (useCharacterHealthSettings) {
			maxLifes = character.GetComponent<CharacterController2d>().maxhp;
			currentLifes = character.GetComponent<CharacterController2d>().hp;
		}

		lifeson = new GameObject[maxLifes];
		lifesoff = new GameObject[maxLifes];

		for (int i=0; i<maxLifes; i++) {
			lifeson[i] = (GameObject)Instantiate (lifeon);
			lifeson[i].GetComponent<RectTransform> ().SetParent (GetComponent<RectTransform>());
			lifeson[i].GetComponent<RectTransform> ().anchoredPosition = new Vector3 (110+i*110, -80, 0);
			lifeson[i].GetComponent<RectTransform> ().localScale = Vector3.one;

			lifesoff[i] = (GameObject)Instantiate (lifeoff);
			lifesoff[i].GetComponent<RectTransform> ().SetParent (GetComponent<RectTransform>());
			lifesoff[i].GetComponent<RectTransform> ().anchoredPosition = new Vector3 (110+i*110, -80, 0);
			lifesoff[i].GetComponent<RectTransform> ().localScale = Vector3.one;

		}

	}
	
	// Update is called once per frame
	void Update () {

		currentLifes = character.GetComponent<CharacterController2d>().hp;

		for (int i=0; i<maxLifes; i++) {
			if (i>currentLifes-1) {
				lifeson[i].SetActive(false);
				lifesoff[i].SetActive(true);
			} else {
				lifeson[i].SetActive(true);
				lifesoff[i].SetActive(false);
			}
		}

	}
}
