using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "CollectableData", menuName = "Collectable/NewCollectable")]
public class CollectableData : ScriptableObject
{
    [SerializeField] private CollectType type;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float spawnTime;
    
    public string Name => name;
    public float SpawnTime => spawnTime;
    public CollectType Type => type;
    public Sprite Sprite => sprite;
}
