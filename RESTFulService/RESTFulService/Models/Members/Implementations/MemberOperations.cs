using RESTFulService.Entity;
using RESTFulService.Models.Members.DTO;
using RESTFulService.Models.Members.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RESTFulService.Models.Members.Implementations
{
    /// <summary>
    /// Member operations class for retrieving all members and adding new member
    /// </summary>
    public class MemberOperations : IMemberOperations
    {
        #region Private fields

        IMemberRepository _memberRepository;

        #endregion

        #region Constructor

        public MemberOperations(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        } 

        #endregion

        #region Implementing IMemberOperations

        /// <summary>
        /// Adds a new member
        /// </summary>
        /// <param name="member">member details object</param>
        /// <returns>Returns true</returns>
        public bool AddMember(Member member)
        {
            _memberRepository.Add(member);
            return true;
        }

        /// <summary>
        /// Retrieves the list of members
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Member> GetMembers()
        {
            return _memberRepository.GetAll();
        }

        #endregion
    }
}
