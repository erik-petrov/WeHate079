using Exiled.API.Features;
using Exiled.CreditTags.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace WeHate079.Events
{
    public class Scp079Handler
    {
        private int time;
        private System.Random rnd;
        private bool is106Contained;
        public void OnSpawn(SpawnedEventArgs ev)
        {
            time = MainClass.Instance.Config.TimeToReact;
            if (ev.Player.Role != RoleType.Scp079)
                return;
            string message = "Не нравиться 079? Замени его!\n";
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
            message += $"Командой .swapTo <scp-num>(049, 939-53, 939-89).\nУ вас {time} секунд :)";
            ev.Player.Broadcast(new Exiled.API.Features.Broadcast(message, duration: 10));
            MainClass.SwapId = ev.Player.UserId;
            Timing.CallDelayed(time, () => MainClass.SwapId = "");
        }
        public void OnDying(DyingEventArgs ev)
        {
            if(ev.Target.Role == RoleType.Scp079)
            {   
                if (ev.Handler.Type == Exiled.API.Enums.DamageType.Recontainment)
                {
                    ev.IsAllowed = false;
                    rnd = new System.Random();
                    var zis = new RoleType[] { RoleType.Scp106, RoleType.Scp93953, RoleType.Scp93989};
                    RoleType chosen = zis[rnd.Next(0, zis.Length-1)];
                    if(chosen == RoleType.Scp106 && is106Contained) chosen = RoleType.Scp93989;
                    Log.Info("Нарандомило: " + chosen.ToString());
                    ev.Target.SetRole(chosen);
                }
            }
        }
        public void OnScp106Contain(ContainingEventArgs ev)
        {
            is106Contained = true;
        }
        public void OnRoundStart()
        {
            is106Contained = false;
        }
    }
}
