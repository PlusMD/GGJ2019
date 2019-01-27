using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerMovement : MonoBehaviour
{

    Vector2 rotation = Vector2.zero;
    public float sensitivity = 3;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public CinemachineVirtualCamera cameraRig;
    public Transform cockpit;
    float tiltAngle = 0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    public AudioClip Jump;
    public AudioClip Land;
    AudioSource JumpAudio;
    bool InAir = false;
    float InAirTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        JumpAudio = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;

            if (Input.GetButton("Jump"))
            {
                InAir = true;
                //JumpAudio.Play(0);
                JumpAudio.Play(0);
                moveDirection.y = jumpSpeed;
            }
        }
        if(InAir)
        {
            InAirTime -= Time.deltaTime;
            if(InAirTime <= 0)
            {
                if (controller.isGrounded)
                {
                    InAirTime = 1f;
                    InAir = false;
                    JumpAudio.Play(0);
                    JumpAudio.PlayOneShot(Land, 0F);
                }
            }
        }
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        controller.Move(moveDirection * Time.deltaTime);

        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        rotation.x = Mathf.Clamp(rotation.x, -90f, 90f);
        transform.eulerAngles = new Vector2(0, rotation.y) * sensitivity;
        cameraRig.transform.localRotation = Quaternion.Euler(rotation.x * sensitivity, 0, 0);

        if (Input.GetKey(KeyCode.A))
        {
            float newTilt = Mathf.SmoothDamp(cameraRig.m_Lens.Dutch, -5, ref tiltAngle, 0.1f);
            cameraRig.m_Lens.Dutch = newTilt;
        }

        else if (Input.GetKey(KeyCode.D))
        {
            float newTilt = Mathf.SmoothDamp(cameraRig.m_Lens.Dutch, 5, ref tiltAngle, 0.1f);
            cameraRig.m_Lens.Dutch = newTilt;
        }

        else
        {
            float newTilt = Mathf.SmoothDamp(cameraRig.m_Lens.Dutch, 0, ref tiltAngle, 0.1f);
            cameraRig.m_Lens.Dutch = newTilt;
        }
    }
}
