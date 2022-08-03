using UnityEngine;
public class Utils
{
    //Название сохранения для PlayerPrefs
    public static readonly string coinsSave = "coins";
    public static readonly string selectedSkinSave = "selectedSkin";
    public static T GetRandInArray<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
	public static bool GetBool(string key) //Дополнение к PlayerPrefs
	{
		return (PlayerPrefs.GetInt(key, 0) == 1);
	}

	public static bool GetBool(string key, bool defaultValue)
	{
		return (PlayerPrefs.GetInt(key, (defaultValue ? 1 : 0)) == 1);
	}

	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, (value ? 1 : 0));
	}
}