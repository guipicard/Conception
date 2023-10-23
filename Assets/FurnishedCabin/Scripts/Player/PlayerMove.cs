using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";

    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] private Transform m_SpawnPoint;

    private CharacterController charController;
    
    private Ray m_MouseRay;
    private RaycastHit m_MouseHit;
    private Inventory m_Inventory;
    private int m_MouseMask;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        m_MouseRay = new Ray();
        m_Inventory = GetComponent<Inventory>();
        LayerMask mask = LayerMask.NameToLayer("item");
        m_MouseMask = 1 << mask.value;
    }

    private void Start()
    {
        transform.position = m_SpawnPoint.position;
    }

    private void Update()
    {
        PlayerMovement();
        m_MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(m_MouseRay, out m_MouseHit, Mathf.Infinity, (1 << 6) | (1 << 12)))
        {
            // Debug.Log(m_MouseHit.collider.gameObject.name);
            GameObject item = m_MouseHit.collider.gameObject;
            if (Input.GetMouseButton(0))
            {
                if (item.CompareTag("Key1"))
                {
                    m_Inventory.AddToInventory(0);
                    item.SetActive(false);
                }

                if (item.CompareTag("Key2"))
                {
                    m_Inventory.AddToInventory(1);
                    item.SetActive(false);
                }
            }
        }
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;     //CharacterController.SimpleMove() applies deltaTime
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        //simple move applies delta time automatically
        charController.SimpleMove(forwardMovement + rightMovement);
    }
}
