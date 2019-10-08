using System.Threading.Tasks;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.DataAccessLayer
{
    public interface IMemberRepository
    {
        Task<Member> GetMemberByUserIdAsync(string userId);
        Task<Member> CreateMemberIfNotExistsAsync(Member member);
        Task<Member> GetMemberAsync(string memberId);
        Task<Member> UpdateMemberAsync(Member member);
        Task<Member> GetMemberByUserEmailIdAsync(string emailId);
    }
}