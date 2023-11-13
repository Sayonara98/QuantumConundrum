using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    private ILauncher enemyLauncher;

    [SerializeField]
    private float fireRate = 2f;

    private bool canShoot = true;

    private void Start()
    {
        canShoot = true;
        enemyLauncher = GetComponent<ILauncher>();
    }

    public void Shoot(Vector2 dir)
    {
        if (canShoot)
        {
            enemyLauncher.Launch(dir);
            StartCoroutine(Reload(fireRate));
        }
    }

    private IEnumerator Reload(float fireRate)
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

}
