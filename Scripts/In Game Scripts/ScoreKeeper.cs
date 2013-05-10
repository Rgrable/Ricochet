using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour { // keeps track of player score along with the game over splash screens
	
	// VS mode victory conditions
	public static int player_Death;
	public static int enemy_Death;
	protected int VS_Score;
	private bool VS_GameOver;
	private bool VS_PlayerWin;
	//
	
	// Target practice victory conditions
	protected int target_Destroy;
	protected int target_Score;
	protected int target_Ammo;
	private bool target_GameOver;
	//
	
	// Misc
	public static int OverallScore; // Contains the players Highscore
	private float OverallScore_F; // used to show the score update slowly
	private GUIStyle gameOver_Text = new GUIStyle();
	private float finishTime = 5.0f; // How long the gameover / winner screen is open
	
	//
	
	void Start()
	{ 
		gameOver_Text.font = (Font)Resources.Load("BAUS");
		gameOver_Text.fontSize = 150;
		
	}
	void Update () {
		
		Debug.Log(enemy_Death);
		if (StartMenu.VS_MODE)
		{
			victoryCon_VS();
		}
	}
	void OnGUI()
	{
		if (StartMenu.VS_MODE)
			CheckVS();
	}
	// VS MODE CALLS/////////////////////////////////////////////////////
	#region VS MODE
	protected virtual void victoryCon_VS()
	{
		if (player_Death >= 4)
		{
			gameOver_Text.normal.textColor = new Color(256,0,0);
			VS_GameOver = true;
		}
		if (enemy_Death >= 4)
		{
			VS_PlayerWin = true;
			gameOver_Text.normal.textColor = new Color(0,256,0);
		}
	}
	private void CheckVS()
	{
		if (VS_GameOver)
		{
			GUI.Label(new Rect(250,300,800,150),"GAMEOVER",gameOver_Text);
			StartCoroutine(KeepPlayerScore());
		}
		if (VS_PlayerWin)
		{
			GUI.Label(new Rect(350,300,800,150),"WINNER",gameOver_Text);
			StartCoroutine(KeepPlayerScore());
		}
	}
	#endregion
	// //////////////////////////////////////////////////////////////////////
	private IEnumerator KeepPlayerScore()
	{
		GUI.Label(new Rect(300,500,800,150),"SCORE: +" + OverallScore_F.ToString("F0"),gameOver_Text);
		OverallScore += enemy_Death;
		yield return new WaitForSeconds(0);
		if (OverallScore_F < enemy_Death)
		{
			OverallScore_F += 0.1f;
		}
		else
			ReturntoStart();
	}
	private void ReturntoStart()
	{
		if (finishTime <= 0)
		{
			Application.LoadLevel("StartMenu");
			ResetGame();
		}
		else
			finishTime -= Time.deltaTime;
	}
	private void ResetGame()
	{
		OverallScore_F = 0;
		player_Death = 0;
		enemy_Death = 0;
		VS_GameOver = false;
		VS_PlayerWin = false;
	}
}
