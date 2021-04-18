using System.Collections.Generic;
using System.IO;
using CoreFX.Abstractions.Consts;
using TestAbstractions.App_Start;

namespace UnitTest.FileSigning.App_Start
{
    public abstract class HelloMediatRUnitTestBase : UnitTestBase
    {
        public override Dictionary<string, bool> GetConfigPathList() => new Dictionary<string, bool>
        {
            { Path.Combine(SvcConst.DefaultConfigFolder, SvcConst.DefaultAppSettingsFile), false },
        };
    }
}
