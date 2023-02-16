using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    public float speed = 10f;
    public float lifeTime = 2f;  
   
    private void Start()
    {    
        Destroy(gameObject, lifeTime);
    }   
    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir o projétil
        Destroy(gameObject);
    }
}