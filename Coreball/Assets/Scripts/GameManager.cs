using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


/// <summary>
/// 总控制脚本
/// </summary>
public class GameManager : MonoBehaviour
{
	private Transform  _startPos;           //就位 位置
	private Transform  _generatePos;        //生成位置
	private GameObject _arrowObj;           //箭
	private Pin        _selectPin;          //选择的针
	private bool       _isOver = false;     //默认游戏未结束
	private int        _score  = 0;         //分数
	public  Text       ScoreText;           //分数文本框
	private Camera     _mainCamera;         //主相机组件对象
	private float      _animationSpeed = 3; //动画播放速度


	/// <summary>
	/// 初始化数据
	/// </summary>
	void Start()
	{
		_startPos    = GameObject.Find("StartPos").transform;    //找到就位 位置
		_generatePos = GameObject.Find("GeneratePos").transform; //找到生成位置
		_mainCamera  = Camera.main;                              //指定主相机
		GenerateArrow();                                         //调用生成
	}


	/// <summary>
	/// 生成箭
	/// </summary>
	public void GenerateArrow()
	{
		_arrowObj      = Resources.Load<GameObject>("Prefabs/Pin"); //动态加载预设物
		GameObject obj =
			Instantiate(_arrowObj, _generatePos.position, _arrowObj.transform.rotation) as GameObject; //实例化预设物（预设物，位置，旋转信息）
		_selectPin = obj.GetComponent<Pin>();                                                       //获取实例化 箭 的脚本Pin
	}


	/// <summary>
	/// 更新函数
	/// </summary>
	void Update()
	{
		if (_isOver) return;             //如果游戏结束，直接跳出不执行
		if (Input.GetMouseButtonDown(0)) //按下鼠标左键
		{
			_score++;                           //分数自增
			ScoreText.text = _score.ToString(); //给文本赋值，int转string
			_selectPin.DistanceBall();          //调用朝着球飞的方法
			GenerateArrow();                    //生成一个新的箭，准备
		}
	}


	/// <summary>
	/// 游戏结束
	/// </summary>
	public void GameOver()
	{
		if (_isOver) return;                                            //如果游戏结束状态为True，直接跳出
		GameObject.Find("Whirl").GetComponent<Whirl>().enabled = false; //关闭球体旋转
		StartCoroutine(GameOverAnimation());                            //开启
		_isOver = true;                                                 //游戏结束
	}


	/// <summary>
	/// 开启协程 —— 游戏结束的动画
	/// </summary>
	/// <returns></returns>
	IEnumerator GameOverAnimation()
	{
		_isOver = true; //游戏结束
		while (true)    //死循环
		{
			//插值运算
			//主相机背景色 = 颜色，插值（当前色，目标色，渐变速度）
			_mainCamera.backgroundColor = Color.Lerp(_mainCamera.backgroundColor, Color.green, _animationSpeed * Time.deltaTime);

			//主相机的正交尺寸 = 运算，插值（当前尺寸，目标尺寸，渐变速度）
			_mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, 3.5f, _animationSpeed * Time.deltaTime);

			//判断是否达到目标值 运算，计算向量（当前值 减去 3.5f ）< 0.01f的话   跳出
			if (Mathf.Abs(_mainCamera.orthographicSize - 3.5f) < 0.01f) break;

			yield return 2; //每次暂停一帧
		}

		yield return new WaitForSeconds(1);                               //等待1秒
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //重载当前场景
	}
}