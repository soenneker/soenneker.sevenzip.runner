using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.SevenZip.Runner.Utils.Abstract;

public interface IFileOperationsUtil
{
    ValueTask Process(string filePath, CancellationToken cancellationToken);
}
