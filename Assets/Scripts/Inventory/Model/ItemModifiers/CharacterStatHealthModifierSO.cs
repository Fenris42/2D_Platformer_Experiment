using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModiferSO
{
    public override void AffectCharacter(GameObject character, int val)
    {
        Player player = character.GetComponent<Player>();
        player.GainMana(val);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
