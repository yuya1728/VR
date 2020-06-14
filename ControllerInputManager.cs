using AniLipSync.VRM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using VRM;
using VrmArPlayer;

public class ControllerInputManager : MonoBehaviour
{
    public VRMBlendShapeProxy blendShapeProxy;
    public Blinker blinker;
    public AnimMorphTarget animMorphTarget;
   
    public VRTest vrtest;

    private SteamVR_ActionSet[] actionSets;
    private Vector3 tmp;
    private Vector3 tmp2;
    private Vector3 tmp3;
    private Vector3 tmp4;
    private Vector3 tmp5;
    public bool n,m,l;

    private GameObject head;
    private GameObject waist;
    private GameObject righthand;
    private GameObject lefthand;
    private float ScaleHead;
    int b,c;

    [SerializeField] private FingerController fingerController;
    public Animator vr;


    // VRMの表情
    private Dictionary<BlendShapeKey, float> shapeValueDictionary = new Dictionary<BlendShapeKey, float> {
            {new BlendShapeKey(BlendShapePreset.Neutral), 0.0f }, // NEUTRAL(標準)
            {new BlendShapeKey(BlendShapePreset.Blink), 0.0f }, // BLINK(目を閉じる、まばたき)
            {new BlendShapeKey(BlendShapePreset.Joy), 0.0f }, // JOY(喜び)
            {new BlendShapeKey(BlendShapePreset.Angry), 0.0f }, // ANGRY(怒り)
            {new BlendShapeKey(BlendShapePreset.Sorrow), 0.0f }, // SORROW(悲しみ)
            {new BlendShapeKey(BlendShapePreset.Fun), 0.0f }, // FUN(楽しみ)
            {new BlendShapeKey(BlendShapePreset.Blink_L), 0.0f }, // BLINK_L(左目閉じる)
            {new BlendShapeKey(BlendShapePreset.Blink_R), 0.0f }, // BLINK_R(右目閉じる)
            {new BlendShapeKey("><"), 0.0f }, // ><(アリシアちゃん専用表情)
            {new BlendShapeKey(BlendShapePreset.LookUp), 0.0f }, // BLINK_L(左目閉じる)
            {new BlendShapeKey(BlendShapePreset.LookDown), 0.0f }, // BLINK_R(右目閉じる)
            {new BlendShapeKey(BlendShapePreset.LookRight), 0.0f }, // 
        };
    private List<BlendShapeKey> keyList;

    private int GetKeyIndex(BlendShapePreset preset) { return keyList.FindIndex(d => d.Name == preset.ToString().ToUpper()); }
    private int GetKeyIndex(string preset) { return keyList.FindIndex(d => d.Name == preset.ToUpper()); }
    private BlendShapeKey GetKey(BlendShapePreset preset) { return keyList[GetKeyIndex(preset)]; }
    private BlendShapeKey GetKey(string preset) { return keyList[GetKeyIndex(preset)]; }

    
    // Use this for initialization
    void Start()
    {
        keyList = new List<BlendShapeKey>(shapeValueDictionary.Keys);
        actionSets = SteamVR_Input.actionSets;
        // if (actionSets == null) { actionSets = SteamVR_Input_Reference.instance.actionSetObjects; }
        b = 0;
        c = 0;
        n = false;
        m = false;
        l = false;
        head = GameObject.Find("Head");
        waist = GameObject.Find("Waist");
        righthand = GameObject.Find("RightHand");
        lefthand = GameObject.Find("LeftHand");
    }

