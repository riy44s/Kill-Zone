using SimpleInputNamespace;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform FirePoint;
    [SerializeField] GameObject BulletPrefab;
    public ParticleSystem shootingParticle;

    private Animator anim;

    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineSize = 30;
    public Text ammoText;
    public float reloadTime = 2f;
    private bool isReloading;

    public PlayerController player;
    bool isShooting;
    private void Start()
    {
        anim=GetComponent<Animator>();
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if(currentAmmo == 0 && magazineSize == 0)
        {
            return;
        }

        if (isReloading)
            return;

        if (isShooting && SimpleInput.GetButton("ShootButton"))
        {
            Debug.Log("Button Worked");
            Shoot();
            ShootParticle();
            AudioManeger.Instance.PlaySFX("GunShot");
            isShooting = false;
        }
        /*else if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            ShootParticle();
            AudioManeger.Instance.PlaySFX("GunShot");
        }*/

        if (currentAmmo == 0 && magazineSize>0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }
    public void Shootbutton()
    {
        isShooting = SimpleInput.GetButton("ShootButton");
    }
    public void ShootParticle()
    {
        Instantiate(shootingParticle, FirePoint.position, FirePoint.rotation);
        
    }

    public void Shoot()
    {
        currentAmmo--;
        ammoText.text = currentAmmo + " / " + magazineSize;
        Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }
    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("isReloading", true);
        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("isReloading", false);
        if (magazineSize >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineSize -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineSize;
            magazineSize = 0;
        }
        isReloading = false;
    }
    public void AmmoRefill()
    {
        int ammoToAdd = maxAmmo - currentAmmo; 
        currentAmmo = maxAmmo;
        if (magazineSize >= ammoToAdd)
        {
            magazineSize -= ammoToAdd;
        }
        else
        {
            magazineSize = 0;
        }
        ammoText.text = currentAmmo + " / " + magazineSize;
    }
    public void playerRespawnAmmoRefill()
    {
        currentAmmo = maxAmmo;
        magazineSize=30;
        ammoText.text = currentAmmo + " / " + magazineSize;
    }
}
