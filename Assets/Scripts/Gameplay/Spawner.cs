using System.Collections;
using UnityEngine;
using UnityEditor;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float minSpawnTime = 0.8f, maxSpawnTime = 1.8f;
    public bool canSpawn = false;
    private void Start() => Spawn();
    public void Spawn() => StartCoroutine(SpawnCoroutine());
    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        var objectToSpawn = GetRandInArray(objectsToSpawn);
        var spawnPoint = transform.position;
        spawnPoint = GetRandInArray(spawnPoints).position;
        if (canSpawn) Instantiate(objectToSpawn, spawnPoint, objectToSpawn.transform.rotation);
        StartCoroutine(SpawnCoroutine());
    }
    public static T GetRandInArray<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
[CustomEditor(typeof(Spawner))]
[CanEditMultipleObjects]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var canSpawn = serializedObject.FindProperty("canSpawn");
        EditorGUILayout.PropertyField(canSpawn, new GUIContent("Спавнит?"));
        if (canSpawn.boolValue)
        {
            var objectsToSpawn = serializedObject.FindProperty("objectsToSpawn");
            EditorGUILayout.PropertyField(objectsToSpawn, new GUIContent("Объекты для спавна"));

            var spawnPoints = serializedObject.FindProperty("spawnPoints");
            EditorGUILayout.PropertyField(spawnPoints, new GUIContent("Точки спавна"));
         
            var minSpawnTime = serializedObject.FindProperty("minSpawnTime");
            EditorGUILayout.PropertyField(minSpawnTime, new GUIContent("Мин. время спавна"));

            var maxSpawnTime = serializedObject.FindProperty("maxSpawnTime");
            EditorGUILayout.PropertyField(maxSpawnTime, new GUIContent("Макс. время спавна"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}