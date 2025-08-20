using AEAssist.CombatRoutine;

namespace Millusion.Define;

public class SpellsDefine
{
    public const uint Sprint = 3;

    public static HashSet<uint> AbilityAsGcdSet = new()
    {
        StandardStep,
        TechnicalStep,
        TenCombo,
        ChiCombo,
        JinCombo,
        TcjFumaShurikenTen,
        TcjFumaShurikenChi,
        TcjFumaShurikenJin,
        TcjKaton,
        TcjRaiton,
        TcjHyoton,
        TcjHuton,
        TcjDoton,
        TcjSuiton
    };

    // DPS Role

    #region DPS Role

    public const uint SecondWind = 7541;
    public const uint Bloodbath = 7542;
    public const uint TrueNorth = 7546;
    public const uint ArmsLength = 7548;
    public const uint Feint = 7549;
    public const uint HeadGraze = 7551;
    public const uint FootGraze = 7553;
    public const uint LegGraze = 7554;
    public const uint Peloton = 7557;
    public const uint LegSweep = 7863;
    public const uint Potion = 846;

    #endregion

    // Magic Role

    #region Magic Role

    public const uint Surecast = 7559;
    public const uint Addle = 7560;
    public const uint Swiftcast = 7561;
    public const uint LucidDreaming = 7562;
    public const uint Esuna = 7568;
    public const uint Rescue = 7571;
    public const uint Repose = 16560;

    #endregion

    // Tank Role

    #region Tank Role

    public const uint Rampart = 7531;
    public const uint Provoke = 7533;
    public const uint Reprisal = 7535;
    public const uint Shirk = 7537;
    public const uint Interject = 7538;
    public const uint LowBlow = 7540;

    public static uint GetTankStateOpenId(Jobs jobs)
    {
        switch (jobs)
        {
            case Jobs.Paladin:
                return 28;
            case Jobs.Warrior:
                return Defiance;
            case Jobs.DarkKnight:
                return 3629;
            case Jobs.Gunbreaker:
                return 16142;
        }

        return 0;
    }

    public static uint GetTankStateAuraId(Jobs jobs)
    {
        switch (jobs)
        {
            case Jobs.Paladin:
                return 79;
            case Jobs.Warrior:
                return AurasDefine.Defiance;
            case Jobs.DarkKnight:
                return 743;
            case Jobs.Gunbreaker:
                return 1833;
        }

        return 0;
    }


    public static uint GetTankStateCloseId(Jobs jobs)
    {
        switch (jobs)
        {
            case Jobs.Paladin:
                return 32065;
            case Jobs.Warrior:
                return 32066;
            case Jobs.DarkKnight:
                return 32067;
            case Jobs.Gunbreaker:
                return 32068;
        }

        return 0;
    }

    #endregion

    // ACN

    #region ACN

    public const uint Ruin = 163;
    public const uint SummonCarbuncle = 25798;
    public const uint RadiantAegis = 25799;
    public const uint Aethercharge = 25800;
    public const uint SummonRuby = 25802;
    public const uint Gemshine = 25883;
    public const uint RubyRuin = 25808;
    public const uint TopazRuin = 25809;
    public const uint EmeraldRuin = 25810;
    public const uint RubyRuinIi = 25811;
    public const uint TopazRuinIi = 25812;
    public const uint EmeraldRuinIi = 25813;
    public const uint Fester = 181;
    public const uint EnergyDrain = 16508;
    public const uint Resurrection = 173;
    public const uint SummonTopaz = 25803;
    public const uint SummonEmerald = 25804;
    public const uint Outburst = 16511;
    public const uint PreciousBrilliance = 25884;
    public const uint RubyOutburst = 25814;
    public const uint TopazOutburst = 25815;
    public const uint EmeraldOutburst = 25816;
    public const uint Ruin2 = 172;
    public const uint SmnPhysick = 16230;

    #endregion

    // AST

    #region AST

    public const uint Draw = 3590;
    public const uint AstralDraw = 37017;
    public const uint UmbralDraw = 37018;
    public const uint Redraw = 3593;
    public const uint Benefic = 3594;
    public const uint AspectedBenefic = 3595;
    public const uint Malefic = 3596;
    public const uint Malefic2 = 3598;
    public const uint Combust = 3599;
    public const uint Helios = 3600;
    public const uint HeliosConjunction = 37030;
    public const uint AspectedHelios = 3601;
    public const uint Ascend = 3603;
    public const uint Lightspeed = 3606;
    public const uint Combust2 = 3608;
    public const uint Benefic2 = 3610;
    public const uint Synastry = 3612;
    public const uint CollectiveUnconscious = 3613;
    public const uint EssentialDignity = 3614;
    public const uint Gravity = 3615;
    public const uint Balance = 4401;
    public const uint Arrow = 4402;
    public const uint Spear = 4403;
    public const uint Bole = 4404;
    public const uint Ewer = 4405;
    public const uint Spire = 4406;
    public const uint EarthlyStar = 7439;
    public const uint Malefic3 = 7442;
    public const uint MinorArcana = 37022;
    public const uint LordofCrowns = 7444;
    public const uint LadyofCrowns = 7445;
    public const uint StellarDetonation = 8324;
    public const uint Undraw = 9629;
    public const uint Divination = 16552;
    public const uint CelestialOpposition = 16553;
    public const uint Combust3 = 16554;
    public const uint Malefic4 = 16555;
    public const uint CelestialIntersection = 16556;
    public const uint Horoscope = 16557;
    public const uint HoroscopeHelios = 16558;
    public const uint NeutralSect = 16559;
    public const uint SunSign = 37031;
    public const uint Play = 17055;
    public const uint Astrodyne = 25870;
    public const uint CrownPlay = 25869;
    public const uint Exaltation = 25873;
    public const uint Macrocosmos = 25874;
    public const uint Microcosmos = 25875;
    public const uint FallMalefic = 25871;
    public const uint GravityIi = 25872;
    public const uint Oracle = 37029;
    public const uint Play1 = 37019;
    public const uint Play2 = 37020;
    public const uint Play3 = 37021;

