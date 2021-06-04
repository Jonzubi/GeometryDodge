﻿using UnityEngine;

[RequireComponent(typeof(InventoryController))]
public class PlayerController : MonoBehaviour
{
    public float m_playerMovement = 1f;
    public float m_rotateSpeed = 0.1f;
    FloatingJoystick m_playerJoystick;

    InventoryController m_inventory;
    GameManager m_GameManager;
    SpawnManager m_SpawnManager;
    Vector3 targetPosition;
    Vector2 lastMoveTo = Vector2.right;

    void Awake()
    {
        m_playerJoystick = FindObjectOfType<FloatingJoystick>();
        m_GameManager = FindObjectOfType<GameManager>();
        m_SpawnManager = FindObjectOfType<SpawnManager>();
        m_inventory = GetComponent<InventoryController>();
        targetPosition = transform.position;
    }

    void Update()
    {
        float horizontal = m_playerJoystick.Horizontal;
        float vertical = m_playerJoystick.Vertical;

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        if (moveDirection == Vector2.zero)
            moveDirection = lastMoveTo;

        Vector2 goTo = moveDirection;
        Vector2 isOutOfBounds = (Vector2)transform.position + (moveDirection * m_playerMovement * Time.deltaTime);

        if (isOutOfBounds.x < m_GameManager.leftBoundX || isOutOfBounds.x > m_GameManager.rightBoundX )
            goTo.x = 0;
        if (isOutOfBounds.y < m_GameManager.bottomBoundY - 0.5f || isOutOfBounds.y > m_GameManager.topBoundY)
            goTo.y = 0;


        transform.Translate(goTo * m_playerMovement * Time.deltaTime, Space.World);
        float angle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;
        Vector3 auxAngles = transform.rotation.eulerAngles;
        transform.eulerAngles = new Vector3(auxAngles.x, auxAngles.y, -angle);
        lastMoveTo = moveDirection;
    }

    // Esta funcion es llamada desde SlotsController, se produce cuando un Slot en el HUD es pulsado.
    public void OnItemUsed(int slotId)
    {
        m_inventory.OnItemUsed(slotId, transform.position, transform.rotation);
        m_GameManager.RenderHUDSlots(m_inventory.items);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Coin":
                GameDataCollector.CoinReceived();
                Destroy(other.gameObject);
                break;
            case "PowerUp":
                PowerUp auxPowerUp = other.gameObject.GetComponent<PowerUp>();
                bool hasTaken = m_inventory.addItem(new Item(auxPowerUp.itemId, auxPowerUp.itemAmount));
                if (hasTaken)
                {
                    Destroy(other.gameObject);
                    m_GameManager.RenderHUDSlots(m_inventory.items);
                }                    
                break;
            default:
                break;

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            m_GameManager.GameOver(m_inventory.items);
        }   
    }
}
