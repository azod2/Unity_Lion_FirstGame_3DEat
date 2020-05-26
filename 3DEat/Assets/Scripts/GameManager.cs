using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("道具")]
    public GameObject[] porps;
    [Header("文字介面 : 道具總數")]
    public Text textCount;
    [Header("文字介面 : 倒數時間")]
    public Text textTime;
    [Header("文字介面 : 遊戲結束畫面標題")]
    public Text textTitle;
    [Header("結束畫面")]
    public CanvasGroup final;

    private float gameTime = 30;


    /// <summary>
    /// 道具總數
    /// </summary>
    private int countTotal;

    /// <summary>
    /// 取得道具數量
    /// </summary>
    private int countProp;



    /// <summary>
    /// 生成道具
    /// </summary>
    /// <param name="prop">想要生成的道具</param>
    /// <param name="count">想要生成的數量+隨機值 + - 5</param>
    /// <returns>傳回生成幾顆</returns>
    private int CreatProp(GameObject prop, int count)
    {
        //取得隨機道具數量 = 指定的數量 + - 5
        int total = count + Random.Range(-5, 5);

        for (int i = 0; i < total; i++)
        {
            //座標(隨機,1.5,隨機)
            Vector3 pos = new Vector3(Random.Range(-10, 10), 1.5f, Random.Range(-10, 10));
            //生成(物件，座標，角度)
            Instantiate(prop, pos, Quaternion.identity);
        }
        return total;
    }


    private void CountTime()
    {
        gameTime -= Time.deltaTime;
        textTime.text = "倒數時間 : " + gameTime.ToString("f2");
        
    }


    // Start is called before the first frame update
    void Start()
    {
        countTotal = CreatProp(porps[0], 20); //道具總數=生成道具(道具一號，指定數量)
        countTotal = CreatProp(porps[1], 10); //道具總數=生成道具(道具一號，指定數量)
        textCount.text = "道具數量 : 0 / " + countTotal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
