using UnityEngine;
using System.Collections;

public enum GameState
{
	Ready,
	Play,
	End
}

public class GameManger : MonoBehaviour {
	
	public GameState GS;

	public Hole[] Holes;
	public float LimitTime;
	public GUIText TimeText;
	public int Count_Bad;
	public int Count_Good;

	public GameObject FinishGUI;
	public GUIText  Final_Count_Bad;
	public GUIText  Final_Count_Good;
	public GUIText  Final_Score;
	
	public AudioClip ReadySound;
	public AudioClip GoSound;
	public AudioClip FinishSound;



	
	
	// Use this for initialization
	void Start () {
		audio.clip = ReadySound;
		audio.Play();
	}
public void GO(){
	GS=GameState.Play;
	audio.clip = GoSound;
	audio.Play();
}
	// Update is called once per frame
	void Update () {
		if (GS == GameState.Play){
			LimitTime-= Time.deltaTime;
			if(LimitTime <= 0){
				LimitTime = 0;
				End ();
			}
		}
		TimeText.text = string.Format("{0:N2}", LimitTime);
	}
	void End()
	{
		GS=GameState.End;
		Final_Count_Bad.text = string.Format("{0}",Count_Bad);
		Final_Count_Good.text = string.Format("{0}",Count_Good);
		Final_Score.text = string.Format("{0}",Count_Bad * 100 - Count_Good * 1000);
		FinishGUI.gameObject.SetActive(true);
		audio.clip = FinishSound;
		audio.Play();
	}
}
