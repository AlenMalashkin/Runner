using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 vec;
    private Animator anim;
    public AudioSource audioSource;
    public GameObject buttonShield;
    private CapsuleCollider playerCol;
    public AfterLoseAd interAd;
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private int coinsCounter;
    [SerializeField] private Text coinsCounterText;
    [SerializeField] private Score scoreScript;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;

    private bool isSliding;
    private bool isImmortal;

    private int lineToMove = 1;
    public float lineDistance = 6;
    private float maxSpeed = 70;
    private int tryCount;


    private void Jump()
    {
        vec.y = jumpStrength;
        anim.SetTrigger("Jump");
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        losePanel.SetActive(false);
        Time.timeScale = 1;
        coinsCounter = PlayerPrefs.GetInt("coins");
        coinsCounterText.text = coinsCounter.ToString();
        playerCol = GetComponent<CapsuleCollider>();
        StartCoroutine(SpeedIncrease());
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        tryCount = PlayerPrefs.GetInt("tryCount");
        isImmortal = false;

        if (!PlayerPrefs.HasKey("shield"))
        {
            buttonShield.SetActive(false);
        }
    }

    private void Update()
    {
        if(SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }

        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }

        if (SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        Vector3 targetPositoin = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPositoin += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPositoin += Vector3.right * lineDistance;

        if (transform.position == targetPositoin)
            return;
        Vector3 diff = targetPositoin - transform.position;
        Vector3 moveVec = diff.normalized * 25 * Time.deltaTime;
        if (moveVec.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveVec);
        else
            controller.Move(diff);

        if (controller.isGrounded && !isSliding)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        vec.z = speed;
        vec.y += gravity * Time.fixedDeltaTime;
        controller.Move(vec * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "barrier")
        {
            if (isImmortal == true)
            {
                Destroy(hit.gameObject);
            }
            else
            {
                tryCount++;
                PlayerPrefs.SetInt("tryCount", tryCount);
                if (tryCount % 7 == 0)
                    interAd.ShowAd();
                losePanel.SetActive(true);
                int lastScore = int.Parse(scoreScript.score.text.ToString());
                PlayerPrefs.SetInt("lastScore", lastScore);
                audioSource.mute = !audioSource.mute;
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "coin")
        {
            coinsCounter++;
            PlayerPrefs.SetInt("coins", coinsCounter);
            coinsCounterText.text = coinsCounter.ToString();
            Destroy(col.gameObject);
        }
    }

    public void useShield()
    {
        StartCoroutine(Immortal());
        buttonShield.gameObject.SetActive(false);
        PlayerPrefs.DeleteKey("shield");
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if (speed < maxSpeed)
        {
            speed += 3;
            StartCoroutine(SpeedIncrease());
        }
    }

    private IEnumerator Slide()
    {
        controller.center = new Vector3(0, 1.5f, 0.52f);
        controller.height = 2.42f;
        isSliding = true;
        anim.SetTrigger("Slide");

        yield return new WaitForSeconds(1);

        controller.center = new Vector3(0, 2.5f, 0.52f);
        controller.height = 4.78f;
    }

    private IEnumerator Immortal()
    {
        isImmortal = true;
        yield return new WaitForSeconds(10);
        isImmortal = false;
    }
}
