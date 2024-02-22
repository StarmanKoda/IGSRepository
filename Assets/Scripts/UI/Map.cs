using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

public class Map : MonoBehaviour
{
    public Button[] buttons;
    public TextMeshProUGUI[] texts;
    public Image[] fades;
    public float fadeTime;
    int subMap = 0;

    public GameObject[] zones;
    public float[] scaleMods;

    Vector2 mapPos;
    Vector2 zonePos;

    public GameObject backBtn;

    // Start is called before the first frame update
    void Start()
    {

    }

    float timeAtLastFrame = 0f;
    float timeAtCurrentFrame = 0f;
    float deltaTime = 0f;

    // Update is called once per frame
    void Update()
    {
        timeAtCurrentFrame = Time.realtimeSinceStartup;
        deltaTime = timeAtCurrentFrame - timeAtLastFrame;
        timeAtLastFrame = timeAtCurrentFrame;
    }

    public void activateBack()
    {
        backBtn.SetActive(true);
    }

    public void openMap(int map)
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        subMap = map;
        mapPos = fades[map].transform.position;

        zonePos = zones[subMap].transform.position;
        zones[subMap].transform.position = mapPos;

        StartCoroutine(fade(false, 0));

        StartCoroutine(toggleMap(fades[subMap].transform, true, true));
        StartCoroutine(toggleMap(zones[subMap].transform, false, true));

        //Invoke("activateBack", fadeTime * 2);
    }

    public void closeMap()
    {
        backBtn.SetActive(false);

        StartCoroutine(toggleMap(fades[subMap].transform, true, false));
        StartCoroutine(toggleMap(zones[subMap].transform, false, false));

        StartCoroutine(fade(true, fadeTime));
    }

    IEnumerator toggleMap(Transform obj, bool doFade, bool grow)
    {
        //yield return new WaitForSeconds(fadeTime);

        if (grow && !doFade)
        {
            obj.gameObject.SetActive(true);
            obj.localScale = Vector2.zero;
            obj.position = mapPos;
        }

        float time = 0;
        //float alpha = 0;
        //Vector2 dist = mapPos;
        float scaleMod = 1;
        Vector2 diff = Vector2.zero;
        //Color col = fades[subMap].color;
        //Color textCol = texts[subMap].color;

        if (doFade)
        {
            scaleMod = scaleMods[subMap];
            diff = Vector2.one;
        }

        while (time < fadeTime +.01f)
        {
            //if (doFade)
            //{
            //    if (grow)
            //    {
            //        alpha = Mathf.Lerp(1, 0, time / fadeTime);
            //    }
            //    else
            //    {
            //        alpha = Mathf.Lerp(0, 1, time / fadeTime);
            //    }
            //    col.a = alpha;
            //    textCol.a = alpha;
            //    fades[subMap].color = col;
            //    texts[subMap].color = textCol;
            //}


            if (grow)
            {
                obj.position = Vector2.Lerp(mapPos, zonePos, time / fadeTime);
                obj.localScale = Vector2.Lerp(Vector2.zero + diff, Vector2.one * scaleMod, time / fadeTime);
            }
            else
            {
                obj.position = Vector2.Lerp(zonePos, mapPos, time / fadeTime);
                obj.localScale = Vector2.Lerp(Vector2.one * scaleMod, Vector2.zero + diff, time / fadeTime);
            }


            time += deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (!grow && !doFade)
        {
            obj.position = zonePos;
            obj.gameObject.SetActive(false);

            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
        if (grow && !doFade)
        {
            backBtn.SetActive(true);
        }
    }

    IEnumerator fade(bool fadeIn, float delay)
    {
        //yield return new WaitForSeconds(delay);

        float time = 0;
        while (time < fadeTime +.01f)
        {
            float alpha = 0;
            if (fadeIn)
            {
                alpha = Mathf.Lerp(0, 1, time / fadeTime);
            }
            else
            {
                alpha = Mathf.Lerp(1, 0, time / fadeTime);
            }

            for (int i = 0; i < fades.Length; i++)
            {
                //if (i == subMap)
                //{
                //    continue;
                //}
                Color col = fades[i].color;
                col.a = alpha;
                fades[i].color = col;
            }

            for (int i = 0; i < texts.Length; i++)
            {
                //if (i == subMap)
                //{
                //    continue;
                //}

                Color col = texts[i].color;
                col.a = alpha;
                texts[i].color = col;
            }

            time += deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
