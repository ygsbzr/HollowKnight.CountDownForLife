using Modding.Converters;
using InControl;
using Newtonsoft.Json;
namespace CountdownforLife
{
    public class Setting
    {
        public int TTL = 300;
        public Mode mode = Mode.None;
        public bool timestart = false;
    }
    public enum Mode
    {
        HallOfGod,
        Pantheon,
        BossStatue,
        EachRoom,
        None
    }
   
}
