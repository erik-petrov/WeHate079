using Exiled.API.Interfaces;

namespace WeHate079
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public int TimeToReact { get; set; } = 30;
    }
}
