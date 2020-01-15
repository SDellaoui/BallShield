using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    bool isHittingBall;
    bool isPunching;
    
    float resetCatchTime;
    float timeLeftToDie;
    public float timeBeforeDie;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isPunching){
            isPunching = true;
            resetCatchTime = Time.time + 0.1f;
        }
        if(isPunching && resetCatchTime < Time.time)
            isPunching = false;
        Debug.Log(isPunching);
        if(isHittingBall){
            if(timeLeftToDie < Time.time && !isPunching)
                Die();
            if(isPunching)
                Block();
        }
        /*
        //Check if we triggered the catch when triggering
        if(resetCatchTime < Time.time)
            isCatching = false;
        if(Input.GetMouseButtonDown(0) && resetCatchTime < Time.time)
        {
            resetCatchTime = Time.time + 0.5f;
            isCatching = true;
        }
        */
    }
    //public bool IsCatching(){return isCatching;}
    void Die()
    {
        Debug.Log("Deaaaaad ! ");
        transform.root.GetComponent<PlayerController>().SendFlashColorEvent(Color.red);
        ResetTimers();
    }
    void Block()
    {
        Debug.Log("Blooock !");
        transform.root.GetComponent<PlayerController>().SendFlashColorEvent(Color.green);
        ResetTimers();
    }
    void ResetTimers()
    {
        timeLeftToDie = 0f;
        isHittingBall = false;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.layer == 12)
        {
            isHittingBall = true;
            timeLeftToDie = Time.time + timeBeforeDie;
            /*
            Debug.Log("Enter Ball area "+isCatching);
            if(isCatching){
                //transform.root.GetComponent<PlayerBehaviour>().SendFlashColorEvent(Color.red);
                Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
                coll.gameObject.GetComponent<BallBehaviour>().SendBallHitEvent(dir.normalized);
            }
            */
        }
    }
}
