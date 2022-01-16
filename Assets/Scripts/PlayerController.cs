using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 vec;
    [SerializeField] private float speed;
    [SerializeField] private float jumpStrength;
    [SerializeField] private int coinsCounter;
    [SerializeField] private Text coinsCounterText;
    [SerializeField] private Score scoreScript;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;

    private int lineToMove = 1;
    public float lineDistance = 4;
    private float maxSpeed = 100;


    private void Jump()
    {
        vec.y = jumpStrength;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        losePanel.SetActive(false);
        Time.timeScale = 1;
        coinsCounter = PlayerPrefs.GetInt("coins");
        coinsCounterText.text = coinsCounter.ToString();
        StartCoroutine(SpeedIncrease());
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
            losePanel.SetActive(true);
            int lastScore = int.Parse(scoreScript.score.text.ToString());
            PlayerPrefs.SetInt("lastScore", lastScore);
            Time.timeScale = 0;
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

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(4);
        if (speed < maxSpeed)
        {
            speed += 4;
            StartCoroutine(SpeedIncrease());
        }
    }
}