    // Update is called once per frame
    void Update()
    {
        vrtest.c = 0;
        vrtest.d = 0;
        l = false;
        tmp3 = head.transform.position;
        tmp4 = waist.transform.position;
        ScaleHead = (tmp3.y - tmp4.y) /2;


        if (vrtest.actionToHaptic2.GetState(SteamVR_Input_Sources.LeftHand))
        {
            tmp2 = lefthand.transform.position;
            b = 0;
        }

       
        else if (vrtest.actionToHaptic2.GetState(SteamVR_Input_Sources.LeftHand)==false && b==0)
        {
            tmp2 = new Vector3(0f, 0f, 0f);
            b = 1;
           
        }
        if (vrtest.actionToHaptic2.GetState(SteamVR_Input_Sources.RightHand))
        {
            tmp = righthand.transform.position;
            c = 0;
        }
        else if (vrtest.actionToHaptic2.GetState(SteamVR_Input_Sources.RightHand) == false && c == 0)
        {
            tmp = new Vector3(0f, 0f, 0f);
            c = 1;
        }
       
        if (b == 1 && c == 1)
        {
            //全ての表情を一旦無効にする
            foreach (var key in keyList)
            {
                shapeValueDictionary[key] = 0.0f;
            }
            Neutral();
            l = false;
            //表情を適用する
            blendShapeProxy.SetValues(shapeValueDictionary.ToList());
        }
        if (blendShapeProxy == null) return;

        var sources = SteamVR_Input_Source.GetAllSources();
       
        foreach (var source  in sources)
        {
           
            if (source == SteamVR_Input_Sources.Any) continue;
           
            foreach (var actionSet in actionSets)
            {
               
                foreach (var action in actionSet.allActions) //ボタン
                {
                    if (action is SteamVR_Action_Boolean)
                    {
                        var actionBoolean = (SteamVR_Action_Boolean)action;
                        if (actionBoolean.GetStateDown(source))
                        {
                            var name = actionBoolean.GetShortName();
                            if (name == "InteractUI") // トリガー半引き
                            {
                                
                            }
                            else if (name == "GrabPinch") // トリガー全引き
                            {


                            }
                            else if (name == "Teleport") // タッチパッド押し
                            {

                            }
                            else if (name == "GrabGrip") // グリップボタン押し
                            {

                            }
                            else if (name == "Menu") // Menu押し
                            {
                                Debug.Log("A");
                                var isLeft = source == SteamVR_Input_Sources.LeftHand;
                                if (isLeft)
                                {
                                    if (n)
                                    {
                                        n = false;
                                    }
                                    else
                                    {
                                        n = true;
                                        m = false;
                                    }
                                }
                                else
                                {
                                    if (m)
                                    {
                                        m = false;
                                    }
                                    else
                                    {
                                        m = true;
                                        n = false;
                                    }
                                }
                            }

                        }
                       
                        if (actionBoolean.GetState(source))
                        {

                            //全ての表情を一旦無効にする
                            foreach (var key in keyList)
                            {
                                shapeValueDictionary[key] = 0.0f;
                            }
                            var name = actionBoolean.GetShortName();
                            if (name == "GrabPinch") // トリガー全引き
                            {                                                                                           
                                if (tmp.y < tmp3.y && tmp.y >= tmp3.y - ScaleHead)
                                {
                                    //表情とぶつからないようにまばたきを止めてリップシンクを弱くする
                                    blinker.enabled = false;
                                    animMorphTarget.curveAmplifier = 0.1f;
                                    l = true;
                                    if (tmp2.y >= tmp3.y - ScaleHead && tmp2.y < tmp3.y)
                                    {                                        
                                        shapeValueDictionary[GetKey(BlendShapePreset.LookUp)] = 1.0f;
                                        vrtest.c = 3;
                                    }
                                    else
                                    {
                                        if (n == true)
                                        {
                                            shapeValueDictionary[GetKey(BlendShapePreset.Blink_R)] = 1.0f;
                                            vrtest.c = 1;
                                        }
                                        else if (m == true)
                                        {
                                            shapeValueDictionary[GetKey(BlendShapePreset.LookDown)] = 1.0f;
                                            vrtest.c = 3;
                                        }
                                        else
                                        {
                                            shapeValueDictionary[GetKey(BlendShapePreset.Sorrow)] = 1.0f;
                                            vrtest.c = 6;
                                        }
                                    }

                                }

                                else if (tmp2.y < tmp3.y && tmp2.y >= tmp3.y -ScaleHead)
                                {
                                    //表情とぶつからないようにまばたきを止めてリップシンクを弱くする
                                    blinker.enabled = false;
                                    animMorphTarget.curveAmplifier = 0.1f;
                                    l = true;
                                    if (tmp.y >= tmp3.y - ScaleHead && tmp.y < tmp3.y)
                                    {
                                        shapeValueDictionary[GetKey(BlendShapePreset.LookUp)] = 1.0f;
                                        vrtest.c = 3;
                                    }
                                    else
                                    {
                                        if (n == true)
                                        {
                                            shapeValueDictionary[GetKey(BlendShapePreset.Blink_L)] = 1.0f;
                                            vrtest.c = 2;
                                        }
                                        else if (m == true)
                                        {
                                            shapeValueDictionary[GetKey(BlendShapePreset.LookRight)] = 1.0f;
                                            vrtest.c = 5;
                                        }

                                        else
                                        {
                                            shapeValueDictionary[GetKey(BlendShapePreset.Fun)] = 1.0f;
                                            vrtest.c = 4;
                                        }
                                    }

                                }
                                else if (tmp.y >= tmp3.y && tmp2.y >= tmp3.y)
                                {
                                    //表情とぶつからないようにまばたきを止めてリップシンクを弱くする
                                    blinker.enabled = false;
                                    animMorphTarget.curveAmplifier = 0.1f;
                                    l = true;
                                    shapeValueDictionary[GetKey(BlendShapePreset.Joy)] = 1.0f;
                                }
                                else if (tmp.y - tmp2.y <= 0.1 || (tmp.y - tmp2.y >= -0.1 && tmp.y < tmp3.y - ScaleHead && tmp2.y < tmp3.y - ScaleHead))
                                {
                                    //表情とぶつからないようにまばたきを止めてリップシンクを弱くする
                                    blinker.enabled = false;
                                    animMorphTarget.curveAmplifier = 0.1f;
                                    vrtest.WaistRotationCheck();
                                    Angry(vrtest.i);
                                }
                              
                                else
                                {
                                    Neutral();
                                    l = false;
                                   
                                }
                            }
                         
                            //表情を適用する
                            blendShapeProxy.SetValues(shapeValueDictionary.ToList());
                           
                        }
                       
                    }

                    else if (action is SteamVR_Action_Single) //Axis
                    {
                        var actionSingle = (SteamVR_Action_Single)action;
                        if (actionSingle.GetChanged(source))
                        {
                            var name = actionSingle.GetShortName();
                            var axis = actionSingle.GetAxis(source);
                            if (name == "Squeeze") // トリガー
                            {
                               
                            }
                        }
                    }
                    else if (action is SteamVR_Action_Vector2) // Padやスティック
                    {
                        var actionVector2 = (SteamVR_Action_Vector2)action;
                        if (actionVector2.GetChanged(source))
                        {
                            var name = actionVector2.GetShortName();
                            var axis = actionVector2.GetAxis(source);
                            //
                            // タッチパッドの座標(両手とも)
                            //        (0, 1)
                            // (-1, 0)(0, 0)(1, 0)
                            //        (0,-1)
                            //
                            // タッチパッドを離した時は(0,0)が飛んでくる
                            //
                            if (name == "TouchPad")
                            {
                                var isLeft = source == SteamVR_Input_Sources.LeftHand;

                                //全ての表情を一旦無効にする
                                foreach (var key in keyList)
                                {
                                    shapeValueDictionary[key] = 0.0f;
                                }

                                if (Mathf.Approximately(axis.x, 0.0f) && Mathf.Approximately(axis.y, 0.0f)) //中心(離した時)
                                {
                                    //まばたきとリップシンクを復活させる
                                    blinker.enabled = true;
                                    animMorphTarget.curveAmplifier = 1f;
                                    shapeValueDictionary[GetKey(BlendShapePreset.Neutral)] = 1.0f;
                                }
                                else
                                {
                                    //表情とぶつからないようにまばたきを止めてリップシンクを弱くする
                                    blinker.enabled = false;
                                    animMorphTarget.curveAmplifier = 0.1f;
                                    if (axis.x < 0) // 左
                                    {
                                        if (axis.y > 0) // 左上
                                        {
                                            shapeValueDictionary[GetKey(isLeft ? BlendShapePreset.Blink_L : BlendShapePreset.Blink)] = 1.0f;
                                           
                                        }
                                        else if (axis.y < 0) // 左下
                                        {
                                            shapeValueDictionary[GetKey(isLeft ? BlendShapePreset.Joy : BlendShapePreset.Angry)] = 1.0f;
                                        }
                                    }
                                    else if (axis.x > 0) // 右
                                    {
                                        if (axis.y > 0) // 右上
                                        {
                                            shapeValueDictionary[isLeft ? GetKey("><"): GetKey(BlendShapePreset.Blink_R)] = 1.0f;
                                        }
                                        else if (axis.y < 0) // 右下
                                        {
                                            shapeValueDictionary[GetKey(isLeft ? BlendShapePreset.Sorrow : BlendShapePreset.Fun)] = 1.0f;
                                        }
                                    }
                                }
                                //表情を適用する
                                blendShapeProxy.SetValues(shapeValueDictionary.ToList());
                            }
                        }
                    }
                }
            }
        }
    }

    public void Angry(int num)
    {     
            switch (num)
            {
                case 1 when tmp2.x < tmp4.x:

                case 3 when tmp2.z < tmp4.z:

                case 5 when tmp2.x > tmp4.x:

                case 7 when tmp2.z > tmp4.z:
                //表情とぶつからないようにまばたきを止めてリップシンクを弱くする
                blinker.enabled = false;
                animMorphTarget.curveAmplifier = 0.1f;
                shapeValueDictionary[GetKey(BlendShapePreset.Angry)] = 1.0f;
                c = 5;
                break;

                case 2 when tmp.x > tmp2.x:

                case 4 when tmp.z > tmp2.z:

                case 6 when tmp.x < tmp2.x:

                case 8 when tmp.z < tmp2.z:
                  
                    break;
            }       
    }

    public void Neutral()
    {
        //まばたきとリップシンクを復活させる
        blinker.enabled = true;
        animMorphTarget.curveAmplifier = 1f;
        shapeValueDictionary[GetKey(BlendShapePreset.Neutral)] = 1.0f;
       
    }
}