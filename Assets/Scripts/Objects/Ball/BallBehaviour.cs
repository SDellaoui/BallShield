using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : Bolt.EntityEventListener<IBallState>
{
    Rigidbody2D rb;
    
    public float ballSpeed;

    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.ballTransform,transform);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized * ballSpeed;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void SendBallHitEvent(Vector3 direction)
    {
        var flash = BallHitEvent.Create(entity);
        flash.hitDirection = direction;
        flash.Send();
    }
    public override void OnEvent(BallHitEvent evnt)
    {
        rb.velocity = new Vector2(evnt.hitDirection.x,evnt.hitDirection.y) * ballSpeed;
    }
}
