using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace WeHate079.Events
{
    public class Scp079Handler
    {
        private readonly MainClass _plugin;
        private int time;
        public void OnSpawn(SpawnedEventArgs ev)
        {
            time = _plugin.Config.TimeToReact;
            if (ev.Player.Role != RoleType.Scp079)
                return;
            string message = "Не нравиться 079? Замените его на любой дцп кроме зомби и ";
            var scps = MainClass.GetAvailableScps();
            foreach (var item in scps)
            {
                switch ((RoleType)item.Role)
                {
                    case RoleType.Scp049:
                        message += "Доктора(049), ";
                        break;
                    case RoleType.Scp106:
                        message += "Деда(106), ";
                        break;
                    case RoleType.Scp173:
                        message += "Печенька(173), ";
                        break;
                    case RoleType.Scp096:
                        message += "Скромник(096), ";
                        break;
                    case RoleType.Scp93953:
                        message += "Собака1(939-53), ";
                        break;
                    case RoleType.Scp93989:
                        message += "Собака2(939-89), ";
                        break;
                    //pc
                    case RoleType.Scp079:
                        break;
                }
            }
            message += $"\nКомандой .swapTo <scp-num>(049, 939-53, 939-89). У вас {time} секунд :)";
            ev.Player.Broadcast(new Exiled.API.Features.Broadcast(message, duration: 10));
            MainClass.SwapId = ev.Player.UserId;
            Timing.CallDelayed(time, () => MainClass.SwapId = "");
        }
    }
}
