using UnityEngine;
using System.Collections;

public class InstanciateTreeScript : MonoBehaviour {

    public GameObject Tree;
    public int Number;

    void Start()
    {
        Application.runInBackground = true;
    }

	// Use this for initialization
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int num = -Number / 2; num < Number / 2; num++)
            {
                GameObject it = (GameObject)Instantiate(Tree, new Vector3(num*4, 0.0f, 0.0f), Quaternion.identity);
                //it.GetComponent<TreeInfosClass>().TreeGrowTimeSecond = Random.Range(3, 10);
                it.GetComponent<TreeScript>().InitializeTree();
            }
        }
	}
}
