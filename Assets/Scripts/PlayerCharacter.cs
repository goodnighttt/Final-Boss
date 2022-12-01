using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement playerMovement;

    [SerializeField]
    private ThirdPerson thirdPerson;

    [SerializeField]
    private Transform followingTarget;

  
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        thirdPerson.InitCamera(followingTarget);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovementInput();
    }

    private void UpdateMovementInput()
    {
        Quaternion rot = Quaternion.Euler(0, thirdPerson.Yaw, 0);//代表旋转Yaw角度的旋转量
        
       // Input.GetAxis("Horizontal");
        playerMovement.SetMovementInput(rot * Vector3.forward* Input.GetAxis("Vertical") + 
            rot * Vector3.right * Input.GetAxis("Horizontal"));
        



    }
}
