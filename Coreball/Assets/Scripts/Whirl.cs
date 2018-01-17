using UnityEngine;


/// <summary>
/// 球体旋转
/// </summary>
public class Whirl : MonoBehaviour
{
	public float Speed = 90; //转速

	void Update()
	{
		transform.Rotate(new Vector3(0, 0, -Speed * Time.deltaTime)); //控制自身旋转
	}
}