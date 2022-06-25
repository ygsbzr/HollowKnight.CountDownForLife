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
                (s)=>CountdownforLife.GS.mode=(Mode)s,
                ()=>(int)CountdownforLife.GS.mode,
                Id:"CountdownMode"
                ),
                new CustomSlider("Time to live",
                (f)=>CountdownforLife.GS.TTL=(int)f,
                ()=>(float)CountdownforLife.GS.TTL,
                Id:"CountDownTTL"
                )
                {
                    wholeNumbers=true,minValue=10,maxValue=7200
                }
               
            }) ;
        }
    }
}
