using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
     public interface IMemberService
    {
        Task<Member> CreateMemberIfNotExistsAsync( Member member);
        Task<Member> GetMemberAsync(string memberId);
        Task<Member> GetMemberByUserIdAsync(string userId);
        Task<Member> UpdateMemberAsync(Member member);
        Task<Member> GetMemberByUserEmailIdAsync(string emailId);
    }
}
