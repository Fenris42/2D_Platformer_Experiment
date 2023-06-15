using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction

{
    public string ActionName => "Use";

    [field: SerializeField] public AudioClip actionSFX {get; private set;}

    public bool PerformAction(GameObject character)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }
        return true;
    }

    [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }

    public AudioClip actionSFX { get; }

    bool PerformAction(GameObject character);

}

[Serializable]
public class ModifierData
{
    public CharacterStatModiferSO statModifier;
    public int value;

}
