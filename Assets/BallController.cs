using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    //ボールが見える可能性のあるz軸の最大値
    private float visiblePosZ = -6.5f;

    //ゲームオーバを表示するテキスト
    private GameObject gameoverText;

    //スコアを表示するテキスト
    private GameObject ScoreText;

    //スコアの値
    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //シーン中のGameOverTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");

        //シーン中のScoreTextオブジェクトを取得
        this.ScoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
        {
            //GameoverTextにゲームオーバを表示
            this.gameoverText.GetComponent<Text>().text = "Game\nOver";

            //シーンのリセット
            if (Input.touchSupported)//タッチに対応しているか判定
            {
                if (Input.touchCount > 0)//タッチしているか判定
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            //タッチ未対応
            else
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.DownArrow))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        //変数Scoreの値を更新
        switch (other.gameObject.tag)
        {
            case "SmallStarTag":
                Score += 10;
                break;
            case "LargeStarTag":
                Score += 50;
                break;
            case "SmallCloudTag":
                Score += 30;
                break;
            case "LargeCloudTag":
                Score += 100;
                break;
        }
        //ScoreTextの点数を更新
        this.ScoreText.GetComponent<Text>().text = "Score:" + Score.ToString("00000");
    }
}
