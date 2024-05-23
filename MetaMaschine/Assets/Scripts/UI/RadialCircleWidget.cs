using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class RadialCirclesWidget : MonoBehaviour
{
    public TextMeshProUGUI widgetName;
    public TextMeshProUGUI widgetValue;
    public TextMeshProUGUI widgetMin;
    public TextMeshProUGUI widgetMax;

    public RectTransform rectTransform;
    public GameObject circlePrefab; // Prefab with an Image component for the circle
    public Color colorA = Color.red; // Starting color
    public Color colorB = Color.blue; // Ending color
    public int totalCircles = 30; // Total number of circles
    public float radius = 90f; // Radius for circle placement
    private GameObject[] circles;
    private Tween[] tweens;

    void Awake()
    {
        if (!rectTransform)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        InitializeCircles();
    }

    void InitializeCircles()
    {
        circles = new GameObject[totalCircles];
        tweens = new Tween[totalCircles];
        float angleStep = 320f / (totalCircles - 1); // Spread the circles over 320 degrees
        float angleOffset = -110f; // Shift the starting point by 20 degrees counterclockwise

        for (int i = 0; i < totalCircles; i++)
        {
            float angle = angleOffset - angleStep * i; // Counterclockwise
            Vector2 position = new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            GameObject circle = Instantiate(circlePrefab, rectTransform);
            circle.GetComponent<RectTransform>().anchoredPosition = position;
            circle.SetActive(false); // Initially deactivate all circles
            circles[i] = circle;
        }
    }

    public void SetValue(string name, float rawValue, float? minValue, float? maxValue, int decimals, string unit)
    {
        float value;
        float minValueParse = 0;
        float maxValueParse = 0;
        // Clamp value between 0 and 1
        if (minValue == null || maxValue == null)
        {
            value = 0;
        }
        else
        {
            minValueParse = (float)minValue;
            maxValueParse = (float)maxValue;
            value = Mathf.Clamp01(Mathf.InverseLerp(minValueParse, maxValueParse, rawValue));


        }

        // Calculate number of circles to display
        int circlesToDisplay = Mathf.FloorToInt(totalCircles * value);

        for (int i = 0; i < totalCircles; i++)
        {
            if (tweens[i] != null)
            {
                tweens[i].Kill(); // Stop ongoing animation
            }

            if (i < circlesToDisplay)
            {
                circles[i].SetActive(true);
                circles[i].GetComponent<Image>().color = Color.Lerp(colorA, colorB, (float)i / (totalCircles - 1));
                circles[i].GetComponent<Image>().color = new Color(circles[i].GetComponent<Image>().color.r, circles[i].GetComponent<Image>().color.g, circles[i].GetComponent<Image>().color.b, 0);
                tweens[i] = circles[i].GetComponent<Image>().DOFade(1, 0.25f).SetDelay(i * 0.05f); // Fade in with delay
            }
            else
            {
                tweens[i] = circles[i].GetComponent<Image>().DOFade(0, 0.25f).SetDelay(i * 0.05f).OnComplete(() => circles[i].SetActive(false)); // Fade out with delay and deactivate
            }
        }

        decimals = Mathf.Max(0, decimals);

        string formattedValue = rawValue.ToString($"F{decimals}");
        formattedValue = $"{formattedValue} {unit}";

        string formattedMin = minValueParse.ToString($"F{decimals}");
        formattedMin = $"{formattedMin} {unit}";

        string formattedMax = maxValueParse.ToString($"F{decimals}");
        formattedMin = $"{formattedMin} {unit}";

        widgetName.text = name;
        widgetValue.text = formattedValue;
        widgetMin.text = formattedMin;
        widgetMax.text = formattedMax;
    }
}
