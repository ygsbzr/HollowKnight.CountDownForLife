using MagicUI.Core;
using MagicUI.Elements;
using GlobalEnums;
namespace CountdownforLife
{
    public class CountDownTimer:MonoBehaviour
    {
        private static LayoutRoot layoutRoot;
        public static float timer = 0f;
       private static TextObject timerdisplay;
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
                HeroController.instance.TakeDamage(HeroController.instance.gameObject, CollisionSide.other, 9999, 2);
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
                }
            }
        }
    }
}
