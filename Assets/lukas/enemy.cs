using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    LayerMask EnemyMask;


    [Header("Enemy Stats")]
    [SerializeField] private bool invincibility = false;
    [SerializeField] private float health = 20, maxHealth = 20;
    [SerializeField] private float enemyDamage = 5f;

    [Header("Movement Settings")]
    [SerializeField] private float speed;

    [SerializeField] private float jumpHeigth = 5f, gravity = 9.14f;



    [Header("Debug Info")]
    [SerializeField] private Vector3 moveDir;
    [SerializeField] private float rotY, rotX;



    void Start()
    {
        EnemyMask = LayerMask.GetMask("Enemy");
        CheckForObjectVisibility("Player");
    }
private void OnTriggerEnter(Collider collision)
{
    Debug.Log("Attacked!");
}

    void CheckForObjectVisibility(string objTag){
        GameObject[] player = GameObject.FindGameObjectsWithTag(objTag);
        print(player.Length);
        if(player.Length == 1){
            RaycastHit rayHit;
            Physics.Raycast(transform.position + (transform.position - player[0].transform.position).normalized*1.5f, player[0].transform.position, out rayHit, 10.0f);
            if(rayHit.collider != null && rayHit.collider == player[0])
                print("test");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
