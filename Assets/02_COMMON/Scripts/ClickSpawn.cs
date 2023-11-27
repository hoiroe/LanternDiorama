using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ClickSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float duration;
    public float targetScale;
    public Material mainMaterial;
    public float distanceOffset;

    private void Start()
    {
        mainMaterial.SetFloat("_DissolveDistance", -0.2f);
    }

    private void OnDestroy()
    {
        mainMaterial.SetFloat("_DissolveDistance", 1.15f);
    }


    
    private IEnumerator Dissolve(MeshRenderer mr)
    {
        float timer = 0;
        float value = mr.materials[1].GetFloat("_DissolveDistance");

        if (value > -0.2f) { }
        else 
        {
            while (timer < 1)
            {

                ;
                value = Mathf.LerpUnclamped(-0.2f, 1.15f, timer);
                mr.materials[1].SetFloat("_DissolveDistance", value);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        

       


    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
           
                

                Vector3 orbPosition = hit.point;
                Vector3 direction = (Camera.main.transform.position - orbPosition).normalized;
                orbPosition += direction * distanceOffset;
                

                GameObject orb = Instantiate(objectToSpawn, orbPosition, Quaternion.identity);
                
                StartCoroutine(Dissolve(hit.collider.gameObject.GetComponent<MeshRenderer>()));


            }


        }
    }
}
