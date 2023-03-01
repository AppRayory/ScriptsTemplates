

public static class EnumsHandler
{
    public enum CharacterID : byte
    {
        c1,
        c2,
        c3,
        c4,
        c5,
        c6,
        c7,
        c8,
        c9,
        c10,
        c11,
        c12,
        c13,
        c14,
        c15
    }

    public enum EnemyID { BoldGuy = 0, Girl = 1, SkinHead = 2 }

    public enum CharacterStats : byte
    {
        Health,
        Speed,
        Damage,
        Defense,
        Range,
        Dodge,
        Evasion,
        Luck,
        AttackSpeedMelee,
        AttackSpeedRange,
        PickupRadius,
        CritChanceMelee,
        CritChanceRange,
        CritDamageMelee,
        CritDamageRange
    }

    public enum ScenesDesignation : byte { MainMenu = 0, GamePlay = 1 }
    public enum EnemyStats : byte { Health = 0, Speed = 1, Damage = 2, Defense = 3, }

    public enum WeaponRarity : byte { None = 0, Common = 1, Uncommon = 2, Rare = 3, Epic = 4, Legendary = 5 }
    public enum WeaponType : byte { Hummer = 0, Knife = 1, Sword = 2, Shotgun = 3}

}
