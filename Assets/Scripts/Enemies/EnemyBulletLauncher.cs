using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLauncher : MonoBehaviour, ILauncher
{

    [SerializeField]
    private EnemyBullet bulletPrefab;

    public void Launch(Vector2 dir)
    {
        EnemyBullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.Launch(dir);
    }
}
