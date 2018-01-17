using UnityEngine;


/// <summary>
/// 碰撞检测
/// </summary>
public class Shit : MonoBehaviour
{
	/// <summary>
	/// 检测碰撞
	/// </summary>
	/// <param name="col"></param>
	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Shit") //检测到碰撞
		{
			GameObject.Find("ScriptMount").GetComponent<GameManager>().GameOver(); //调用游戏结束方法
		}
	}
}