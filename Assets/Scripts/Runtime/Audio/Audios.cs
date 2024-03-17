using System.Diagnostics;

/// <summary>
/// 储存所有音频的引用
/// </summary>
public static class Audios
{
    public static AudioBase menuBgm = new AudioBase(new AudioType("Audio/8Bit Music Album - 051321/MenuBgm"), 1, true,
        true, AudioPlayerManager.Instance.Music());

    public static AudioBase gameidleBgm = new AudioBase(new AudioType("Audio/8Bit Music Album - 051321/GameIdleBgm"), 1,
        true, true, AudioPlayerManager.Instance.Music());

    public static AudioBase buttonClick =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Liquid/Click"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase buttonEnter =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Crispy/Enter"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase cardEnter =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Plastic/CardEnter"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase cardClick =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Plastic/CardClick"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase weaponWave_1 = new AudioBase(new AudioType("Audio/Weapon/weaponWave_1"), 1, false, false,
        AudioPlayerManager.Instance.SFX());

    public static AudioBase bigMouthSpawn_1 = new AudioBase(new AudioType("Audio/Fantasy Sfx/Mp3/Dragon_Growl_00"), 1,
        false, false, AudioPlayerManager.Instance.SFX());

    public static AudioBase babySpawn_1 = new AudioBase(new AudioType("Audio/Fantasy Sfx/Mp3/Goblin_01"), 1, false,
        false, AudioPlayerManager.Instance.SFX());

    public static AudioBase interactive_1 =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Liquid/active_1"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase explosion_1 = new AudioBase(new AudioType("Audio/Self/explosion_1"), 1, false, false,
        AudioPlayerManager.Instance.SFX());

    public static AudioBase enemyDead_1 =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Crispy/enemydead_1"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase enemyDead_2 =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Liquid/enemydead_2"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase enemyDead_3 =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Pop/enemydead_3"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase enemyDead_4 =
        new AudioBase(new AudioType("Audio/Free UI Click Sound Effects Pack/AUDIO/Pop/enemydead_4"), 1, false, false,
            AudioPlayerManager.Instance.SFX());

    public static AudioBase coinCollected = new AudioBase(new AudioType("Audio/Item/CoinCollected"), 1, false, false,
        AudioPlayerManager.Instance.SFX());

    public static AudioBase weaponWave_2 = new AudioBase(new AudioType("Audio/Weapon/weaponWave_2"), 1, false, false,
        AudioPlayerManager.Instance.SFX());

    public static AudioBase weaponWave_3 = new AudioBase(new AudioType("Audio/Weapon/weaponWave_3"), 1, false, false,
        AudioPlayerManager.Instance.SFX());

    public static AudioBase weaponWave_4 = new AudioBase(new AudioType("Audio/Weapon/weaponWave_4"), 1, false, false,
        AudioPlayerManager.Instance.SFX());

    public static AudioBase weaponWave_5 = new AudioBase(new AudioType("Audio/Weapon/weaponWave_5"), 1, false, false,
        AudioPlayerManager.Instance.SFX());
}