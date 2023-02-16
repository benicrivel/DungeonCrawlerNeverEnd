using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Vari�veis p�blicas
    public GameObject projectilePrefab;
    public GameObject target;
       
    public float fireRate = 0.5f;
    public int magazineSize = 10;
    public float reloadTime = 2f;

    private float nextFireTime = 0f;  

   [SerializeField] private int currentAmmo;

    private bool isReloading = false;

    private SpriteRenderer sprite;
    // Inicializa��o
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentAmmo = magazineSize;
    }

    // Atualiza��o
    private void Update()
    {
        Aim();
        Shoot();
        Reload();
    }

    void Aim() 
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 offSet = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        float angleGun = Mathf.Atan2(offSet.y, offSet.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angleGun);

        sprite.flipY = (mousePos.x < screenPoint.x);
    }
    void Shoot()
    {
        // Verifica se o bot�o esquerdo do mouse est� pressionado e se a cad�ncia de tiro foi atingida
        if (Input.GetMouseButton(0) && Time.time > nextFireTime && !isReloading && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;

            // Cria o proj�til e o dispara na dire��o do mouse
            Instantiate(projectilePrefab, target.transform.position, target.transform.rotation);                   

            currentAmmo--;     
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && (currentAmmo <= 0))
        {   
            isReloading = true;
            StartCoroutine("TimeToShootAgain");
        }
    }

    IEnumerator TimeToShootAgain() 
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
        Debug.Log("Recarregado");
    }
}