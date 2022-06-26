using Modding.Converters;
using InControl;
using Newtonsoft.Json;
namespace CountdownforLife
{
    public class Setting
    {
        public int TTL => minute * 60 + second;
        public Mode mode = Mode.None;
        public bool timestart = false;
        public Punishmode punish = Punishmode.Die;
        public int minute = 5;
        public int second = 0;
    }
    public enum Mode
    {
        HallOfGod,
        Pantheon,
        BossStatue,
        EachRoom,
        All,
        None
    }
    public enum Punishmode
    {
        Die,
        TeleportToHG,
        TeleportToTown,
        TelePortToBlueLake,
        TelePortToPOP
    }
   
}
