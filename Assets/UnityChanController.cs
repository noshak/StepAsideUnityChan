using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator; //アニメーションするためのコンポーネントを入れる

    private Rigidbody myRigidbody; //Unityちゃんを移動させるコンポーネントを入れる

    private float velocityZ = 16f; //前方向の速度
    private float velocityX = 10f; //横方向の速度
    private float velocityY = 10f; //上方向の速度
    private float movableRange = 3.4f; //左右の移動できる範囲
    private float coefficient = 0.99f; //動きを減速させる係数
    private bool isEnd = false; //ゲーム終了の判定
    private GameObject stateText; //ゲーム終了時に表示するテキスト
    private GameObject scoreText; //スコアを表示するテキスト
    private int score = 0; //得点
    private bool isLButtonDown = false; //左ボタン押下の判定
    private bool isRButtonDown = false; //右ボタン押下の判定
    private bool isJButtonDown = false;  //ジャンプボタン押下の判定

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>(); //Animatorコンポーネントを取得

        this.myAnimator.SetFloat("Speed", 1); //走るアニメーションを開始

        this.myRigidbody = GetComponent<Rigidbody>(); //Rigidbodyコンポーネントを取得

        this.stateText = GameObject.Find("GameResultText"); //シーン中のstateTextオブジェクトを取得

        this.scoreText = GameObject.Find("ScoreText"); //シーン中のscoretextオブジェクトを取得
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを減衰する
        if(this.isEnd)
        {
            this.velocityZ *= this.coefficient;
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }
        float inputVelocityX = 0; //横方向の入力による速度
        float inputVelovityY = 0; //上方向の入力による速度

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && - this.movableRange < this.transform.position.x)
        {
            inputVelocityX = -this.velocityX; //左方向への速度を代入
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            inputVelocityX = this.velocityX; //右方向への速度を代入
        }

        //ジャンプしていない時にスペースが押されたらジャンプする
        if((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true); //ジャンプアニメ再生
            inputVelovityY = this.velocityY;  //上方向への速度を代入
        }
        else
        {
            inputVelovityY = this.myRigidbody.velocity.y; //現在のY軸の速度を代入
        }

        //Jumpステートの場合はJumpにFalseをセットする
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //Unityちゃんに速度を与える
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelovityY, velocityZ);
    }

    //トリガーモードで他のオブジェクトと接触した場合の処理
    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;

            //stateTxtにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        //ゴール地点に到達した場合
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;

            //stateTextにGAME CREARを表示
            this.stateText.GetComponent<Text>().text = "GAME CREAR!!";
        }

        //コインに衝突した場合
        if(other.gameObject.tag == "CoinTag")
        {
            this.score += 10;

            //ScoreTextに獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();
            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;
    }

    //ジャンプぼたんを離した場合の処理
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;
    }

    //左ボタンを押し続けた場合の処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    //左ボタンを離した場合の処理
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタンを押し続けた場合の処理
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    //右ボタンを離した場合の処理
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}
