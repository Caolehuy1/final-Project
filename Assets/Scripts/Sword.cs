using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private PlayerControlls playerControlls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon ActiveWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        ActiveWeapon = GetComponent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControlls = new PlayerControlls();
    }
    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void Start()
    {
        playerControlls.Combat.Attack.started += _ => Attack();
    }
    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);


        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x <playerScreenPoint.x)
        {
            ActiveWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        } else
        {
            ActiveWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
