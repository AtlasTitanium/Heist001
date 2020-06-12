using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtComputerData : MonoBehaviour
{
    public Text title, description, author, value;
    public Image[] images;

    public void ChangeData(string _title, string _description, string _author, int _value) {
        title.text = _title;
        description.text = _description;
        author.text = _author;
        value.text = _value + " Mil.";

        if(_value <= 1) {
            foreach(Image i in images) {
                i.color = Color.red;
            }
        }
    }
}
