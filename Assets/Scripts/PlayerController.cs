using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private bool isWalk = false;
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = true;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GunController gunConlroller;

    [SerializeField]
    private Crosshair crosshair;

    private Rigidbody rigid;
    private CapsuleCollider col;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        applySpeed = walkSpeed;
        originPosY = cam.transform.localPosition.y;
        applyCrouchPosY = originPosY;
    }

    private void Update()
    {
        GroundCheck();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        MoveCheck(MathF.Abs(moveX) + MathF.Abs(moveZ));

        Vector3 dirX = moveX * transform.right;
        Vector3 dirZ = moveZ * transform.forward;

        Vector3 dir = (dirX + dirZ).normalized * applySpeed;
        rigid.velocity = dir + (Vector3.up * rigid.velocity.y);
    }

    private void MoveCheck(float input)
    {
        if (!isRun && !isCrouch && isGround)
        {
            if (input > 0)
                isWalk = true;
            else
                isWalk = false;

            crosshair.WalkingAnimation(isWalk);
        }
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isCrouch)
            Crouch();

        gunConlroller.CancelFineSight();

        isRun = true;
        crosshair.RunningAnimation(isRun);
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;

        crosshair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isCrouch)
            Crouch();

        rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    // 앉기 (변수 값 세팅)
    private void Crouch()
    {
        isCrouch = !isCrouch;
        crosshair.CrouchingAnimation(isCrouch);

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        StartCoroutine(CrouchCoroutine());
    }

    // 앉기 코루틴 (카메라 위치 조정)
    IEnumerator CrouchCoroutine()
    {   
        float posY = cam.transform.localPosition.y;
        int count = 0;
        while (posY != applyCrouchPosY)
        {
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.1f);
            cam.transform.localPosition = new Vector3(0, posY, 0);
            if (count > 20)
            {
                break;
            }
            else
            {
                count++;
            }
            yield return null;
        }
        cam.transform.localPosition = new Vector3 (0, applyCrouchPosY, 0);
    }

    private void GroundCheck()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.2f);
        crosshair.JumpingAnimation(!isGround);
    }

    // 캐릭터 회전 (좌, 우)
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(0, yRotation * lookSensitivity, 0));
    }

    // 카메라 회전 (위, 아래)
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        currentCameraRotationX -= Mathf.Clamp(xRotation * lookSensitivity, -cameraRotationLimit, cameraRotationLimit);

        cam.transform.localEulerAngles = Vector3.right * currentCameraRotationX;
    }
}
