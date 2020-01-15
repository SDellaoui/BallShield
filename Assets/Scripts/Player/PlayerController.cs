using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Bolt.EntityEventListener<IPlayerState>
{
    bool _isBlocking;
    float resetColorTime;
    public PlayerBehaviour bhv;
    
    #region Public Attributes
    #endregion
    // Start is called before the first frame update
    public override void Attached()
    {
        //renderer.GetComponent<SpriteRenderer>();
        state.SetTransforms(state.transform, transform);
        if(entity.IsOwner)
        {
            state.color = Color.white;
            //state.color = new Color(Random.value, Random.value, Random.value);
        }
        else
            bhv.enabled = false;
        
        state.AddCallback("color", OnColorChanged);
        state.AddCallback("arrowAngle", OnArrowAngleChanged);
    }
    void OnGUI()
    {
        if (entity.IsOwner)
        {
            GUI.color = state.color;
            GUILayout.Label("Player "+entity.NetworkId);
            GUI.color = Color.white;
        }
    }
    void PollKeys()
    {
        _isBlocking = Input.GetMouseButtonDown(0);
    }
    // Update is called once per frame
    void Update()
    {
        PollKeys();
        if(resetColorTime < Time.time)
        {
            GetComponent<SpriteRenderer>().color = state.color;
        }
    }
    public override void SimulateOwner()
    {
        var speed = 4f;
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.Z)) { movement.y += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.y -= 1; }
        if (Input.GetKey(KeyCode.Q)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        state.arrowAngle = angle;
        bhv.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(Input.GetKey(KeyCode.F)){
            SendFlashColorEvent(Color.red);
        }
    }
    public override void SimulateController()
    {
    }
    public override void ExecuteCommand(Bolt.Command command, bool resetState)
    {
    }
    void OnArrowAngleChanged()
    {
        bhv.transform.rotation = Quaternion.AngleAxis(state.arrowAngle, Vector3.forward);
    }
    void OnColorChanged()
    {
        GetComponent<SpriteRenderer>().color = state.color;
    }
    public override void OnEvent(FlashColorEvent evnt)
    {
        resetColorTime = Time.time + 0.25f;
        GetComponent<SpriteRenderer>().color = evnt.FlashColor;
    }
    public void SendFlashColorEvent(Color c)
    {
        var flash = FlashColorEvent.Create(entity);
        flash.FlashColor = c;
        flash.Send();
    }
}