    #endregion

    // BLM

    #region BLM

    public const uint Fire = 141;
    public const uint Blizzard = 142;
    public const uint Thunder = 144;
    public const uint Fire2 = 147;
    public const uint Transpose = 149;
    public const uint Fire3 = 152;
    public const uint Thunder3 = 153;
    public const uint Blizzard3 = 154;
    public const uint AetherialManipulation = 155;
    public const uint Scathe = 156;
    public const uint ManaFont = 158;
    public const uint Flare = 162;
    public const uint Freeze = 159;
    public const uint LeyLines = 3573;
    public const uint Sharpcast = 3574;
    public const uint Enochian = 3575;
    public const uint Blizzard4 = 3576;
    public const uint Fire4 = 3577;
    public const uint Thunder2 = 7447;
    public const uint Thunder4 = 7420;
    public const uint Triplecast = 7421;
    public const uint Foul = 7422;
    public const uint Despair = 16505;
    public const uint UmbralSoul = 16506;
    public const uint Xenoglossy = 16507;
    public const uint Blizzard2 = 25793;
    public const uint HighFireIi = 25794;
    public const uint HighBlizzardIi = 25795;
    public const uint Amplifier = 25796;

    public const uint Paradox = 25797;

    //魔纹步
    public const uint BTL = 7419;

    //魔罩
    public const uint Manaward = 157;

    #endregion

    // BRD

    #region BRD

    //SingleTarget

    public const uint HeavyShot = 97;
    public const uint StraightShot = 98;
    public const uint Bloodletter = 110;
    public const uint PitchPerfect = 7404;
    public const uint EmpyrealArrow = 3558;
    public const uint Sidewinder = 3562;
    public const uint RefulgentArrow = 7409;
    public const uint BurstShot = 16495;

    //AoE

    public const uint QuickNock = 106;
    public const uint RainofDeath = 117;
    public const uint Shadowbite = 16494;
    public const uint ApexArrow = 16496;
    public const uint Ladonsbite = 25783;
    public const uint BlastArrow = 25784;

    //Dot

    public const uint VenomousBite = 100;
    public const uint Windbite = 113;
    public const uint IronJaws = 3560; //Not a DoT but will refresh both
    public const uint CausticBite = 7406;
    public const uint Stormbite = 7407;

    //Cooldowns - unsure about naming this :/

    public const uint RagingStrikes = 101;
    public const uint Barrage = 107;
    public const uint BattleVoice = 118;
    public const uint RadiantFinale = 25785;

    //Songs

    public const uint MagesBallad = 114;
    public const uint ArmysPaeon = 116;
    public const uint TheWanderersMinuet = 3559;

    //Utility/Movement

    public const uint RepellingShot = 112;
    public const uint TheWardensPaean = 3561;
    public const uint Troubadour = 7405;
    public const uint NaturesMinne = 7408;

    #endregion

    // DNC

    #region DNC

    public const uint Cascade = 15989;
    public const uint Fountain = 15990;
    public const uint ReverseCascade = 15991;
    public const uint Fountainfall = 15992;
    public const uint Windmill = 15993;
    public const uint Bladeshower = 15994;
    public const uint RisingWindmill = 15995;
    public const uint Bloodshower = 15996;
    public const uint StandardStep = 15997;
    public const uint Emboite = 15999;
    public const uint Entrechat = 16000;
    public const uint Jete = 16001;
    public const uint Pirouette = 16002;
    public const uint StandardFinish = 16003;
    public const uint SaberDance = 16005;
    public const uint ClosedPosition = 16006;
    public const uint FanDance = 16007;
    public const uint FanDance2 = 16008;
    public const uint FanDance3 = 16009;
    public const uint EnAvant = 16010;
    public const uint Devilment = 16011;
    public const uint ShieldSamba = 16012;
    public const uint Flourish = 16013;
    public const uint Improvisation = 16014;
    public const uint CuringWaltz = 16015;
    public const uint SingleStandardFinish = 16191;
    public const uint DoubleStandardFinish = 16192;
    public const uint Ending = 18073;
    public const uint TechnicalStep = 15998;
    public const uint SingleTechnicalFinish = 16193;
    public const uint DoubleTechnicalFinish = 16194;
    public const uint TripleTechnicalFinish = 16195;
    public const uint QuadrupleTechnicalFinish = 16196;
    public const uint FanDanceIv = 25791;
    public const uint StarfallDance = 25792;
    public const uint Tillana = 25790;

    #endregion

    // DRG

    #region DRG

