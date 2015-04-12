/**
* Author:    Janos Kiss
* Email:	 elte.kissjanos@gmail.com
* Created:   12.04.2015
* textureFromSprite function is copied from the following link
* http://answers.unity3d.com/questions/651984/convert-sprite-image-to-texture.html
**/
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
public class BitmapFont2D : MonoBehaviour {

	public struct FontTextureElement
	{
		public Texture2D texture;
		public string name;
	}
	public Sprite FontImage;
	public FontTextureElement[] FontTextureArray;
	public string fileLocation;
	public string text;
	public Sprite[] FontSpriteArray;
	void Start () 
	{

		FontSpriteArray =  Resources.LoadAll (fileLocation,typeof(Sprite)).Cast<Sprite>().ToArray();
		FontTextureArray = new FontTextureElement[FontSpriteArray.Length];
		for (int i = 0; i < FontSpriteArray.Length; i++) 
		{
			FontTextureArray[i].texture = textureFromSprite(FontSpriteArray[i]);
			FontTextureArray[i].name = FontSpriteArray[i].name;
		}
	}
	public static Texture2D textureFromSprite(Sprite sprite)
	{
		if(sprite.rect.width != sprite.texture.width)
		{
			Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
			Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
			                                             (int)sprite.textureRect.y, 
			                                             (int)sprite.textureRect.width, 
			                                             (int)sprite.textureRect.height );
			newText.SetPixels(newColors);
			newText.Apply();
			return newText;
		}else
			return sprite.texture;
	}
	void drawText()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(this.transform.position);
		int x = (int)screenPos.x;
		int y = (int)(Screen.height - screenPos.y);
		for (int i = 0; i<text.Length ; i++) 
		{
			for (int j = 0;j<FontTextureArray.Length ;j++)
			{
				if (FontTextureArray[j].name == text[i].ToString())
				{
					Rect rect = new Rect (x,y,FontTextureArray[j].texture.width,FontTextureArray[j].texture.height);
					GUI.DrawTexture(rect,FontTextureArray[j].texture);
					x += FontTextureArray[j].texture.width;
				}
			}
		}
	}
	void OnGUI()
	{
		drawText ();
	}
	public void setText(string text)
	{
		this.text = text;
	}
	void Update () 
	{

	}
}
