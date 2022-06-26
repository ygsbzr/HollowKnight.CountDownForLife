using Satchel.BetterMenus;
namespace CountdownforLife
{
   public class ModMenu
    {
        public static Menu menu = null;

        public static MenuScreen GetMenu(MenuScreen lastmenu)
        {
            if (menu == null)
            {
                menu = PrepareMenu();
            }
            return menu.GetMenuScreen(lastmenu);
        }
        public static Menu PrepareMenu()
        {
            return new("CountDownLife", new Element[]
            {
                new HorizontalOption("CountDownMode","Choose where it works",
                Enum.GetNames(typeof(Mode)).ToArray(),
                (s) =>
                {
                    CountdownforLife.GS.mode=(Mode)s;
                    CountDownTimer.timer=0;
                    CountdownforLife.GS.timestart=false;
                    if(CountdownforLife.GS.mode==Mode.All||CountdownforLife.GS.mode==Mode.EachRoom)
                    {
                        CountdownforLife.GS.timestart=true;
                    }
                },
                ()=>(int)CountdownforLife.GS.mode,
                Id:"CountdownMode"
                ),
                
                 new HorizontalOption("PunishmentMode","Choose how it works",
                Enum.GetNames(typeof(Punishmode)).ToArray(),
                (s) =>
                {
                    CountdownforLife.GS.punish=(Punishmode)s;
                },
                ()=>(int)CountdownforLife.GS.punish,
                Id:"CountdownPunishMode"
                ),
                new CustomSlider("Time to live Minute",
                (f)=>{CountdownforLife.GS.minute=(int)f;CountDownTimer.timer=0; },
                ()=>(float)CountdownforLife.GS.minute,
                Id:"CountDownTTLMinute"
                )
                {
                    wholeNumbers=true,minValue=0,maxValue=300
                },
                new CustomSlider("Time to live Second",
                (f)=>{CountdownforLife.GS.second=(int)f;CountDownTimer.timer=0; },
                ()=>(float)CountdownforLife.GS.second,
                Id:"CountDownTTLSecond"
                )
                {
                    wholeNumbers=true,minValue=0,maxValue=59
                }

            }) ;
        }
    }
}
