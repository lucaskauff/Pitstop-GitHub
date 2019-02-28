using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBehaviour2 : MonoBehaviour
{
    public LineRenderer liana;
    public GameObject player;
    public GameObject[] hookpoints;
    public bool pointSelect;

    int layer_mask;

    [SerializeField]
    float lifeInSeconds;

    public int damageDealing = 1;

    //public EnemyHealthManager bossHealth;
    public CrystalController crys;
    public ScannableObjectBehaviour scannableObjBeh;
    private bool living;

    private void Awake()
    {
        liana = this.GetComponent<LineRenderer>();
        layer_mask = LayerMask.GetMask("Bear");
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {
            pointSelect = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            pointSelect = false;
            for (int x = 0; x < 3; x++)
            {
                if (hookpoints[x] == null)
                {
                    liana.positionCount = x;
                }                       
            }

            if (hookpoints[1] == null)
            {
                return;
            }

            else
            {
                liana.enabled = true;
                liana.SetPosition(0, hookpoints[0].transform.position);
                liana.SetPosition(1, hookpoints[1].transform.position);
                liana.SetPosition(2, hookpoints[2].transform.position);
                liana.SetPosition(3, hookpoints[3].transform.position);
            }
        }

    }
}
