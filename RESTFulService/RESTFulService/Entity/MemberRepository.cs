using RESTFulService.Models.Members.DTO;

namespace RESTFulService.Entity
{
    /// <summary>
    /// Class to perform operations on Members table
    /// </summary>
    public class MemberRepository : EntityBaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(MembersContext context)
            : base(context)
        {
        }
    }
}
