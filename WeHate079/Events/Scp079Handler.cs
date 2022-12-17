using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;

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
            if (ev.Player.Role != RoleTypeId.Scp079)
                return;
            string message = "Не нравиться 079? Замени его!\n";
            var scps = MainClass.GetAvailableScps();
            foreach (var item in scps)
            {
                switch ((RoleTypeId)item.Role)
                {
                    case RoleTypeId.Scp049:
                        message += "Доктора(049), ";
                        break;
                    case RoleTypeId.Scp106:
                        message += "Деда(106), ";
                        break;
                    case RoleTypeId.Scp173:
                        message += "Печенька(173), ";
                        break;
                    case RoleTypeId.Scp096:
                        message += "Скромник(096), ";
                        break;
                    case RoleTypeId.Scp939:
                        message += "Собака(939), ";
                        break;
                    //pc
                    case RoleTypeId.Scp079:
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
            if(ev.Player.Role == RoleTypeId.Scp079)
            {   
                if (ev.DamageHandler.Type == Exiled.API.Enums.DamageType.Recontainment)
                {
                    ev.IsAllowed = false;
                    rnd = new System.Random();
                    var zis = new RoleTypeId[] { RoleTypeId.Scp106, RoleTypeId.Scp939};
                    RoleTypeId chosen = zis[rnd.Next(0, zis.Length-1)];
                    if(chosen == RoleTypeId.Scp106 && is106Contained) chosen = RoleTypeId.Scp939;
                    Log.Info("Нарандомило: " + chosen.ToString());
                    ev.Player.SetRole(chosen);
                }
            }
        }
        /*public void OnScp106Contain(ContainingEventArgs ev)
        {
            is106Contained = true;
        }*/
        public void OnRoundStart()
        {
            is106Contained = false;
        }
    }
}
