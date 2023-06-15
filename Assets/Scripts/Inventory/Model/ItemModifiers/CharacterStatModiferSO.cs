using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatModiferSO : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, int val);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
