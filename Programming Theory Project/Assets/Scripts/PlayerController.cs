using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : Unit
{

    public Energy mana = new Energy();
    [SerializeField] float jumpForce;

    public RangeWeapon[] weaponSlot = new RangeWeapon[4];
    [SerializeField] RangeWeapon currentWeapon;

    [SerializeField] Transform characterBody;
    [SerializeField] Transform focalPoint;
    [SerializeField] Camera backView;
    [SerializeField] Camera shoulderView;

    bool isJumping = false;
    Vector2 moveInput;
    Vector3 lookForward;
    Vector3 lookRight;
    Vector3 moveDir;

    protected override void Start()
    {
        base.Start();
        //this line have to move to gamemanager script in the future
        Cursor.lockState = CursorLockMode.Locked;
        currentWeapon = weaponSlot[0];
        mana.RecoverAll();
    }
    protected override void Update()
    {
        base.Update();
        if (mana.CurrentValue < mana.MaxValue) RegenerateMana();

        if (Input.GetKey(KeyCode.Mouse1)) Aim();
        else DeAim();
        if (Input.GetKey(KeyCode.Mouse0)) currentWeapon.Attck();
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwapWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwapWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwapWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwapWeapon(3);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) Jump();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) Move();
    }
    void Move()
    {
        Debug.Log("Moving");
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        lookForward = new Vector3(focalPoint.forward.x, 0f, focalPoint.forward.z).normalized;
        lookRight = new Vector3(focalPoint.right.x, 0f, focalPoint.right.z).normalized; 
        moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        rb.position += moveDir * Time.deltaTime * speed;
    }
    void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    void Aim()
    {
        backView.enabled = false;
        shoulderView.enabled = true;
        characterBody.forward = lookForward;

    }
    void DeAim()
    {
        backView.enabled = true;
        shoulderView.enabled = false;
        characterBody.forward = lookForward;
    }
    void SwapWeapon(int number)
    {
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weaponSlot[number];
        currentWeapon.gameObject.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isJumping = false;
    }
    void RegenerateMana()
    {
        mana.IncreaseCurrentValue(mana.Regeneration * Time.deltaTime);
    }
}
