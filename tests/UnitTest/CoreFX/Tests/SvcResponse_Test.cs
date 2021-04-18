using CoreFX.Abstractions.Contracts;
using CoreFX.Abstractions.Enums;
using CoreFX.Abstractions.Extensions;
using TestAbstractions.App_Start;
using Xunit;

namespace UnitTest.CoreFX.Tests
{
    public class SvcResponse_Test : UnitTestBase
    {
        [Fact]
        public void SubCode_Test()
        {
            var res = new SvcResponseDto<string>();
            res.SetSubCode(SvcCodeEnum.ErrorCode_Max);

            Assert.False(res.IsSuccess);
            Assert.False(res.Any());
            Assert.Equal(res.SubCode, ((int)SvcCodeEnum.ErrorCode_Max).ToString());
            Assert.Equal(res.SubMsg, SvcCodeEnum.ErrorCode_Max.ToString());
        }
    }
}