    public const uint TrueThrust = 75;
    public const uint VorpalThrust = 78;
    public const uint LifeSurge = 83;
    public const uint DoomSpike = 86;
    public const uint Disembowel = 87;
    public const uint ChaosThrust = 88;
    public const uint Jump = 92;
    public const uint SpineshatterDive = 95;
    public const uint DragonfireDive = 96;
    public const uint BloodoftheDragon = 3553;
    public const uint FangAndClaw = 3554;
    public const uint Geirskogul = 3555;
    public const uint WheelingThrust = 3556;
    public const uint BattleLitany = 3557;
    public const uint MirageDive = 7399;
    public const uint Nastrond = 7400;
    public const uint LanceCharge = 85;
    public const uint FullThrust = 84;
    public const uint SonicThrust = 7397;
    public const uint DragonSight = 7398;
    public const uint CoerthanTorment = 16477;
    public const uint HighJump = 16478;
    public const uint RaidenThrust = 16479;
    public const uint Stardiver = 16480;
    public const uint HeavensThrust = 25771;
    public const uint ChaoticSpring = 25772;
    public const uint WyrmwindThrust = 25773;
    public const uint DraconianFury = 25770;

    #endregion

    // DRK

    #region DRK

    public const uint HardSlash = 3617;
    public const uint Unleash = 3621;
    public const uint SyphonStrike = 3623;
    public const uint Unmend = 3624;
    public const uint BloodWeapon = 3625;
    public const uint Grit = 3629;
    public const uint Souleater = 3632;
    public const uint DarkMind = 3634;
    public const uint ShadowWall = 3636;
    public const uint LivingDead = 3638;
    public const uint SaltedEarth = 3639;
    public const uint Plunge = 3640;
    public const uint AbyssalDrain = 3641;
    public const uint CarveandSpit = 3643;
    public const uint Delirium = 7390;
    public const uint Quietus = 7391;
    public const uint Bloodspiller = 7392;
    public const uint TheBlackestNight = 7393;
    public const uint FloodofDarkness = 16466;
    public const uint EdgeofDarkness = 16467;
    public const uint StalwartSoul = 16468;
    public const uint FloodofShadow = 16469;
    public const uint EdgeofShadow = 16470;
    public const uint DarkMissionary = 16471;
    public const uint LivingShadow = 16472;
    public const uint Oblation = 25754;
    public const uint Shadowbringer = 25757;
    public const uint SaltandDarkness = 25755;

    #endregion

    // GNB

    #region GNB

    public const uint KeenEdge = 16137;
    public const uint NoMercy = 16138;
    public const uint BrutalShell = 16139;
    public const uint Camouflage = 16140;
    public const uint DemonSlice = 16141;
    public const uint RoyalGuard = 16142;
    public const uint LightningShot = 16143;
    public const uint DangerZone = 16144;
    public const uint SolidBarrel = 16145;
    public const uint GnashingFang = 16146;
    public const uint SavageClaw = 16147;
    public const uint Nebula = 16148;
    public const uint DemonSlaughter = 16149;
    public const uint WickedTalon = 16150;
    public const uint Aurora = 16151;
    public const uint Superbolide = 16152;
    public const uint SonicBreak = 16153;
    public const uint RoughDivide = 16154;
    public const uint Continuation = 16155;
    public const uint JugularRip = 16156;
    public const uint AbdomenTear = 16157;
    public const uint EyeGouge = 16158;
    public const uint BowShock = 16159;
    public const uint HeartofLight = 16160;
    public const uint HeartofStone = 16161;
    public const uint BurstStrike = 16162;
    public const uint FatedCircle = 16163;
    public const uint Bloodfest = 16164;
    public const uint BlastingZone = 16165;
    public const uint HeartOfCorundum = 25758;
    public const uint DoubleDown = 25760;
    public const uint Hypervelocity = 25759;

    #endregion

    // MCH

    #region MCH

    public const uint RookAutoturret = 2864;
    public const uint SplitShot = 2866;
    public const uint SlugShot = 2868;
    public const uint SpreadShot = 2870;
    public const uint HotShot = 2872;
    public const uint CleanShot = 2873;
    public const uint GaussRound = 2874;
    public const uint Reassemble = 2876;
    public const uint Wildfire = 2878;
    public const uint Dismantle = 2887;
    public const uint Ricochet = 2890;
    public const uint HeatBlast = 7410;
    public const uint HeatedSplitShot = 7411;
    public const uint HeatedSlugShot = 7412;
    public const uint HeatedCleanShot = 7413;
    public const uint BarrelStabilizer = 7414;
    public const uint RookOverdrive = 7415;
    public const uint Flamethrower = 7418;
    public const uint AutoCrossbow = 16497;
    public const uint Drill = 16498;
    public const uint Bioblaster = 16499;
    public const uint AirAnchor = 16500;
    public const uint AutomationQueen = 16501;
    public const uint QueenOverdrive = 16502;
    public const uint Detonator = 16766;
    public const uint Tactician = 16889;
    public const uint Hypercharge = 17209;
    public const uint Scattergun = 25786;
    public const uint ChainSaw = 25788;

    public const uint PvpDrill = 17749;
    public const uint PvpRicochet = 17753;
    public const uint PvpGaussRound = 18933;
    public const uint PvpHypercharge = 17754;
    public const uint PvpWildfire = 8855;
    public const uint PvpAirAnchor = 17750;
    public const uint PvpSpreadShot = 18932;

