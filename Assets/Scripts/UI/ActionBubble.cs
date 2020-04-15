using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays some comic book action text (POW!, SMASH!) on 
/// </summary>
public class ActionBubble : MonoBehaviour
{
    private const float _timeToExist = 0.5f;
    private float _startTime;
    private Camera parentCam;

    //options for a word
    private static string[] _wordOptionList = { "Pow", "Smash", "Smack", "Ka-Pow", "Bang", "Crack", "Biff" };

    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
        parentCam = GameObject.Find("CityCamera").GetComponent<Camera>();
        if (parentCam == null)
        {
            Debug.LogError("Could not find CityCamera!");
        }

        //set up the thing
        //set random hue for the bubble
        float randomHue = Random.Range(0.0f, 1.0f);

        //set random text rotation
        int rotation = Random.Range(0, 361);

        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

        if (sr != null) //Idk how this would happen but it would be bad
        {
            sr.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            sr.color = Color.HSVToRGB(randomHue, 1, 1);
        } else
        {
            Debug.LogError("Sprite Renderer was null");
        }

        //set a random text hue that will contrast
        float randomTextHue = (randomHue + 0.5f) % 1;

        Text text = GetComponentInChildren<Text>();

        if (text != null) //again, not sure how this would happen but it would be bad
        {
            text.color = Color.HSVToRGB(randomTextHue, 1, 1);
            text.text = (_wordOptionList[Random.Range(0, _wordOptionList.Length)] + "!").ToUpper();
        } else
        {
            Debug.LogError("Text could not be found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState.IsPaused)
        {
            return;
        }

        //if it has existed long enough, delete it
        if (Time.time >= _startTime + _timeToExist)
        {
            Destroy(gameObject);
        }
        
    }

}
