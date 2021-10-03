using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Entity
{
    Player player;
    public GameObject body;
    public List<Transform> movePoints;
    protected Transform movingTo;
    public bool armAttacking = false;
    public bool armWoundUp = false;
    public bool armReturn = false;
    public float armSpeed = 5f;
    public float armWindupSpeed = 1f;
    public float armRetractSpeed = 5f;
    public int armIndex = 0;
    public int armDamage = 1;
    public List<GameObject> arms;
    public List<Transform> armHomes;
    public List<Transform> armWindups;
    public List<Transform> armTargets;
    public List<GameObject> shotPrefabs;
    public Transform shotSpawn;
    public int lastAttack = 0;
    public float nextShot = 0f;
    public float shotCooldown = 0f;
    public bool doubleShot = false;

    protected override void Start()
    {
        base.Start();
        this.player = GameObject.FindWithTag("Player").GetComponent<Player>();
        this.selectMoveTarget();
    }

    protected virtual void Update()
    {
        if (Time.time > this.wakeUp) {
            if (!(Vector2.Distance(this.body.transform.position, this.movingTo.position) <= 0.1)) {
                this.Move();
            } else {
                this.selectMoveTarget();
            }

            if (Time.time > this.nextShot) {
                if (this.armAttacking) {
                    if (!this.armWoundUp) {
                        // wind up the arm
                        this.moveArm(this.arms[this.armIndex].transform, this.armWindups[this.armIndex], this.armWindupSpeed);
                        if (Vector2.Distance(this.arms[this.armIndex].transform.position, this.armWindups[this.armIndex].position) <= 0.1) {
                            this.armWoundUp = true;
                        }
                    } else if (this.armReturn) {
                        // bring arms back home
                        this.moveArm(this.arms[this.armIndex].transform, this.armHomes[this.armIndex], this.armRetractSpeed);
                        if (Vector2.Distance(this.arms[this.armIndex].transform.position, this.armHomes[this.armIndex].position) <= 0.1) {
                            this.endArmAttack();
                        }
                    } else {
                        // move arm forward
                        this.moveArm(this.arms[this.armIndex].transform, this.armTargets[this.armIndex], this.armSpeed);
                        if (Vector2.Distance(this.arms[this.armIndex].transform.position, this.armTargets[this.armIndex].position) <= 0.1) {
                            this.retractArms();
                        }
                    }
                } else {
                    if ((!this.doubleShot && this.lastAttack == 0) || Random.Range(0, 2) == 1) {
                        this.armAttack();
                        this.lastAttack = 1;
                    } else {
                        GameObject shotSpawn = Instantiate(this.shotPrefabs[Random.Range(0, this.shotPrefabs.Count)], this.shotSpawn.position, Quaternion.identity);
                        Destroy(shotSpawn, 20f);
                        this.lastAttack = 0;
                        this.nextShot = Time.time + this.shotCooldown;
                    }
                }
            }
        }
    }

    protected override void FixedUpdate()
    {

    }

    protected override void Move()
    {
        this.body.transform.position = Vector2.MoveTowards(this.body.transform.position, this.movingTo.position, this.runSpeed * Time.deltaTime);
    }

    void selectMoveTarget()
    {
        this.movingTo = this.movePoints[Random.Range(0, this.movePoints.Count)];
    }

    void armAttack()
    {
        this.armAttacking = true;
        this.armIndex = Random.Range(0, this.arms.Count);
    }

    void moveArm(Transform arm, Transform target, float speed)
    {
        arm.position = Vector2.MoveTowards(arm.position, target.position, speed * Time.deltaTime);
    }

    public void retractArms()
    {
        this.armReturn = true;
    }

    public void endArmAttack()
    {
        this.armReturn = false;
        this.armWoundUp = false;
        this.armAttacking = false;
    }

    public void armHitPlayer()
    {
        if (this.armAttacking && this.armWoundUp && !this.armReturn) {
            this.player.damage(this.armDamage);
        }
    }
}
