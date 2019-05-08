using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Transform cameraRig;
    private Transform player;
    public SteamVR_Input_Sources handtype;
    private Vector3 axis = Vector3.zero;
    //public float movementSpeed;
    public SteamVR_Action_Boolean TriggerClickMove;
    private SteamVR_Behaviour_Pose mPose = null;



    private void Awake()
    {
        mPose = GetComponent<SteamVR_Behaviour_Pose>();
    }
    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        player = GetComponent<Transform>();

    }


    // Update is called once per frame
    void Update()
    {
        float yPos = cameraRig.position.y;
         if (TriggerClickMove.GetState(handtype))
        {
            cameraRig.position += transform.forward * Time.deltaTime * 2.5f;
            cameraRig.position = new Vector3(cameraRig.position.x, yPos, cameraRig.position.z);
            Debug.Log("Moving");
        }

        
    }
}
