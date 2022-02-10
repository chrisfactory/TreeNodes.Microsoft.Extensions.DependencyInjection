using Microsoft.Extensions.DependencyInjection;

namespace TreeBranch.Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// Specifies the contract for a TreeBranch services.
    /// </summary>
    public interface ITreeBranch
    {
        public string Key { get;}
        ServiceCollection CreateNewServicesFromBranch();
        void MergeTo(params IServiceCollection[] sources);
    }
}