    #endregion

    // MNK

    #region MNK

    public const uint ArmOfTheDestroyer = 62;
    public const uint Bootshine = 53;
    public const uint TrueStrike = 54;

    public const uint SnapPunch = 56;
    public const uint SixSidedStar = 16476;

    //public const uint FistsOfEarth = 60;
    public const uint TwinSnakes = 61;
    public const uint Demolish = 66;

    public const uint Rockbreaker = 70;
    public const uint SteelPeak = 25761;
    public const uint HowlingFist = 25763;

    //public const uint FistsOfWind = 73;
    //public const uint ShoulderTackle = 71;
    //public const uint FistsOfFire = 63;
    public const uint Anatman = 16475;
    public const uint Mantra = 65;
    public const uint Thunderclap = 25762;
    public const uint PerfectBalance = 69;
    public const uint DragonKick = 74;
    public const uint TheForbiddenChakra = 3547;
    public const uint ElixirField = 3545;
    public const uint FlintStrike = 25882;
    public const uint RisingPhoenix = 25768;
    public const uint CelestialRevolution = 25765;
    public const uint RiddleofEarth = 7394;
    public const uint RiddleofFire = 7395;
    public const uint RiddleofWind = 25766;
    public const uint Brotherhood = 7396;
    public const uint FormShift = 4262;
    public const uint FourPointFury = 16473;
    public const uint Enlightenment = 16474;
    public const uint TornadoKick = 3543;
    public const uint PhantomRush = 25769;
    public const uint MasterfulBlitz = 25764;
    public const uint ShadowOfTheDestroyer = 25767;
    public const uint FinalHeaven = 202;

    #endregion

    // NIN

    #region NIN

    public const uint SpinningEdge = 2240;
    public const uint ShadeShift = 2241;
    public const uint GustSlash = 2242;
    public const uint Hide = 2245;
    public const uint Assassinate = 2246;
    public const uint ThrowingDagger = 2247;
    public const uint Mug = 2248;
    public const uint DeathBlossom = 2254;
    public const uint AeolianEdge = 2255;
    public const uint ShadowFang = 2257;
    public const uint TrickAttack = 2258;
    public const uint Ten = 2259;
    public const uint Ninjutsu = 2260;
    public const uint Chi = 2261;
    public const uint Shukuchi = 2262;
    public const uint Jin = 2263;
    public const uint Kassatsu = 2264;
    public const uint FumaShuriken = 2265;
    public const uint Katon = 2266;
    public const uint Raiton = 2267;
    public const uint Hyoton = 2268;
    public const uint Huton = 2269;
    public const uint Doton = 2270;
    public const uint Suiton = 2271;
    public const uint RabbitMedium = 2272;
    public const uint ArmorCrush = 3563;
    public const uint DreamWithinaDream = 3566;
    public const uint HellfrogMedium = 7401;
    public const uint Bhavacakra = 7402;
    public const uint TenChiJin = 7403;
    public const uint HakkeMujinsatsu = 16488;
    public const uint Meisui = 16489;
    public const uint GokaMekkyaku = 16491;
    public const uint HyoshoRanryu = 16492;
    public const uint Bunshin = 16493;
    public const uint PhantomKamaitachi = 25774;
    public const uint ForkedRaiju = 25777;
    public const uint FleetingRaiju = 25778;
    public const uint Huraijin = 25876;
    public const uint TenCombo = 18805;
    public const uint ChiCombo = 18806;
    public const uint JinCombo = 18807;
    public const uint TcjFumaShurikenTen = 18873;
    public const uint TcjFumaShurikenChi = 18874;
    public const uint TcjFumaShurikenJin = 18875;
    public const uint TcjKaton = 18876;
    public const uint TcjRaiton = 18877;
    public const uint TcjHyoton = 18878;
    public const uint TcjHuton = 18879;
    public const uint TcjDoton = 18880;
    public const uint TcjSuiton = 18881;

    public const uint LimitBreak = 3;

    #endregion

    // PLD

    #region PLD

    public const uint Sentinel = 17;
    public const uint FightorFlight = 20;
    public const uint Cover = 27;
    public const uint HallowedGround = 30;
    public const uint DivineVeil = 3540;
    public const uint Sheltron = 3542;
    public const uint CircleofScorn = 23;
    public const uint SpiritsWithin = 29;
    public const uint IronWill = 28;
    public const uint Clemency = 3541;
    public const uint FastBlade = 9;
    public const uint RiotBlade = 15;
    public const uint ShieldBash = 16;
    public const uint RageofHalone = 21;
    public const uint ShieldLob = 24;
    public const uint GoringBlade = 3538;
    public const uint RoyalAuthority = 3539;
    public const uint TotalEclipse = 7381;
    public const uint Intervention = 7382;
    public const uint HolySpirit = 7384;
    public const uint Requiescat = 7383;
    public const uint Prominance = 16457;
    public const uint HolyCircle = 16458;
    public const uint Intervene = 16461;
    public const uint Atonement = 16460;
    public const uint Confiteor = 16459;
    public const uint HolySheltron = 25746;
    public const uint Expiacion = 25747;
    public const uint BladeOfFaith = 25748;
    public const uint BladeOfTruth = 25749;
    public const uint BladeOfValor = 25750;
    public const uint Bulwark = 22;
    public const uint PassageOfArms = 7385;

