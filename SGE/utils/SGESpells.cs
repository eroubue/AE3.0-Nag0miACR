using AEAssist;

namespace Nagomi.SGE.utils;

public class SGESpells
{
         #region SGE
        public const uint EukrasianDyskrasia = 37032;
        public const uint 均衡失衡 = 37032;
        public const uint 心神风息 = 37033;
        public const uint Psyche = 37033;
        public const uint Dosis = 24283;
        public const uint 注药 = 24283;
        public const uint 发炎 = 24289;
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
        public const uint 均衡 = 24290;
        public const uint EukrasianDiagnosis = 24291;

        public static uint EukrasianPrognosis
        {
            get
            {
                const uint defaultEukrasianPrognosis = 24292;
                const uint level96EukrasianPrognosis = 37034;

                return Core.Me.Level >= 96 ? level96EukrasianPrognosis : defaultEukrasianPrognosis;
            }
        }


        public const uint Philosophia = 37035;
        public const uint EukrasianDosis = 24293;
        public const uint 均衡注药 = 24293;
        public const uint Soteria = 24294;
        public const uint Druochole = 24296;
        public const uint Dyskrasia = 24297;
        public const uint 失衡 = 24297;
        public const uint Kerachole = 24298;
        public const uint Ixochole = 24299;
        public const uint Zoe = 24300;
        public const uint Pepsis = 24301;
        public const uint Taurochole = 24303;
        public const uint Toxikon = 24304;
        public const uint 箭毒 = 24304;
        public const uint ToxikonIi = 24316;
        public const uint Haima = 24305;
        public const uint DosisIi = 24306;
        public const uint 注药II= 24306;
        public const uint 发炎II= 24307;
        public const uint 均衡注药II = 24308;
        public const uint 根素 = 24309;
        public const uint Holos = 24310;
        public const uint Panhaima = 24311;
        public const uint 注药III = 24312;
        public const uint 发炎III = 24313;
        public const uint 均衡注药III = 24314;
        public const uint 失衡II = 24315;
        public const uint 箭毒II = 24316;
        public const uint Krasis = 24317;
        public const uint 魂灵风息 = 24318;
        public const uint Icarus = 24295;
        public const uint 醒梦 = 7562;
        public const uint 康复 = 7568;
        public const uint 沉稳咏唱 = 7559;
        public const uint 即刻咏唱 = 7561;

        #endregion
    
}