namespace WeHate079.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using Exiled.API.Features.Roles;
    using PlayerRoles;

    [CommandHandler(typeof(ClientCommandHandler))]
    public class Swap : ICommand
    {
        public string Command { get; } = "swapTo";
        public string[] Aliases { get; } = new[] { "st" };
        public string Description { get; } = "Команда для смены сцп. Формат: .swapTo <номер-сцп>(049, 939-53, 939-89)";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if(arguments.Count == 0)
            {
                response = "Отсутствуют аргументы. Формат: .swapTo <номер-сцп>(049, 939-53, 939-89)";
                return true;
            }
            if (MainClass.SwapId == "")
            {
                response = "чел ты";
                return true;
            }
            
            Player plr = Player.Get(sender);
            RoleTypeId targetRole = RoleTypeId.None;
            if (plr.UserId != MainClass.SwapId)
            {
                response = "Вы не имеете права.";
                return true;
            }
            switch (arguments.At(0))
            {
                case "049":
                    targetRole = RoleTypeId.Scp049;
                    break;
                case "096":
                    targetRole = RoleTypeId.Scp096;
                    break;
                case "106":
                    targetRole = RoleTypeId.Scp106;
                    break;
                case "173":
                    targetRole = RoleTypeId.Scp173;
                    break;
                case "939-53":
                    targetRole = RoleTypeId.Scp939;
                    break;
                case "939-89":
                    targetRole = RoleTypeId.Scp939;
                    break;
                case "049-2":
                    response = "чел ну не зомби же, а";
                    return true;
                case "079":
                    response = "иди нахуй";
                    return true;
            }
            if(targetRole == RoleTypeId.None)
            {
                response = "Неизвестный сцп "+ arguments.At(0);
                return true;
            }
            Log.Info($"Игрок {plr.Nickname} поменял комп на {arguments.At(0)}");
            plr.SetRole(targetRole);
            plr.ClearBroadcasts();
            plr.Broadcast(new Broadcast("Успешная смена! Удачной игры uWu~~", duration: 5));
            MainClass.SwapId = "";
            response = " Успешная смена сцп! Удачной игры uWu~~";
            return true;
        }
    }
}
