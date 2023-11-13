using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    
    private int hp = 5;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x,inputVector.y, 0f);
        if (moveDir.sqrMagnitude > 0f)
        {
            Vector3 moveCheck = transform.position + moveDir * (moveSpeed * Time.deltaTime);
            if (MapCollider.Instance.Check(moveCheck))
            {
                transform.position = moveCheck;
            }
        }
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
        if (hp <= 0)
        {
            Debug.Log("Die");
        }
    }

}
