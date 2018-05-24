using Moq;
using RESTFulService.Entity;
using RESTFulService.Models.Members.DTO;
using RESTFulService.Models.Members.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RESTFulServiceUnitTest.Models.Members
{
    public class MemberOperationsUnitTest
    {
        #region Private fields
        MemberOperations _member;
        Mock<IMemberRepository> _mockMemberRepository = new Mock<IMemberRepository>();
        #endregion

        #region Construction
        public MemberOperationsUnitTest()
        {
            _member = new MemberOperations(_mockMemberRepository.Object);
        } 

        #endregion

        #region Test methods

        [Fact]
        public void MemberOperations_Construction_Test()
        {
            MemberOperations member = new MemberOperations(_mockMemberRepository.Object);
            Assert.NotNull(member);
        }

        [Fact]
        public void MemberOperations_GetMembers_Test()
        {
            IList<Member> fakeMembers = new List<Member>();
            fakeMembers.Add(new Member() { FirstName = "Test", LastName = "test", Id = 1, DateOfBirth = new DateTime() });
            _mockMemberRepository.Setup(x => x.GetAll()).Returns(fakeMembers);
            IList<Member> members = _member.GetMembers() as List<Member>;
            Assert.NotNull(members);
            Assert.True(members.Count > 0);
        }

        [Fact]
        public void MemberOperations_AddMembers_Test()
        {
            _mockMemberRepository.Setup(x => x.Add(new Member())).Verifiable();
            
            Assert.True(_member.AddMember(new Member()));
        }
        #endregion
    }
}
