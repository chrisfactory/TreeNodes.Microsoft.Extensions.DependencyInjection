using Microsoft.Extensions.DependencyInjection;

namespace TreeNodes.Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// Specifies the contract for a TreeNodes services.
    /// </summary>
    public interface ITreeNodeProvider
    {
        public string Key { get;}
        ServiceCollection CreateNode();
        void MergeNodeTo(params IServiceCollection[] sources);
    }
}
