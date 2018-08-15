using UnityEngine;
using System.Collections;

public enum MoleState
{
	None,
	Open,
	Idle,
	Close,
	Catch
}
public class Hole : MonoBehaviour {

	public MoleState MS;
	
	public Texture[] Open_Images;
	public Texture[] Idle_Images;
	public Texture[] Close_Images;
	public Texture[] Catch_Images;
	
	public bool GoodMole;
	public int PerGood = 15;
	
	public Texture[] Open_Images_2;
	public Texture[] Idle_Images_2;
	public Texture[] Close_Images_2;
	public Texture[] Catch_Images_2;
	

	public AudioClip Open_Sound;
	public AudioClip Catch_Sound;
	
	public GameManger GM;
	
	public float Ani_speed;
	public float _now_ani_time;
	
	int Ani_count;
	int Length = 0;
	public void Open_On()
	{
		MS = MoleState.Open;
		Ani_count = 0;
		audio.clip = Open_Sound;
		audio.Play();
	
		int a = Random.Range (0, 100);
		if(a>=PerGood){
									GoodMole=false;
		}else{
									
									GoodMole=true;
		}
		if(GM.GS==GameState.Ready)
		GM.GO();
	}
	
	public void Open_ing()
	{
		Debug.Log("Open_ing = " + Ani_count);
		if(GoodMole == false){
		renderer.material.mainTexture = Open_Images[Ani_count];
		}else{
		renderer.material.mainTexture = Open_Images_2[Ani_count];
		}
		Ani_count += 1;
		
		if(GoodMole == false){
		   Length = Open_Images.Length;
		}else{
		   Length = Open_Images_2.Length;
		}
		if(Ani_count >= Length){
			Debug.Log("Ani_count = " + Ani_count + "Open_Images.Length = " + Open_Images.Length);
			Idle_On();
		}
	}		
	
	public void Idle_On()
	{
		MS = MoleState.Idle;
		Ani_count = 0;
	}
	
	public void Idle_ing()
	{
		Debug.Log("Idle_ing = " + Ani_count + "Idle_Images2.Length = " + Idle_Images_2.Length);
		Debug.Log("Idle_ing = " + Ani_count + "Idle_Images.Length = " + Idle_Images.Length);
		if(GoodMole == false){
			renderer.material.mainTexture = Idle_Images[Ani_count];
		}else{
			renderer.material.mainTexture = Idle_Images_2[Ani_count];
		}
		Ani_count += 1;
		if(Ani_count >= (Idle_Images.Length-2)){
			Debug.Log("Ani_count = " + Ani_count + "Idle_Images.Length = " + Idle_Images.Length);
			Close_On();
		}	
		
	}
	public void Close_On()
	{
		MS = MoleState.Close;
		Ani_count = 0;
	}
	
	public void Close_ing()
	{
		Debug.Log("Close_ing = " + Ani_count);
			
		if(GoodMole == false){
		renderer.material.mainTexture = Close_Images[Ani_count];
		}else{
		renderer.material.mainTexture = Close_Images_2[Ani_count];
		}
		Ani_count += 1;
		if(Ani_count >= Close_Images.Length){
			Debug.Log("Ani_count = " + Ani_count + "Close_Images.Length = " + Close_Images.Length);
			StartCoroutine("Wait");
		}
	
	}
	
	public void Catch_On()
	{
		MS = MoleState.Catch;
		Ani_count = 0;
	
		audio.clip = Catch_Sound;
		audio.Play();
	
		if(GoodMole == false){
		GM.Count_Bad+=1;
		}else{
		GM.Count_Good+=1;
		}
	}	
	public void Catch_ing()
	{
		Debug.Log("Catch_ing = " + Ani_count);
		if(GoodMole == false){
		renderer.material.mainTexture = Catch_Images[Ani_count];
		}else{
		renderer.material.mainTexture = Catch_Images_2[Ani_count];
		}
		Ani_count += 1;
		if(Ani_count >= Catch_Images.Length){
			Debug.Log("Ani_count = " + Ani_count + "Catch_Images.Length = " + Catch_Images.Length);
			StartCoroutine("Wait");
		}

	}			
	
	public IEnumerator Wait(){
		MS = MoleState.None;
		Ani_count = 0;
		Debug.Log("Wait  = Ani_count" + Ani_count);
		float wait_Time = Random.Range(0.5f, 4.5f);
		yield return new WaitForSeconds(wait_Time);
		Open_On();
	}
	
	public void OnMouseDown(){
		if(MS == MoleState.Idle|| MS == MoleState.Open){
			Catch_On();
		}
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_now_ani_time>=Ani_speed){
		
			if(MS == MoleState.Open){
				Open_ing();
			}
			if(MS == MoleState.Idle){
				Idle_ing();
			}
	
			if(MS == MoleState.Close){
				Close_ing();
			}
	
			if(MS == MoleState.Catch){
				Catch_ing();
			}
			_now_ani_time=0;
		}else{
			_now_ani_time+=Time.deltaTime;
		}
	}
}