    #endregion

    // RDM

    #region RDM

    public const uint Jolt = 7503;
    public const uint Riposte = 7504;
    public const uint Verthunder = 7505;
    public const uint CorpsACorps = 7506;
    public const uint Veraero = 7507;
    public const uint Scatter = 7509;
    public const uint Verfire = 7510;
    public const uint Verstone = 7511;
    public const uint Zwerchhau = 7512;
    public const uint EnchantedMoulinet = 7530; //魔划圆斩
    public const uint Moulinet = 7513; //划圆斩
    public const uint Vercure = 7514;
    public const uint Displacement = 7515;
    public const uint Redoublement = 7516;
    public const uint Fleche = 7517;
    public const uint Acceleration = 7518;
    public const uint ContreSixte = 7519;
    public const uint Embolden = 7520;
    public const uint Manafication = 7521;
    public const uint Verraise = 7523;
    public const uint Jolt2 = 7524;
    public const uint Verflare = 7525;
    public const uint Verholy = 7526;
    public const uint EnchantedRedoublement = 7529;
    public const uint Verthunder2 = 16524;
    public const uint Veraero2 = 16525;
    public const uint Impact = 16526;
    public const uint Engagement = 16527;
    public const uint Reprise = 16528;
    public const uint Scorch = 16530;
    public const uint Resolution = 25858;
    public const uint VerthunderIii = 25855;
    public const uint VeraeroIii = 25856;
    public const uint MagickBarrier = 25857;

    public const uint EnchantedRiposte = 7527;
    public const uint EnchantedZwerchhau = 7528;
    public const uint Enchanted = 7529;

    #endregion

    // SAM

    #region SAM

    public const uint Hakaze = 7477;
    public const uint Shoha = 16487;
    public const uint Jinpu = 7478;
    public const uint Shifu = 7479;
    public const uint Yukikaze = 7480;
    public const uint Gekko = 7481;
    public const uint Kasha = 7482;
    public const uint Fuga = 7483;
    public const uint Mangetsu = 7484;
    public const uint Oka = 7485;
    public const uint Enpi = 7486;
    public const uint MidareSetsugekka = 7487;
    public const uint KaeshiSetsugekka = 16486;
    public const uint TenkaGoken = 7488;
    public const uint KaeshiGoken = 16485;
    public const uint Higanbana = 7489;
    public const uint KaeshiHiganbana = 16484;
    public const uint HissatsuShinten = 7490;
    public const uint HissatsuKyuten = 7491;
    public const uint HissatsuKaiten = 7494;
    public const uint Ikishoten = 16482;
    public const uint HissatsuGuren = 7496;
    public const uint HissatsuGyoten = 7492;
    public const uint HissatsuYaten = 7493;
    public const uint HissatsuSenei = 16481;
    public const uint Hagakure = 7495;
    public const uint TsubameGaeshi = 16483;
    public const uint Meditate = 7497;
    public const uint ThirdEye = 7498;

    public const uint MeikyoShisui = 7499;

    //public const uint HissatsuSeigan = 7501;
    public const uint Meditation = 3546;
    public const uint ShohaIi = 25779;
    public const uint Fuko = 25780;
    public const uint OgiNamikiri = 25781;
    public const uint KaeshiNamikiri = 25782;

    #endregion

    // SGE

    #region SGE

    public const uint Dosis = 24283;
    public const uint Diagnosis = 24284;
    public const uint Kardia = 24285;
    public const uint Prognosis = 24286;
    public const uint Egeiro = 24287;
    public const uint Physis = 24288;
    public const uint PhysisIi = 24302;
    public const uint Phlegma = 24289;
    public const uint PhlegmaIi = 24307;
    public const uint PhlegmaIii = 24313;
    public const uint Eukrasia = 24290;
    public const uint EukrasianDiagnosis = 24291;
    public const uint EukrasianPrognosis = 24292;
    public const uint EukrasianDosis = 24293;
    public const uint Soteria = 24294;
    public const uint Druochole = 24296;
    public const uint Dyskrasia = 24297;
    public const uint Kerachole = 24298;
    public const uint Ixochole = 24299;
    public const uint Zoe = 24300;
    public const uint Pepsis = 24301;
    public const uint Taurochole = 24303;
    public const uint Toxikon = 24304;
    public const uint ToxikonIi = 24316;
    public const uint Haima = 24305;
    public const uint DosisIi = 24306;
    public const uint EukrasianDosisIi = 24308;
    public const uint Rhizomata = 24309;
    public const uint Holos = 24310;
    public const uint Panhaima = 24311;
    public const uint DosisIii = 24312;
    public const uint EukrasianDosisIii = 24314;
    public const uint DyskrasiaIi = 24315;
    public const uint Krasis = 24317;
    public const uint Pneuma = 24318;
    public const uint Icarus = 24295;

    #endregion

    // SCH

    #region SCH

    //Dot-毒菌-猛毒菌-蛊毒法
    public const uint Bio = 17864;
    public const uint Bio2 = 17865;
    public const uint Biolysis = 16540;

