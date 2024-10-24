using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TextFieldsPrinter : MonoBehaviour
{
    void Start()
    {
        PrintTextFields();
    }

    void PrintTextFields()
    {
        Text[] textFields = FindObjectsOfType<Text>();
        List<string> textList = new List<string>();

        foreach (Text textField in textFields)
        {
            textList.Add(string.Format("\"{0}\": \"{1}\"", textField.name, textField.text));
        }

        string jsonText = "{\n" + string.Join(",\n", textList.ToArray()) + "\n}";
        Debug.Log(jsonText);
    }
}