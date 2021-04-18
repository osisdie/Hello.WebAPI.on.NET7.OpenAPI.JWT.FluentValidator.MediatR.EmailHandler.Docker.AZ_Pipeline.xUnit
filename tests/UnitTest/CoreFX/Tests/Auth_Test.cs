using CoreFX.Abstractions.Extensions;
using CoreFX.Auth.Models;
using CoreFX.Auth.Utils;
using TestAbstractions.App_Start;
using Xunit;

namespace UnitTest.CoreFX.Tests
{
    public class Auth_Test : UnitTestBase
    {
        [Fact]
        public void JWT_Test()
        {
            var username = nameof(Auth_Test);
            var beforeTokenDto = new JwtTokenDto
            {
                UserId = username.ToMD5(),
                UserName = username
            };

            var accessToken = JwtUtil.GenTokenkey(beforeTokenDto, 1);
            var afterTokenDto = JwtUtil.ExtracToken(accessToken);
            Assert.NotNull(afterTokenDto);
            Assert.Equal(beforeTokenDto.UserId, afterTokenDto.UserId);
            Assert.Equal(beforeTokenDto.UserName, afterTokenDto.UserName);
        }
    }
}
