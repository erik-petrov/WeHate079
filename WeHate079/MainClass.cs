using Exiled.API.Features;
using Exiled.API.Enums;
using WeHate079.Events;
using System.Collections.Immutable;

namespace WeHate079
{
    using Player = Exiled.Events.Handlers.Player;
    using Scp106 = Exiled.Events.Handlers.Scp106;
    using Server = Exiled.Events.Handlers.Server;
    using API = Exiled.API.Features;
    public class MainClass : Plugin<Config>
    {
        public static string SwapId;
        private static readonly MainClass Singleton = new MainClass();
        public Scp079Handler EventHandler;
        private MainClass() { }
        public static MainClass Instance => Singleton;
        public override PluginPriority Priority { get; } = PluginPriority.Last;
        public override void OnEnabled()
        {
            EventHandler = new Scp079Handler();
            Player.Spawned += EventHandler.OnSpawn;
            Player.Dying += EventHandler.OnDying;
            Server.RoundStarted += EventHandler.OnRoundStart;
            Scp106.Containing += EventHandler.OnScp106Contain;
            SwapId = "";
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Player.Spawned -= EventHandler.OnSpawn;
            Player.Dying -= EventHandler.OnDying;
            Server.RoundStarted -= EventHandler.OnRoundStart;
            Scp106.Containing -= EventHandler.OnScp106Contain;
            EventHandler = null;
            SwapId = null;
            base.OnDisabled();
        }
        public static ImmutableArray<API.Player> GetAvailableScps()
        {
            var scps = API.Player.Get(Side.Scp);
            return scps.ToImmutableArray();
        }
    }
}