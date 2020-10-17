using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFloatValue : MonoBehaviour
{
    public FloatVariable val;
    public string Postfix;
    public string Prefix;
    private Text txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = $"{Prefix}{val.RuntimeValue:##.}{Postfix}";
    }
}
