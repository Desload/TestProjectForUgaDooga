using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class LettersGenerator : MonoBehaviour
{
    [SerializeField] private int maxCountOnSide;

    [SerializeField] private LatterInfo prefabLetter;
    [SerializeField] private CellInfo cellPrefab;
    
    private List<CellInfo> points;
    private List<RectTransform> lattersTransforms;

    private RectTransform lettersParent;
    private GridLayout gridLayout;
    private InputReader inputReader;

    private char[] letters = Enumerable.Range('A', 'Z' - 'A' + 1).Select(c => (char)c).ToArray();

    private void Awake()
    {
        lettersParent = GetComponent<RectTransform>();
        inputReader = GetComponent<InputReader>();
        gridLayout = GetComponent<GridLayout>();
    }

    public void GenerateCombination()
    {
        float xSize;
        float ySize;

        float endValueForLatters;

        if (inputReader.GetValueFromFields(out int widthCount, out int heightCount))
        {
            if (widthCount == 0 || heightCount == 0)
            {
                print("Вы ввели значение равное нулю");
                return;
            }
            if (widthCount > maxCountOnSide || heightCount > maxCountOnSide)
            {
                print("Вы ввели значение большее максимума");
                return;
            }

            xSize = lettersParent.sizeDelta.x / widthCount;
            ySize = lettersParent.sizeDelta.y / heightCount;

            endValueForLatters = xSize > ySize ?  ySize : xSize;
        }
        else
        {
            print("Некоректно введены значения");
            return;
        }


        Vector2 center = new Vector2(lettersParent.sizeDelta.x / 2, lettersParent.sizeDelta.y / 2);
        Vector2 topLeftPoint = new Vector2(((widthCount - 1) * -endValueForLatters) / 2, ((heightCount - 1) * endValueForLatters) / 2);

        for (int w = 0; w < widthCount; w++)
        {
            for(int h = 0; h < heightCount; h++)
            {
                CellInfo tempCell = Instantiate(cellPrefab, transform);
                tempCell.CellTransform.localPosition = topLeftPoint + new Vector2(endValueForLatters * w, -endValueForLatters * h);
                points.Add(tempCell);
            }
        }

        for (int count = 0; count < widthCount * heightCount; count++)
        {
            LatterInfo tempLatter;
            tempLatter = Instantiate(prefabLetter, transform);
            tempLatter.SetLetter(letters[Random.Range(0, letters.Length)].ToString());
            //tempLatter.LatterTransform.localPosition = 
        }

        print($"{widthCount} + {heightCount}");
    }
}