    //GCD-毁灭-毁坏-气炎法-魔炎法-死炎法-极炎法
    public const uint SchRuin = 17869;
    public const uint SchRuin2 = 17870;
    public const uint SchBroil = 3584;
    public const uint SchBroil2 = 7435;
    public const uint SchBroil3 = 16541;
    public const uint SchBroil4 = 25865;

    //GCDAOE-破阵法-裂阵法
    public const uint ArtOfWar = 16539;
    public const uint ArtOfWarIi = 25866;

    //以太超流-能量吸收
    public const uint Aetherflow = 166;

    public const uint EnergyDrain2 = 167;

    //转化
    public const uint Dissipation = 3587;
    public const uint Adloquium = 185;
    public const uint Succor = 186;
    public const uint SacredSoil = 188;
    public const uint Lustrate = 189;
    public const uint Physick = 190;
    public const uint Indomitability = 3583;
    public const uint DeploymentTactics = 3585;
    public const uint EmergencyTactics = 3586;
    public const uint Excogitation = 7434;
    public const uint ChainStrategem = 7436;
    public const uint Aetherpact = 7437;
    public const uint DissolveUnion = 7869;
    public const uint WhisperingDawn = 16537;
    public const uint FeyIllumination = 16538;
    public const uint Recitation = 16542;
    public const uint FeyBlessing = 16543;
    public const uint SummonSeraph = 16545;
    public const uint Consolation = 16546;
    public const uint SummonEos = 17215;
    public const uint SummonSelene = 17216;


    public const uint Protraction = 25867;
    public const uint Expedient = 25868;


    public const uint Enkindle = 184;
    public const uint Galvanize = 297;

    #endregion

    // SMN

    #region SMN

    public const uint SummonIfrit = 25805;
    public const uint SummonTitan = 25806;
    public const uint Painflare = 3578;
    public const uint SummonGaruda = 25807;
    public const uint EnergySiphon = 16510;
    public const uint Ruin3 = 3579;
    public const uint RubyRuinIii = 25817;
    public const uint TopazRuinIii = 25818;
    public const uint EmeraldRuinIii = 25819;
    public const uint DreadwyrmTrance = 3581;
    public const uint AstralFlow = 25822;
    public const uint Ruin4 = 7426;
    public const uint SearingLight = 25801;
    public const uint SummonBahamut = 7427;
    public const uint EnkindleBahamut = 7429;
    public const uint TriDisaster = 25826;
    public const uint SummonIfrit2 = 25838;
    public const uint SummonTitan2 = 25839;
    public const uint SummonGaruda2 = 25840;
    public const uint AstralImpulse = 25820;
    public const uint AstralFlare = 25821;
    public const uint Deathflare = 3582;
    public const uint Wyrmwave = 7428;
    public const uint AkhMorn = 3010;
    public const uint RubyRite = 25823;
    public const uint TopazRite = 25824;
    public const uint EmeraldRite = 25825;
    public const uint FountainOfFire = 16514;
    public const uint BrandofPurgatory = 16515;
    public const uint SummonPhoenix = 25831;
    public const uint EverlastingFlight = 16517;
    public const uint Rekindle = 25830;
    public const uint ScarletFlame = 16508;
    public const uint EnkindlePhoenix = 16516;
    public const uint Revelation = 2951;
    public const uint RubyDisaster = 25827;
    public const uint TopazDisaster = 25828;
    public const uint EmeraldDisaster = 25829;
    public const uint RubyCatastrophe = 25832;
    public const uint TopazCatastrophe = 25833;
    public const uint EmeraldCatastrophe = 25834;
    public const uint CrimsonCyclone = 25835;
    public const uint CrimsonStrike = 25885;
    public const uint MountainBuster = 25836;
    public const uint Slipstream = 25837;

    #endregion

    // WAR

    #region WAR

    public const uint HeavySwing = 31;
    public const uint Maim = 37;
    public const uint Berserk = 38;
    public const uint ThrillofBattle = 40;
    public const uint Overpower = 41;
    public const uint StormsPath = 42;
    public const uint Holmgang = 43;
    public const uint Vengeance = 44;
    public const uint StormsEye = 45;
    public const uint Tomahawk = 46;
    public const uint Defiance = 48;
    public const uint InnerBeast = 49;
    public const uint SteelCyclone = 51;
    public const uint Infuriate = 52;
    public const uint FellCleave = 3549;
    public const uint Decimate = 3550;
    public const uint RawIntuition = 3551;
    public const uint Equilibrium = 3552;
    public const uint Onslaught = 7386;
    public const uint Upheaval = 7387;
    public const uint ShakeItOff = 7388;
    public const uint InnerRelease = 7389;
    public const uint MythrilTempest = 16462;
    public const uint InnerChaos = 16465;
    public const uint ChaoticCyclone = 16463;
    public const uint Bloodwhetting = 25751;
    public const uint Orogeny = 25752;
    public const uint PrimalRend = 25753;
    public const uint NascentFlash = 16464;

    #endregion

    // WHM

    #region WHM

    public const uint Stone = 119;
    public const uint Cure = 120;
    public const uint Aero = 121;
    public const uint Medica = 124;
    public const uint Raise = 125;
    public const uint Stone2 = 127;
    public const uint Cure3 = 131;
    public const uint Aero2 = 132;

    public const uint Medica2 = 133;

