using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/* Population Manager for Genetic Algorithm, in this case, it will be breed out 1/3 of the individuals, but feel free
 * to change the function GenerateNewPopulation(), although the "DNA" info it will be collected upon those 
 * individuals who have lived longer.
*/

public class PopulationManager : MonoBehaviour {

    public GameObject person;
    public int populationSize = 20;
    List<GameObject> populationList = new List<GameObject>();
    public static float elapsed = 0;
    private int trialTime = 10;
    private int generation = 1;

    GUIStyle g_Style = new GUIStyle();



    // Use this for initialization
    void Start() {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-15, 15), Random.Range(-5f, 5f), 0);
            GameObject temp = Instantiate(person, spawnPos, Quaternion.identity);
            temp.GetComponent<Chromosome>().personHeight = (int)Random.Range(1.0f, 5.0f);
            temp.GetComponent<Chromosome>().r = Random.Range(0.0f, 1.0f);
            temp.GetComponent<Chromosome>().g = Random.Range(0.0f, 1.0f);
            temp.GetComponent<Chromosome>().b = Random.Range(0.0f, 1.0f);
            populationList.Add(temp);
        }
    }

    // Generic Algorithm Swaping DNA information
    GameObject Generate(GameObject mom, GameObject dad)
    {
        Vector3 spawnPos = new Vector3(Random.Range(-15, 15), Random.Range(-5f, 5f), 0);
        GameObject offSpring = Instantiate(person, spawnPos, Quaternion.identity);
        Chromosome DNA_mom = mom.GetComponent<Chromosome>();
        Chromosome DNA_dad = dad.GetComponent<Chromosome>();

        /*Swapping the DNA data and please do not Combine or Mix them.
         * If you combine them, your individuals can end up having two different eye colours :D
        // Swap parents DNA Colours and height at 50% at the time.
        */
        offSpring.GetComponent<Chromosome>().r = Random.Range(0, 10) < 5 ? DNA_mom.r : DNA_dad.r;
        offSpring.GetComponent<Chromosome>().g = Random.Range(0, 10) < 5 ? DNA_mom.r : DNA_dad.r;
        offSpring.GetComponent<Chromosome>().b = Random.Range(0, 10) < 5 ? DNA_mom.r : DNA_dad.r;
        offSpring.GetComponent<Chromosome>().personHeight = (int)Random.Range(0, 10) < 5 ? DNA_mom.personHeight : DNA_dad.personHeight;
        return offSpring;
    }

    public void GenerateNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        // sorted the list out by how long each individual has lived for.
        List<GameObject> sortedList = populationList.OrderBy(order => order.GetComponent<Chromosome>().life).ToList();
        populationList.Clear();

        // loop through the list one third down
	// change the value to divide for by 2, if you want to breed out half of the list
        for (int i = (int)(sortedList.Count / 3.0f) - 1; i < sortedList.Count - 1; i++)
        {
            populationList.Add(Generate(sortedList[i], sortedList[i + 1]));
            populationList.Add(Generate(sortedList[i + 1], sortedList[i]));
        }

        // Going through the sorted list and Destroy the previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        //Update the generation count
        generation++;
    }
  

    // Update is called once per frame
    void Update ()
    {
        elapsed += Time.deltaTime;
        if(elapsed > trialTime)
        {
            GenerateNewPopulation();
            elapsed = 0;
        }
	}

    private void OnGUI()
    {
        g_Style.fontSize = 46;
        g_Style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, g_Style);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, g_Style);
    }
}
