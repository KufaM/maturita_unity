using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float damage = 1f;
    public float range = 1000f;
    private float shootDelay = 0.6f;
    private bool canShoot = true;
    public LineRenderer lineRenderer;
    public Transform visualOrigin;
    private Vector3 lineStart;
    private Vector3 lineEnd;
    private bool isBulletTraveling = false;
    public AudioClip gunshotSound;

    private void Start()
    {
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Shoot();

            AudioSource.PlayClipAtPoint(gunshotSound, transform.position, 0.1f);
            canShoot = false;
            Invoke("ResetShoot", shootDelay);
        }

        // Move the start point of the line towards the end point
        if (isBulletTraveling)
        {
            lineStart = Vector3.MoveTowards(lineStart, lineEnd, range * Time.deltaTime);
            lineRenderer.SetPosition(0, lineStart);

            if (lineStart == lineEnd)
            {
                lineRenderer.enabled = false;
                isBulletTraveling = false;
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {

            ZombieSpawn targetZombie = hit.transform.GetComponent<ZombieSpawn>();

            if (targetZombie != null)
            {
                targetZombie.TakeDamage(damage);
            }

            lineStart = visualOrigin.position;
            lineEnd = hit.point;

            lineRenderer.SetPosition(0, lineStart);
            lineRenderer.SetPosition(1, lineEnd);
            lineRenderer.enabled = true;
            isBulletTraveling = true;
        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}