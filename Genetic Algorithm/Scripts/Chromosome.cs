using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour {

    public static Chromosome instance;

    // gene for colour
    public float r, g, b;
    // gene for height
    public Transform get_height;
    public float personHeight;
    // record how long each person has lived for
    public float life = 0f;

    MeshRenderer mRenderer;
    Collider col;

    private bool hasDied = false;   

	// Use this for initialization
	void Start () {
        InitialiseVariables();
    }

    void InitialiseVariables()
    {
        get_height = this.GetComponent<Transform>();
        get_height.localScale = new Vector3(1, personHeight, 1);
        col = GetComponent<Collider>();
        mRenderer = GetComponent<MeshRenderer>();
        mRenderer.material.color = new Color(r, g, b);
    }

    private void OnMouseDown()
    {
        hasDied = true;
        life = PopulationManager.elapsed;
        Debug.Log("The person has lived for : " + life + " Seconds");
        mRenderer.enabled = false;
        col.enabled = false;
    }

    void CreateInstance()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
