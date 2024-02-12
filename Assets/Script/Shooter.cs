using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] Bullet bulletPref;
    Vector3 firePoint;
    GameObject projectilesRoot;

    private AudioManager _audioManager;

    // Start is called before the first frame update
    void Start()
    {
        FindFirePointCoords();

        if (bulletPref == null)
        {
            Debug.LogError("Please, add some bullet prefab to shooter");
        }

        InitializeProjectilesRootGameObj();

        _audioManager = AudioManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Bullet bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);
        // TODO assign bullet to Defender who shoot. Problem whis cactus, he dont have BODY object.
        //bullet.transform.parent = gameObject.transform;

        bullet.transform.parent = projectilesRoot.transform;
    }

    void FindFirePointCoords()
    {
        Transform firePointTransform = transform.Find("FirePoint");
        if (firePointTransform == null)
        {
            Debug.LogError("Please, add child obj to shooter, and name it FirePoint");
        }
        else
        {
            firePoint = firePointTransform.transform.position;
        }
    }
    void InitializeProjectilesRootGameObj()
    {
        projectilesRoot = GameObject.Find(GameState.PROJECTILE_PARRENT_GAME_OBJ);
        if (!projectilesRoot)
        {
            projectilesRoot = new GameObject(GameState.PROJECTILE_PARRENT_GAME_OBJ);
        }
    }
}
