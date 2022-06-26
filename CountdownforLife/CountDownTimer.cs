using MagicUI.Core;
using MagicUI.Elements;
using GlobalEnums;
using Modding;
namespace CountdownforLife
{
    public class CountDownTimer:MonoBehaviour
    {
        private static LayoutRoot layoutRoot;
        public static float timer = 0f;
       private static TextObject timerdisplay;
        private Color origcolor;
        public void Awake()
        {
            if(layoutRoot == null)
            {
                layoutRoot = new(true, "CountDownTimer");
                timerdisplay = new(layoutRoot, "CountDownTimerDisplay")
                {
                    Visibility= Visibility.Hidden,
                    HorizontalAlignment= HorizontalAlignment.Right,
                    VerticalAlignment= VerticalAlignment.Top,
                    Font=UI.TrajanBold,
                    Text="",
                    FontSize=26
                };
                origcolor = timerdisplay.ContentColor;
            }
        }
        private bool Ispause()
        {
            if(HeroController.instance != null)
            {
                if(GameManager.instance.GetSceneNameString() == "Menu_Title" ||
                GameManager.instance.IsNonGameplayScene()||GameManager.instance.IsGamePaused()||!HeroController.instance.acceptingInput)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        void Update()
        {
            UpdateTimer();
            UpdateDisplayer();
        }
        public void UpdateTimer()
        {
            if (Ispause()||!CountdownforLife.GS.timestart)
                return;
            timer += Time.deltaTime;
            if(timer>CountdownforLife.GS.TTL)
            {
                timer = 0;
                CountdownforLife.GS.timestart = false;
                if(CountdownforLife.GS.punish==Punishmode.Die)
                {
                    HeroController.instance.TakeDamage(HeroController.instance.gameObject, CollisionSide.other, 9999, 2);
                }
                else
                {
                    TelePort();
                }
                if(CountdownforLife.GS.mode==Mode.EachRoom||CountdownforLife.GS.mode==Mode.All)
                {
                    CountdownforLife.GS.timestart = true;
                }
            }
        }
        public void UpdateDisplayer()
        {
           if(GameManager.instance != null)
            {
                if (GameManager.instance.GetSceneNameString() == "Menu_Title" ||
               GameManager.instance.IsNonGameplayScene() || HeroController.instance == null)
                {
                    timerdisplay.Visibility = Visibility.Hidden;
                }
                else
                {
                    timerdisplay.Visibility = Visibility.Visible;
                    timerdisplay.Text = $"The rest of your life: {((int)(CountdownforLife.GS.TTL - timer) / 60).ToString()}:{((int)(CountdownforLife.GS.TTL - timer) % 60).ToString("00")}";
                    if(CountdownforLife.GS.TTL - timer<=5)
                    {
                        timerdisplay.ContentColor = Color.red;
                    }
                    else
                    {
                        timerdisplay.ContentColor = origcolor;
                    }
                }
            }
        }
        private void TelePort()
        {
            HeroController.instance.IgnoreInputWithoutReset();
            HeroController.instance.CancelSuperDash();
            ReflectionHelper.SetField(HeroController.instance, "airDashed", false);
            ReflectionHelper.SetField(HeroController.instance, "doubleJumped", false);
            HeroController.instance.AffectedByGravity(false);
            if(CountdownforLife.GS.punish==Punishmode.TeleportToHG)
            {
                GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
                {
                    SceneName = "GG_Workshop",
                    HeroLeaveDirection = GatePosition.right,
                    EntryDelay = 0,
                    EntryGateName = "left1",
                    WaitForSceneTransitionCameraFade = false,
                    Visualization = 0,
                    AlwaysUnloadUnusedAssets = true
                });
            }
            if(CountdownforLife.GS.punish==Punishmode.TeleportToTown)
            {
                GameManager.instance.BeginSceneTransition(new GameManager.SceneLoadInfo
                {
                    SceneName = "Town",
                    HeroLeaveDirection = GatePosition.bottom,
                    EntryDelay = 0,
                    EntryGateName = "top1",
                    WaitForSceneTransitionCameraFade = false,
                    Visualization = 0,
                    AlwaysUnloadUnusedAssets = true
                });
            }
        }
    }
}
