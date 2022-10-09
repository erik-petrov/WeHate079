namespace WeHate079.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;

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
            RoleType targetRole = RoleType.None;
            if (plr.UserId != MainClass.SwapId)
            {
                response = "Вы не имеете права.";
                return true;
            }
            switch (arguments.At(0))
            {
                case "049":
                    targetRole = RoleType.Scp049;
                    break;
                case "096":
                    targetRole = RoleType.Scp096;
                    break;
                case "106":
                    targetRole = RoleType.Scp106;
                    break;
                case "173":
                    targetRole = RoleType.Scp173;
                    break;
                case "939-53":
                    targetRole = RoleType.Scp93953;
                    break;
                case "939-89":
                    targetRole = RoleType.Scp93989;
                    break;
                case "079":
                    response = "иди нахуй";
                    return true;
            }
            if(targetRole == RoleType.None)
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
