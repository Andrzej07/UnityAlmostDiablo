using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile")]
public class ProjectileAbility : Ability
{
    [Header("Projectile")]
    public GameObject projectile;
    public float velocity;
    public float timeToLive;
    public Effect[] projectileExpireEffects;
    public bool destroyProjectileOnHit;

    private Transform projectiles;
    private GameController gameController
    {
        get
        {
            if (_gameController == null)
                _gameController = GameObject.Find("Game").GetComponent<GameController>();
            return _gameController;
        }
    }
    private GameController _gameController;

    void Start()
    {
        projectiles = GameObject.Find("Projectiles").transform;
    }

    protected override bool TriggerEffect(GameObject source, GameObject target)
    {
        Vector3 targetPosition;
        if (target == null)
            targetPosition = GetMousePosition();
        else
            targetPosition = target.transform.position;
        if (targetPosition == Vector3.zero)
            return false;
        GameObject projectileInstance = Instantiate(projectile, projectiles);
        ProjectileController projectileController = projectileInstance.GetComponent<ProjectileController>();
        Vector3 dir = (targetPosition - source.transform.position);
        dir.y = 0;
        dir.Normalize();
        projectileController.direction = dir;
        Vector3 projStartPos = source.transform.position + dir;
        projStartPos.y += 0.75f;
        projectileInstance.transform.position = projStartPos;
        projectileController.timeToLive = timeToLive;
        projectileController.velocity = velocity;
        projectileController.onProjectileTriggerEnter += delegate (Collider other)
        {
            Debug.Log("Projectile hit: " + other.gameObject.name);
            if (gameController.AreHostile(source, other.gameObject))
            {
                foreach (Effect effect in effects)
                    effect.ApplyEffect(source, other.gameObject);
                if (destroyProjectileOnHit)
                    Destroy(projectileInstance);
            }
            else if (other.gameObject.tag == "Terrain")
            {
                Destroy(projectileInstance);
            }

        };
        projectileController.onProjectileEndOfLife += delegate ()
        {
            Debug.Log("Projectile end of life");
            foreach (Effect effect in projectileExpireEffects)
                effect.ApplyEffect(source, projectileInstance);
        };
        return true;
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1 << 9))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    protected override void GetInPosition(GameObject source, GameObject target)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsInPosition(GameObject source, GameObject target)
    {
        return true;
    }
}
