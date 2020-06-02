using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;      //場景管理


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

    public float gameTime = 10;

    public GameObject player;


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
            Vector3 pos = new Vector3(Random.Range(-9, 9), 1f, Random.Range(-9, 9));
            //生成(物件，座標，角度)
            Instantiate(prop, pos, Quaternion.identity);
        }
        return total;
    }

    public void Getprop(string prop)
    {
        Debug.Log("GM : "+prop);
        if(prop == "Dia")
        {
            countProp++;
            textCount.text = "道具數量 : " + countProp + "/" + countTotal;

            if (countProp==countTotal)
            {
            Win();
            }


        }
        if(prop == "Ruby")
        {
            gameTime -= 2;
            textCount.text = "倒數時間 : " + gameTime.ToString("f2");
            Lose();
        }
    }

    private void Win()
    {

        Time.timeScale = 0f;
        final.alpha = 1;                                    //顯示結束畫面，啟動互動，啟動遮擋
        final.interactable = true;
        final.blocksRaycasts = true;
        textTitle.text = "挑戰成功";

    }

    private void Lose()
    {
        if (gameTime == 0)
        {
            
            final.alpha = 1;                                    //顯示結束畫面，啟動互動，啟動遮擋
            final.interactable = true;
            final.blocksRaycasts = true;
            textTitle.text = "挑戰失敗";
            player.GetComponent<Player>().enabled = false;
        }
    }


    public void Replay()
    {
        SceneManager.LoadScene("遊戲場景");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }


    private void CountTime()
    {
        //遊戲時間，遞減一禎的時間
        gameTime -= Time.deltaTime;

        //遊戲時間 = 數學.夾住(遊戲時間,最小值,最大值)
        gameTime = Mathf.Clamp(gameTime, 0, 100);

        //更新倒數時間介面ToString("f小數點位數")
        textTime.text = "倒數時間 : " + gameTime.ToString("f2");
        
    }


    // Start is called before the first frame update
    void Start()
    {
        countTotal = CreatProp(porps[0], 10); //道具總數=生成道具(道具一號，指定數量)
        CreatProp(porps[1], 10); //道具總數=生成道具(道具二號，指定數量)
        textCount.text = "道具數量 : 0 / " + countTotal;
    }

    // Update is called once per frame
    void Update()
    {
        CountTime();

        if (gameTime== 0)
        {
            Lose();
        }

    }




}
