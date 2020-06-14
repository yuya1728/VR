using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using VrmArPlayer;
using VRM;

public class VRTest : MonoBehaviour
{
    private SteamVR_Action_Boolean actionToHaptic = SteamVR_Actions._default.Menu;

    private SteamVR_Action_Vibration haptic = SteamVR_Actions._default.Haptic;

    public SteamVR_Action_Boolean actionToHaptic2 = SteamVR_Actions._default.GrabPinch;

    public SteamVR_Action_Pose actionToHaptic3 = SteamVR_Actions._default.Pose;
    private SteamVR_Action_Boolean actionToHaptic4 = SteamVR_Actions._default.GrabGrip;

    private SteamVR_Action_Vector2 actionToHaptic5 = SteamVR_Actions._default.TouchPad;
    public AudioClip sound1;

    public AudioClip sound2;

    AudioSource audioSource;
    AudioSource audioSource2;
    public GameObject head;
    public GameObject waist;
    private GameObject righthand;
    private GameObject lefthand;
    public int i=0;
    public int a,b,c,d;
   
    public bool n;
    public float v,w,x,y;
    private Vector3 tmp;
    public Vector3 tmp2;
    private Vector3 tmp3;
    private Vector3 tmp4;
    private Vector3 tmp5;
    public Player player;
    [SerializeField] private FingerController fingerController;
    public ControllerInputManager ControllerInput;
    public Animator vr;
    private Transform  RightIndexProximal, RightMiddleProximal, RightThumbProximal,RightRingProximal, 
        RightLittleProximal, RightThumbDistal,RightIndexIntermediate, RightIndexDistal, RightThumbIntermediate,
        RightMiddleIntermediate, RightMiddleDistal;
    private Transform  LeftIndexProximal, LeftMiddleProximal, LeftThumbProximal, LeftRingProximal,
        LeftLittleProximal, LeftThumbDistal, LeftIndexIntermediate,LeftIndexDistal, LeftThumbIntermediate,
        LeftMiddleIntermediate, LeftMiddleDistal;
    private void Start()

