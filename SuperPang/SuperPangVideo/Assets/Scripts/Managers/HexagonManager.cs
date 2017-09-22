using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonManager : MonoBehaviour
{
    public bool spliting;
    public static HexagonManager hm;
    public List<GameObject> hexagons = new List<GameObject>();

    void Awake()
    {
        if (hm == null)
            hm = this;
        else if (hm != this)
            Destroy(gameObject);
    }

    void Start()
    {
        hexagons.AddRange(GameObject.FindGameObjectsWithTag("Hexagon"));
    }

    public void StartGame()
    {
        foreach (GameObject item in hexagons)
        {
            if (hexagons.IndexOf(item) % 2 == 0)
                item.GetComponent<Hexagon>().right = true;
            else
                item.GetComponent<Hexagon>().right = false;

            item.GetComponent<Hexagon>().Startforce(item);
        }
    }

    public void LoseGame()
    {
        foreach (GameObject item in hexagons)
            item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void DestroyHexagon(GameObject hexagon, GameObject hex1, GameObject hex2)
    {
        LastHexagon(hexagon);
        hexagons.Add(hex1);
        hexagons.Add(hex2);
    }

    public void LastHexagon(GameObject hexagon)
    {
        hexagons.Remove(hexagon);
        Destroy(hexagon);
    }

    public void Dynamite(int maxNumberHexagons)
    {
        StartCoroutine(DynamiteH(maxNumberHexagons));
    }

    public void SlowTime()
    {
        StartCoroutine(TimeSlow());
    }

    List<GameObject> FindHexagons(int typeOfHexagon)
    {
        List<GameObject> hexagonsToDestroy = new List<GameObject>();

        for (int i = 0; i < hexagons.Count; i++)
        {
            if (hexagons[i].GetComponent<Hexagon>().name.Contains(typeOfHexagon.ToString()) && hexagons[i] != null)
                hexagonsToDestroy.Add(hexagons[i]);
        }

        return hexagonsToDestroy;
    }

    void ReloadList()
    {
        hexagons.Clear();
        hexagons.AddRange(GameObject.FindGameObjectsWithTag("Hexagon"));
    }

    IEnumerator DynamiteH(int maxNumberHexagons)
    {
        ReloadList();
        spliting = true;
        int numberToFind = 1;

        while (numberToFind < maxNumberHexagons)
        {
            foreach (GameObject item in FindHexagons(numberToFind))
            {
                item.GetComponent<Hexagon>().Split();
                Destroy(item);
            }

            yield return new WaitForSeconds(.4f);
            ReloadList();
            numberToFind++;
        }

        spliting = false;
    }

    IEnumerator TimeSlow()
    {
        float time = 0;

        foreach (GameObject item in hexagons)
        {
            if (item != null)
                item.GetComponent<Hexagon>().SlowHexagon();
        }

        while (time < 3)
        {
            time += Time.deltaTime;
            yield return null;
        }

        foreach (GameObject item in hexagons)
        {
            if (item != null)
                item.GetComponent<Hexagon>().NormalSpeedHexagon();
        }
    }

    public int AleatoryNumber()
    {
        return Random.Range(0, 3);
    }
}
