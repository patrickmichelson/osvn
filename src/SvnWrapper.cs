using System.Text;
using System.Threading.Tasks;
using CliWrap;

namespace osvn
{
    /// <summary>
    /// Wrapper around svn.exe
    /// </summary>
    class SvnWrapper
    {
        protected readonly string _workingCopyPath;
        
        public SvnWrapper(string workingCopyPath)
        {
            _workingCopyPath = workingCopyPath;
        }

        public Task<string> GetUrlAsync()
        {
            return RunAsync("info --show-item url");
        }

        private async Task<string> RunAsync(string command)
        {
            var stdOutBuffer = new StringBuilder();

            await Cli.Wrap("svn")
                .WithArguments(command)
                .WithWorkingDirectory(_workingCopyPath)
                .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
                .ExecuteAsync();

            return stdOutBuffer.ToString().TrimEnd();
        }
    }
}
