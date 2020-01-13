using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Bolt.EntityBehaviour<IPlayerState>
{
    // Start is called before the first frame update
    public override void Attached()
    {
        state.SetTransforms(state.transform, transform);
    }

    // Update is called once per frame
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
    }
}