    {
        //Componentを取得

        audioSource = GetComponent<AudioSource>();
        audioSource2 = GetComponent<AudioSource>();
        head = GameObject.Find("Head");
        waist = GameObject.Find("Waist");
        righthand = GameObject.Find("RightHand");
        lefthand = GameObject.Find("LeftHand");
        n = false;
        a = 1;
        b = 0;
        w = 0f;
        v = 0f;
        x = 0f;
        y = 0f;
      
        c = 0;
        d = 0;
        RightIndexProximal = vr.GetBoneTransform(HumanBodyBones.RightIndexProximal);
        RightMiddleProximal = vr.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
        RightThumbProximal = vr.GetBoneTransform(HumanBodyBones.RightThumbProximal);
        RightRingProximal = vr.GetBoneTransform(HumanBodyBones.RightRingProximal);
        RightLittleProximal = vr.GetBoneTransform(HumanBodyBones.RightLittleProximal);
        RightThumbDistal= vr.GetBoneTransform(HumanBodyBones.RightThumbDistal);
        LeftThumbProximal = vr.GetBoneTransform(HumanBodyBones.LeftThumbProximal);
        LeftIndexProximal = vr.GetBoneTransform(HumanBodyBones.LeftIndexProximal);
        LeftMiddleProximal = vr.GetBoneTransform(HumanBodyBones.LeftMiddleProximal);
        LeftRingProximal = vr.GetBoneTransform(HumanBodyBones.LeftRingProximal);
        LeftLittleProximal = vr.GetBoneTransform(HumanBodyBones.LeftLittleProximal);
        LeftThumbDistal= vr.GetBoneTransform(HumanBodyBones.LeftThumbDistal);
        RightIndexIntermediate = vr.GetBoneTransform(HumanBodyBones.RightIndexIntermediate);
        RightIndexDistal = vr.GetBoneTransform(HumanBodyBones.RightIndexDistal);
        RightMiddleIntermediate = vr.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate);
        RightMiddleDistal = vr.GetBoneTransform(HumanBodyBones.RightMiddleDistal);
        LeftIndexIntermediate = vr.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate);
        LeftIndexDistal = vr.GetBoneTransform(HumanBodyBones.LeftIndexDistal);
        LeftMiddleIntermediate = vr.GetBoneTransform(HumanBodyBones.LeftMiddleIntermediate);
        LeftMiddleDistal = vr.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);
        RightThumbIntermediate= vr.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
        LeftThumbIntermediate = vr.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
    }



    private void Update()
    { 
        tmp = righthand.transform.position;            
        var WastAngle =waist.transform.rotation.eulerAngles;
        tmp2 = waist.transform.position;    
        tmp3 = WastAngle;
        tmp4 = lefthand.transform.position;
        tmp5 = head.transform.position;
       
        if (ControllerInput.l == false)
        {


            if (actionToHaptic.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                haptic.Execute(0, 0.1f, 60, 1, SteamVR_Input_Sources.LeftHand);
            }
            else if (actionToHaptic.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                haptic.Execute(0, 0.1f, 60, 1, SteamVR_Input_Sources.RightHand);
            }
            if (actionToHaptic2.GetStateDown(SteamVR_Input_Sources.Any))
            {
                audioSource.PlayOneShot(sound1);
                WaistRotationCheck();
            }

            if (actionToHaptic2.GetState(SteamVR_Input_Sources.RightHand))
            {
                BgmChange(i);
                w = actionToHaptic3.GetVelocity(SteamVR_Input_Sources.RightHand).magnitude;
                x = actionToHaptic3.GetVelocity(SteamVR_Input_Sources.LeftFoot).magnitude;
                y = actionToHaptic3.GetVelocity(SteamVR_Input_Sources.RightFoot).magnitude;             
                    
                Debug.Log(w);
            }
          
            else if (actionToHaptic2.GetState(SteamVR_Input_Sources.LeftHand))
            {
                CameraChange(b);
                v = actionToHaptic3.GetVelocity(SteamVR_Input_Sources.LeftHand).magnitude;
                x = actionToHaptic3.GetVelocity(SteamVR_Input_Sources.LeftFoot).magnitude;
                y = actionToHaptic3.GetVelocity(SteamVR_Input_Sources.RightFoot).magnitude;
                Debug.Log(v);
            }
            else
            {
                w = 0f;
                v = 0f;
                x = 0f;
                y = 0f;

            }

        }
    }

    public void WaistRotationCheck()
    {
        if (135f <= tmp3.y && tmp3.y <= 225f)
        {
            if (tmp.x > tmp2.x)
            {
                i = 1;
            }
            else if (tmp.x < tmp2.x)
            {
                i = 2;
            }
            if (tmp4.x > tmp2.x)
            {
                b = 1;
            }
            else if (tmp4.x < tmp2.x)
            {
                b = 2;
            }
        }

        if (45f <= tmp3.y && tmp3.y < 135f)
        {
            if (tmp.z > tmp2.z)
            {
                i = 3;
            }
            else if (tmp.z < tmp2.z)
            {
                i = 4;
            }
            if (tmp4.z > tmp2.z)
            {
                b = 3;
            }
            else if (tmp4.z < tmp2.z)
            {
                b = 4;
            }
        }

        if ((325f <= tmp3.y && tmp3.y < 360f) || (0f <= tmp3.y && tmp3.y < 45f))
        {
            if (tmp.x < tmp2.x)
            {
                i = 5;
            }
            else if (tmp.x > tmp2.x)
            {
                i = 6;
            }
            if (tmp4.x < tmp2.x)
            {
                b = 5;
            }
            else if (tmp4.x > tmp2.x)
            {
                b = 6;
            }
        }

        if (225 < tmp3.y && tmp3.y < 315)
        {
            if (tmp.z < tmp2.z)
            {
                i = 7;
            }
            else if (tmp.z > tmp2.z)
            {
                i = 8;
            }
            if (tmp4.z < tmp2.z)
            {
                b = 7;
            }
            else if (tmp4.z > tmp2.z)
            {
                b = 8;
            }
        }
    }

    public void BgmChange(int num)
    {
        switch (num)
        {
            case 1 when tmp.x < tmp2.x:

            case 3 when tmp.z < tmp2.z:

            case 5 when tmp.x > tmp2.x:

            case 7 when tmp.z > tmp2.z:
                audioSource2.PlayOneShot(sound2);

                break;

            case 2 when tmp.x > tmp2.x:

            case 4 when tmp.z > tmp2.z:

            case 6 when tmp.x < tmp2.x:

            case 8 when tmp.z < tmp2.z:
                audioSource.Stop();

                break;
        }
        
    }
    public void CameraChange(int num)
    {
        switch (num)
        {
            case 1 when tmp4.x < tmp2.x:

            case 3 when tmp4.z < tmp2.z:

            case 5 when tmp4.x > tmp2.x:

            case 7 when tmp4.z > tmp2.z:

                a = a + 1;
                if (a > 5)
                {
                    a = 1;
                }
                break;

            case 2 when tmp4.x > tmp2.x:

            case 4 when tmp4.z > tmp2.z:

            case 6 when tmp4.x < tmp2.x:

            case 8 when tmp4.z < tmp2.z:

                break;

        }     
    }
    void LateUpdate()
    {
        var axis = actionToHaptic5.GetAxis(SteamVR_Input_Sources.RightHand);

        var axis2 = actionToHaptic5.GetAxis(SteamVR_Input_Sources.LeftHand);    

        switch (c)
        {

            case 1:
                fingerController.FingerRotation(FingerController.FingerType.RightAll, 1); //いっかい全部曲げる
                fingerController.FingerRotation(FingerController.FingerType.RightIndex, 0); // 人差し指を開く
                fingerController.FingerRotation(FingerController.FingerType.RightMiddle, 0); // 中指を開く             
                RightIndexProximal.localRotation = Quaternion.Euler(0, -10, 0);
                RightMiddleProximal.localRotation = Quaternion.Euler(0, 10, 0);
                break;

            case 2:
                fingerController.FingerRotation(FingerController.FingerType.LeftAll, 1); //いっかい全部曲げる
                fingerController.FingerRotation(FingerController.FingerType.LeftIndex, 0); // 人差し指を開く
                fingerController.FingerRotation(FingerController.FingerType.LeftMiddle, 0); // 中指を開く             
                LeftIndexProximal.localRotation = Quaternion.Euler(0, 10, 0);
                LeftMiddleProximal.localRotation = Quaternion.Euler(0, -10, 0);
                break;

            case 3:
                fingerController.FingerRotation(FingerController.FingerType.RightAll, 0); //いっかい全部伸ばす
                fingerController.FingerRotation(FingerController.FingerType.LeftAll, 0); //いっかい全部伸ばす             
                RightIndexProximal.localRotation = Quaternion.Euler(0, -20, 0);
                RightMiddleProximal.localRotation = Quaternion.Euler(0, 10, 0);
                RightThumbProximal.localRotation = Quaternion.Euler(0, -20, 0);
                RightThumbDistal.localRotation = Quaternion.Euler(0, -20, 0);
                RightRingProximal.localRotation = Quaternion.Euler(0, 20, 0);
                RightLittleProximal.localRotation = Quaternion.Euler(0, 35, 0);
                LeftIndexProximal.localRotation = Quaternion.Euler(0, 20, 0);
                LeftMiddleProximal.localRotation = Quaternion.Euler(0, -10, 0);
                LeftThumbProximal.localRotation = Quaternion.Euler(0, 20, 0);
                LeftThumbDistal.localRotation = Quaternion.Euler(0, 20, 0);
                LeftRingProximal.localRotation = Quaternion.Euler(0, -20, 0);
                LeftLittleProximal.localRotation = Quaternion.Euler(0, -35, 0);
                break;

            case 4:
                fingerController.FingerRotation(FingerController.FingerType.LeftAll, 1); //いっかい全部曲げる
                fingerController.FingerRotation(FingerController.FingerType.LeftIndex, 0); // 人差し指を開く
                break;

            case 5:
                fingerController.FingerRotation(FingerController.FingerType.RightAll, 1); //いっかい全部曲げる
                fingerController.FingerRotation(FingerController.FingerType.LeftAll, 1); //いっかい全部曲げる
                break;

            case 6:
                fingerController.FingerRotation(FingerController.FingerType.RightAll, 1); //いっかい全部曲げる
                fingerController.FingerRotation(FingerController.FingerType.LeftAll, 1); //いっかい全部曲げる
                fingerController.FingerRotation(FingerController.FingerType.RightIndex, 0); // 人差し指を開く
                fingerController.FingerRotation(FingerController.FingerType.RightMiddle, 0); // 中指を開く
                fingerController.FingerRotation(FingerController.FingerType.LeftIndex, 0); // 人差し指を開く
                fingerController.FingerRotation(FingerController.FingerType.LeftMiddle, 0); // 中指を開く
                RightIndexIntermediate.localRotation= Quaternion.Euler(0, 0, -30);
                RightIndexDistal.localRotation = Quaternion.Euler(0, 0, -30);
                RightMiddleIntermediate.localRotation = Quaternion.Euler(0, 0, -30);
                RightMiddleDistal.localRotation = Quaternion.Euler(0, 0, -30);
                LeftIndexIntermediate.localRotation = Quaternion.Euler(0, 0, 30);
                LeftIndexDistal.localRotation = Quaternion.Euler(0, 0, 30);
                LeftMiddleIntermediate.localRotation = Quaternion.Euler(0, 0, 30);
                LeftMiddleDistal.localRotation = Quaternion.Euler(0, 0, 30);
                break;
        }
        if (actionToHaptic4.GetState(SteamVR_Input_Sources.RightHand))
        {
            fingerController.FingerRotation(FingerController.FingerType.RightAll, 1); //いっかい全部曲げる
        }
        if (actionToHaptic4.GetState(SteamVR_Input_Sources.LeftHand))
        {
            fingerController.FingerRotation(FingerController.FingerType.LeftAll, 1); //いっかい全部曲げる
        }
        if (axis.x < 0) // 左
        {
            fingerController.FingerRotation(FingerController.FingerType.RightThumb, 0); 
            RightThumbProximal.localRotation = Quaternion.Euler(0, 0, -30);
            RightThumbIntermediate.localRotation = Quaternion.Euler(0, 0, -20);
        }
        else if (axis.x > 0) // 右
        {
            fingerController.FingerRotation(FingerController.FingerType.RightThumb, 0);
            RightThumbProximal.localRotation = Quaternion.Euler(-30, -15, 0);
            RightThumbIntermediate.localRotation = Quaternion.Euler(0, -40, 0);
        }
        if (axis2.x < 0) // 左
        {
            fingerController.FingerRotation(FingerController.FingerType.LeftThumb, 0);
            LeftThumbProximal.localRotation = Quaternion.Euler(-30, 15, 0);
            LeftThumbIntermediate.localRotation = Quaternion.Euler(0, 40, 0);
        }
        else if (axis2.x > 0) // 右
        {
            fingerController.FingerRotation(FingerController.FingerType.LeftThumb, 0);
           LeftThumbProximal.localRotation = Quaternion.Euler(0, 0, 40);
            LeftThumbIntermediate.localRotation = Quaternion.Euler(0, 0, 20);
        }
    }


}

