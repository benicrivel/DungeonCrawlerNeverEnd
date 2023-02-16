using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Variáveis públicas
    public GameObject projectilePrefab;
    public GameObject target;
       
    public float fireRate = 0.5f;
    public int magazineSize = 10;
    public float reloadTime = 2f;

    private float nextFireTime = 0f;  

   [SerializeField] private int currentAmmo;

    private bool isReloading = false;

    private SpriteRenderer sprite;
    // Inicialização
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        currentAmmo = magazineSize;
    }

    // Atualização
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
        // Verifica se o botão esquerdo do mouse está pressionado e se a cadência de tiro foi atingida
        if (Input.GetMouseButton(0) && Time.time > nextFireTime && !isReloading && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;

            // Cria o projétil e o dispara na direção do mouse
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