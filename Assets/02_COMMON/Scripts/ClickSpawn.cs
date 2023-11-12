using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;

    public float duration;
    public float targetScale;



    private IEnumerator TweenObject(GameObject orb)
    {
        float timer = 0;
        orb.transform.localScale = Vector3.zero;
        while (timer < duration)    
        {
            
            orb.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one*targetScale, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(orb);
       

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
        // Ekran üzerinde bir dokunma algılandı mı kontrol et
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Dokunulan noktada objeyi oluştur
                GameObject orb = Instantiate(objectToSpawn, hit.point, Quaternion.identity);
                StartCoroutine(TweenObject(orb));
                StartCoroutine(Dissolve(hit.collider.gameObject.GetComponent<MeshRenderer>()));


            }


        }
    }
}
