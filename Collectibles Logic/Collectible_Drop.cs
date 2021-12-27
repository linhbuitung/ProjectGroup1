using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Drop : MonoBehaviour
{
    public GameObject StartPoint;
    public GameObject EndPoint;
    public GameObject[] Dropables;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Drop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Drop()
    {
        while (true)
        {
            float i = Random.Range(StartPoint.transform.position.x, EndPoint.transform.position.x);
            int j = Random.Range(0, Dropables.Length);

            GameObject Drop = Instantiate(Dropables[j], new Vector3(i, 4, 1), Quaternion.identity);
            Destroy(Drop, 4);
            yield return new WaitForSeconds(5);           
        }
        
    }
}
