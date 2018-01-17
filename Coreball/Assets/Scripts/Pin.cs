using UnityEngine;


/// <summary>
/// 发射针
/// </summary>
public class Pin : MonoBehaviour
{
	private bool      _isBall     = false; //是否朝着球走
	private bool      _isStartPos = false; //是否朝着就位点走
	private Transform _startPos;           //就位 位置
	private Transform _targetBall;         //球 位置
	private Vector3   _targetOffsetBall;   //与球中心的偏移位置（正确位置）
	public  float     Speed = 5;           //速度5

	void Start()
	{
		_startPos           =  GameObject.Find("StartPos").transform; //找到就位 位置
		_targetBall         =  GameObject.Find("Whirl").transform;    //找到生成位置
		_targetOffsetBall   =  _targetBall.position;                  //把球的中心位置，赋值给球偏移位置
		_targetOffsetBall.y -= 1.66f;                                 //得到球偏移的正点位置，所以需要球的中心点的Y 减去 1.66f
	}


	void Update()
	{
		if (_isBall == false) //朝着球飞 为 false
		{
			if (_isStartPos == false) //朝着就位点飞 为 false
			{
				transform.position =
					Vector3.MoveTowards(transform.position, _startPos.position, Speed * Time.deltaTime); //朝目标移动（当前位置，目标位置，速度）
				if (Vector3.Distance(transform.position, _startPos.position) < 0.05f)                 //如果当前位置 与 目标位置 < 0.05f
				{
					_isStartPos = true; //到达就位点
				}
			}
		}
		else //如果_isBall 为 True的情况，就说明需要让 箭 从就位点，到球
		{
			transform.position =
				Vector3.MoveTowards(transform.position, _targetOffsetBall, Speed * Time.deltaTime); //朝目标移动（当前位置，目标位置，速度）
			if (Vector3.Distance(transform.position, _targetOffsetBall) < 0.05f)                 //如果当前位置 与 目标位置 < 0.05f
			{
				transform.position = _targetOffsetBall; //赋值，定点到位
				transform.SetParent(_targetBall);       //设置父物体
				_isBall = false;                        //到了目标点后，就不需要飞了
			}
		}
	}


	/// <summary>
	/// 朝着球飞
	/// </summary>
	public void DistanceBall()
	{
		_isBall     = true;
		_isStartPos = true;
	}
}