    //public const uint FluidAura = 134;
    public const uint Cure2 = 135;
    public const uint PresenceofMind = 136;
    public const uint Regen = 137;
    public const uint Holy = 139;
    public const uint Benediction = 140;
    public const uint Stone3 = 3568;
    public const uint Asylum = 3569;
    public const uint Tetragrammaton = 3570;
    public const uint Assize = 3571;
    public const uint ThinAir = 7430;
    public const uint Stone4 = 7431;
    public const uint DivineBenison = 7432;
    public const uint PlenaryIndulgence = 7433;
    public const uint AfflatusSolace = 16531;
    public const uint Dia = 16532;
    public const uint Glare = 16533;
    public const uint AfflatusRapture = 16534;
    public const uint AfflatusMisery = 16535;
    public const uint Temperance = 16536;
    public const uint GlareIii = 25859;
    public const uint HolyIii = 25860;
    public const uint Aquaveil = 25861;
    public const uint LiturgyOfTheBell = 25862;

    #endregion

    // BLU

    #region BLU

    public const uint Snort = 11383;
    public const uint FourTonzWeight = 11384;
    public const uint WaterCannon = 11385;
    public const uint SongOfTorment = 11386;
    public const uint HighVoltage = 11387;
    public const uint BadBreath = 11388;
    public const uint FlyingFrenzy = 11389;
    public const uint AquaBreath = 11390;
    public const uint Plaincracker = 11391;
    public const uint AcornBomb = 11392;
    public const uint Bristle = 11393;
    public const uint MindBlast = 11394;
    public const uint BloodDrain = 11395;
    public const uint BombToss = 11396;
    public const uint ThousandNeedles = 11397;
    public const uint DrillCannons = 11398;
    public const uint TheLook = 11399;
    public const uint SharpKnife = 11400;
    public const uint Loom = 11401;
    public const uint FlameThrower = 11402;
    public const uint Faze = 11403;
    public const uint Glower = 11404;
    public const uint Missile = 11405;
    public const uint WhiteWind = 11406;
    public const uint FinalSting = 11407;
    public const uint SelfDestruct = 11408;
    public const uint Transfusion = 11409;
    public const uint ToadOil = 11410;
    public const uint OffGuard = 11411;
    public const uint StickyTong = 11412;
    public const uint TailScrew = 11413;
    public const uint LevelFivePetrify = 11414;
    public const uint MoonFlute = 11415;
    public const uint Doom = 11416;
    public const uint MightyGuard = 11417;
    public const uint IceSpikes = 11418;
    public const uint TheRamVoice = 11419;
    public const uint TheDragonVoice = 11420;
    public const uint PeculiarLight = 11421;
    public const uint InkJet = 11422;
    public const uint FlyingSardine = 11423;
    public const uint DiamondBack = 11424;
    public const uint FireAngon = 11425;
    public const uint FeatherRain = 11426;
    public const uint Eruption = 11427;
    public const uint BluMountainBuster = 11428;
    public const uint ShockStrike = 11429;
    public const uint GlassDance = 11430;
    public const uint VeilOfTheWhorl = 11431;
    public const uint AlpineDraft = 18295;
    public const uint ProteanWave = 18296;
    public const uint Northerlies = 118297;
    public const uint Electrogenesis = 18298;
    public const uint Kaltstrahl = 18299;
    public const uint AbyssalTransfixion = 18300;
    public const uint Chirp = 18301;
    public const uint EerieSoundwave = 18302;
    public const uint PomCure = 18303;
    public const uint GobSkin = 18304;
    public const uint MagicHammer = 18305;
    public const uint Avail = 18306;
    public const uint FrogLegs = 18307;
    public const uint SonicBoom = 18308;
    public const uint Whistle = 18309;
    public const uint WhiteKnightsTour = 18310;
    public const uint BlackKnightsTour = 18311;
    public const uint LevelFiveDeath = 18312;
    public const uint Launcher = 18313;
    public const uint PerpetualRay = 18314;
    public const uint Cactguard = 18315;
    public const uint RevengeBlast = 18316;
    public const uint AngelWhisper = 18317;
    public const uint Exuviation = 18318;
    public const uint Reflux = 18319;
    public const uint Devour = 18320;
    public const uint CondensedLibra = 18321;
    public const uint AetherialMimicry = 18322;
    public const uint Surpanakha = 18323;
    public const uint Quasar = 18324;
    public const uint JKick = 18325;
    public const uint TripleTrident = 23264;
    public const uint Tingle = 23265;
    public const uint TatamiGaeshi = 23266;
    public const uint ColdFog = 23267;
    public const uint WhiteDeath = 23268;
    public const uint SaintlyBeam = 23270;
    public const uint FeculentFlood = 23271;
    public const uint AngelsSnack = 23272;
    public const uint ChelonianGate = 23273;
    public const uint DivineCataract = 23274;
    public const uint TheRoseOfDestruction = 23275;
    public const uint BasicInstinct = 23276;
    public const uint Ultravibration = 23277;
    public const uint Blaze = 23278;
    public const uint MustardBomb = 23279;
    public const uint DragonForce = 23280;
    public const uint AetherialSpark = 23281;
    public const uint HydroPull = 23282;
    public const uint MaledictionOfWater = 23283;
    public const uint ChocoMeteor = 23284;
    public const uint MatraMagic = 23285;
    public const uint PeripheralSynthesis = 23286;
    public const uint BothEnds = 23287;
    public const uint PhantomFlurry = 23288;
    public const uint PhantomFlurryEnd = 23289;
    public const uint NightBloom = 23290;
    public const uint Stotram = 23416;
    public const uint AetherialMimicryHealer = 2126;
    public const uint AetherialMimicryDps = 2125;
    public const uint AetherialMimicryTank = 2124;

