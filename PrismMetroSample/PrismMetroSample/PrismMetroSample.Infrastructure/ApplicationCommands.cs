using Prism.Commands;

namespace PrismMetroSample.Infrastructure
{
    public interface IApplicationCommands
    {
        public CompositeCommand ShowCommand { get; }
    }

    public class ApplicationCommands : IApplicationCommands
    {
        public CompositeCommand ShowCommand { get; } = new CompositeCommand();

    }
}
