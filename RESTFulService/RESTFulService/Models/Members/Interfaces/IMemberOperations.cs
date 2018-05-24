using RESTFulService.Models.Members.DTO;
using System.Collections.Generic;

namespace RESTFulService.Models.Members.Interfaces
{
    public interface IMemberOperations
    {
        IEnumerable<Member> GetMembers();

        bool AddMember(Member member);
    }
}