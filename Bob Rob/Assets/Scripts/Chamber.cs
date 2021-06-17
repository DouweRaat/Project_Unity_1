using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour {
    public SpriteRenderer spriteRenderer;
    public Sprite six;
    public Sprite five;
    public Sprite four;
    public Sprite three;
    public Sprite two;
    public Sprite one;
    public Sprite zero;

    public void changeChamber(int bulletAmount) {
        switch (bulletAmount)
        {
            case 6:
                spriteRenderer.sprite = six;
                break;
            case 5:
                spriteRenderer.sprite = five;
                break;
            case 4:
                spriteRenderer.sprite = four;
                break;
            case 3:
                spriteRenderer.sprite = three;
                break;
            case 2:
                spriteRenderer.sprite = two;
                break;
            case 1:
                spriteRenderer.sprite = one;
                break;
            case 0:
                spriteRenderer.sprite = zero;
                break;
        }
    }
}
