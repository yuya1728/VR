using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{

    

 
[SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject otherCamera;
    [SerializeField]
    private GameObject otherCamera2;
    [SerializeField]
    private GameObject otherCamera3;
    [SerializeField]
    private GameObject otherCamera4;
    public VRTest vRTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Changed();
    }

    public void Changed()
    {

        //　1キーを押したらカメラの切り替えをする
        if (Input.GetKeyDown("1") || vRTest.a == 1)
        {
            mainCamera.SetActive(true);                        
            vRTest.a = 1;
        }
        else
        {
            mainCamera.SetActive(false);
        }
        if (Input.GetKeyDown("2") || vRTest.a == 2)
        {           
            otherCamera.SetActive(true);          
            vRTest.a = 2;
        }
        else
        {
            otherCamera.SetActive(false);
        }
        if (Input.GetKeyDown("3") || vRTest.a == 3)
        {          
            otherCamera2.SetActive(true);
            vRTest.a = 3;
        }
        else
        {
            otherCamera2.SetActive(false);
        }      
        if (Input.GetKeyDown("4") || vRTest.a == 4)
        {             
            otherCamera4.SetActive(true);
            vRTest.a = 4;
        }
        else
        {
            otherCamera4.SetActive(false);
        }
        if (Input.GetKeyDown("5") || vRTest.a == 5)
        {           
            otherCamera3.SetActive(true);
            vRTest.a = 5;
        }
        else
        {
            otherCamera3.SetActive(false);
        }

    }
}
