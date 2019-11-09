using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        images = new [] {image1, image2, image3, image4, image5};
        names = new[] {"", "", "", "", ""};
        sprites = new[] { defaultIcon, defaultIcon, defaultIcon, defaultIcon, defaultIcon};

        UpdateImages();
    }

    // Update is called once per frame
    private void Update()
    {
       this.slider.value -= alcoholUse * Time.deltaTime;
    }

    public void AddAlcohol(float amount)
    {
       this.slider.value += amount;
    }

    public void AddItem(Sprite im, string name)
    {
       //insert in first possible slot
       for (var i = 0; i < 5; i++)
       {
          if (names[i] != "") continue;
          sprites[i] = im;
          names[i] = name;
          break;
       }
       
       _itemcount++;
       UpdateImages();
    }

    private void UpdateImages()
    {
       for (var i = 0; i < 5; i++)
       {
          images[i].sprite = sprites[i];
       }
    }

    public bool HasItem(string name)
    {
       return names.Contains(name);
    }

    public bool RemoveItem(string name)
    {
       var result = false;
       for (var i = 0; i < 5; i++)
       {
          if (names[i] != name) continue;
          names[i] = "";
          sprites[i] = defaultIcon;
          result = true;
          break;
       }

       UpdateImages();
       return result;
    }

    public float alcoholUse;

    private Image[] images;
    private string[] names;
    private Sprite[] sprites;

    private int _itemcount;

    public Image image1;
    public Image image2;
    public Image image3;
    public Image image4;
    public Image image5;
    public Sprite defaultIcon;
    public Slider slider;

    public Inventory(int itemcount)
    {
       _itemcount = itemcount;
    }
}
