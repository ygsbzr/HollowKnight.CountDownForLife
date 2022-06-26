using Modding;
namespace CountdownforLife
{
    public class CountdownforLife:Mod,IGlobalSettings<Setting>,ICustomMenuMod
    {
        public static Setting GS { get; set; } = new();
        public void OnLoadGlobal(Setting s) => GS = s;
        public Setting OnSaveGlobal() => GS;
        public static GameObject timerGO = null;
        public override string GetVersion()
        {
            return "1.3";
        }
        public override void Initialize()
        {
            if(timerGO==null)
            {
                timerGO = new GameObject("CountDownTimerGO", typeof(CountDownTimer));
                UObject.DontDestroyOnLoad(timerGO);
                GS.timestart = false;
                On.BossSequenceController.SetupNewSequence += StartTimer;
                On.BossSequenceController.FinishLastBossScene += StopTimer;
                ModHooks.TakeDamageHook += CheckDie;
                UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneCheck;
                On.BossSceneController.Start += Check;
                On.BossSceneController.EndBossScene += StopBossTimer;
                On.HeroController.Start += Checkall;
            }
        }

        private void Checkall(On.HeroController.orig_Start orig, HeroController self)
        {
            orig(self);
            if(GS.mode==Mode.All)
            {
                GS.timestart = true;
                CountDownTimer.timer = 0f;
            }
        }

        private void StopBossTimer(On.BossSceneController.orig_EndBossScene orig, BossSceneController self)
        {
            orig(self);
            if(GS.mode == Mode.BossStatue&& !BossSequenceController.IsInSequence)
            {
                GS.timestart = false;
            }
        }

        private System.Collections.IEnumerator Check(On.BossSceneController.orig_Start orig, BossSceneController self)
        {
            yield return orig(self);
            if (!BossSequenceController.IsInSequence&&GS.mode==Mode.BossStatue)
            {
                CountDownTimer.timer = 0f;
                GS.timestart = true;
            }
           
        }

        private readonly List<string> GGNotHG = new() { "GG_Atrium", "GG_Atrium_Roof", "GG_Blue_Room" };
        private void SceneCheck(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            if(GS.mode==Mode.HallOfGod)
            {
                if(arg1.name== "GG_Workshop")
                {
                    GS.timestart = true;
                    CountDownTimer.timer = 0;
                }
                if(GGNotHG.Contains(arg1.name))
                {
                    GS.timestart = false;
                }
            }
            if(GS.mode==Mode.EachRoom)
            {
                GS.timestart=true;
                CountDownTimer.timer = 0;
            }
            if(GS.mode==Mode.None)
            {
                GS.timestart = false;
                CountDownTimer.timer = 0;
            }
        }

        public bool ToggleButtonInsideMenu => false;
        public MenuScreen GetMenuScreen(MenuScreen lastmenu, ModToggleDelegates? modToggle) => ModMenu.GetMenu(lastmenu);
        private int CheckDie(ref int hazardType, int damage)
        {
            if(damage>PlayerData.instance.health+PlayerData.instance.healthBlue)
            {
                GS.timestart = false;
            }
            return damage;
        }

        private void StopTimer(On.BossSequenceController.orig_FinishLastBossScene orig, BossSceneController self)
        {
            orig(self);
           if(GS.mode==Mode.Pantheon)
            {
                GS.timestart = false;
            }
        }

        private void StartTimer(On.BossSequenceController.orig_SetupNewSequence orig, BossSequence sequence, BossSequenceController.ChallengeBindings bindings, string playerData)
        {
            orig(sequence, bindings, playerData);
            if (GS.mode == Mode.Pantheon)
            {
                CountDownTimer.timer = 0;
                GS.timestart = true;
            }
        }

    }
}