    #endregion

    // RPR

    #region RPR

    public const uint Slice = 24373; // [24373;
    public const uint WaxingSlice = 24374; // [24374;
    public const uint InfernalSlice = 24375; // [24375;
    public const uint SpinningScythe = 24376; // [24376;
    public const uint NightmareScythe = 24377; // [24377;
    public const uint ShadowOfDeath = 24378; // [24378;
    public const uint WhorlOfDeath = 24379; // [24379;
    public const uint SoulSlice = 24380; // [24380;
    public const uint SoulScythe = 24381; // [24380;
    public const uint Gibbet = 24382; // [24382;
    public const uint Gallows = 24383; // [24383;
    public const uint Guillotine = 24384; // [24384;

    public const uint
        PlentifulHarvest = 24385; // [24385;

    public const uint Harpe = 24386; // [24386;
    public const uint Soulsow = 24387;
    public const uint HarvestMoon = 24388; // [24388;
    public const uint BloodStalk = 24389; // [24389;
    public const uint UnveiledGibbet = 24390; // [24390;
    public const uint UnveiledGallows = 24391; // [24391;
    public const uint GrimSwathe = 24392; // [24392;
    public const uint Gluttony = 24393; // [24393;
    public const uint Enshroud = 24394;
    public const uint VoidReaping = 24395; // [24395;
    public const uint CrossReaping = 24396; // [24396;
    public const uint GrimReaping = 24397; // [24397;
    public const uint Communio = 24398; // [24398;
    public const uint LemuresSlice = 24399; // [24399;
    public const uint LemuresScythe = 24400; // [24400;
    public const uint HellsIngress = 24401; // [24401;
    public const uint HellsEgress = 24402; // [24402;
    public const uint Regress = 24403; // [24403;
    public const uint ArcaneCrest = 24404;
    public const uint ArcaneCircle = 24405;

    #endregion

    //PVP

    #region PVP

    public const uint Concentrate = 1582;
    public const uint Muse = 1583;
    public const uint Safeguard = 1585;
    public const uint Enliven = 1580;
    public const uint Recuperate = 1590;
    public const uint Testudo = 1558;
    public const uint GlorySlash = 1559;
    public const uint FullSwing = 1562;
    public const uint PushBack = 1597;
    public const uint EmpyreanRain = 3362;

    public const uint PvpMalefic3 = 8912;
    public const uint PvpEssentialDignity = 8916;
    public const uint PvpLightspeed = 8917;
    public const uint PvpSynastry = 8918;
    public const uint PvpBenefic = 8913;
    public const uint PvpBenefic2 = 8914;
    public const uint Deorbit = 9466;
    public const uint PvpDisable = 9623;
    public const uint PvpDraw = 10026;
    public const uint PvpPlayDrawn = 10026;

    public const uint StraightShotPvp = 8835;
    public const uint EmpyrealArrowPvp = 8838;
    public const uint RepellingShotPvp = 8839;
    public const uint BloodletterPvp = 9624;
    public const uint SidewinderPvp = 8841;
    public const uint BarragePvp = 9625;
    public const uint PitchPerfectPvp = 8842;
    public const uint TheWanderersMinuetPvp = 8843;
    public const uint ArmysPaeonPvp = 8844;
    public const uint TroubadourPvp = 10023;

    public const uint Pvpmch123 = 17749;
    public const uint MountedPvp = 1420;

    //WHM
    public const uint Purify = 1584;
    public const uint Stone3Pvp = 8894;
    public const uint CurePvp = 8895;
    public const uint Cure2Pvp = 8896;
    public const uint RegenPvp = 8898;
    public const uint DivineBenisonPvp = 9621;
    public const uint AssizePvp = 9620;
    public const uint FluidAuraPvp = 8900;

    //NIN_PVP
    public const uint basecombo1 = 29500;
    public const uint basecombo2 = 29501;
    public const uint basecombo3 = 29502;
    public const uint LB = 29515;
    public const uint Shuriken = 29505;
    public const uint Hyosho = 29506;
    public const uint Goka = 29504;
    public const uint doton = 29514;
    public const uint hoton = 29512;
    public const uint meisui = 29508;

    public const uint heal = 29711;
    public const uint sprint = 29057;
    public const uint Guard = 29054;
    public const uint bunnsinn = 29511;
    public const uint purify = 29056;
    public const uint mug = 29509;
    public const uint ThreeMudrag = 29507;

    #endregion

    #region 多变迷宫

    public const uint 多变治疗水道 = 29729;
    public const uint 多变治疗六根山 = 33862;

    public const uint 多变最后通牒 = 29730;
    public const uint 多变复活 = 29731;

    public const uint 多变精神镖水道 = 29732;
    public const uint 多变精神镖六根山 = 33863;

    public const uint 多变铁壁水道 = 29733;
    public const uint 多变铁壁六根山 = 33864;

    #endregion
}