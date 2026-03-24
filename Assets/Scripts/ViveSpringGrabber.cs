using UnityEngine;
using System.Collections;
using Valve.VR;

[RequireComponent(typeof(SpringJoint))]
[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
[RequireComponent(typeof(Collider))]
public class ViveSpringGrabber : Grabber
{
    public string grabAction = "GrabPinch";
    private SpringJoint joint;
    public GameObject chestTop;
    private HingeJoint hJoint;
    
    new void Start ()
    {
        base.Start();
        joint = GetComponent<SpringJoint>();
        hJoint = chestTop.GetComponent<HingeJoint>();
    }
	
	protected override void Update ()
    {
        if (joint.connectedBody == null && target != null && SteamVR_Input.GetStateDown(grabAction, controller.inputSource))
        {
            joint.connectedBody = target.GetComponent<Rigidbody>();
        }
        else if (joint.connectedBody != null && SteamVR_Input.GetStateUp(grabAction, controller.inputSource))
        {
            joint.connectedBody = null;
        }
        if (joint.connectedBody != null)
        {
            float duration = 0.2f;
            float strength = 0.75f;
            SteamVR_Actions.default_Haptic[controller.inputSource].Execute(0, duration, -hJoint.angle *10, strength);
        }

    }
}
