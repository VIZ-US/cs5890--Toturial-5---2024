using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;


public class Navigation : MonoBehaviour
{
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    public float speed = 10;
    private XROrigin rig;
    private float fallingSpeed = -9.81f;

    public Animator animator;

    private void FixedUpdate()
    {
        //Vector3 direction = new Vector3(inputAxis.x, 0, inputAxis.y);

        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);


        character.Move(direction * Time.fixedDeltaTime * speed);

        if (direction != Vector3.zero)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
}
