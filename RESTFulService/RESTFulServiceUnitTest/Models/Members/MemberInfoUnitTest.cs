using RESTFulService.Models.Members.Config;
using RESTFulService.Models.Members.DTO;
using System;
using Xunit;

namespace RESTFulServiceUnitTest.Models.Members
{
    /// <summary>
    /// Unit test class for MemberInfo
    /// </summary>
    public class MemberInfoUnitTest
    {
        #region Test methods

        [Fact]
        public void MemberInfo_Props_Test()
        {
            Member member = new Member() { FirstName = "test",
                LastName = "testLast",
                Email = "test@gmail.com",
                DateOfBirth = new DateTime(1945, 01, 01) };

            Assert.NotNull(member);
            Assert.Equal("test", member.FirstName);
            Assert.Equal("testLast", member.LastName);
            Assert.Equal("01-01-1945", member.DateOfBirth.ToString(DateConverter.DateFormat));
        }
        #endregion
    }
}
