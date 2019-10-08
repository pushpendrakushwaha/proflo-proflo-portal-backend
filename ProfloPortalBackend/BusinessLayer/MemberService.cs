using ProfloPortalBackend.DataAccessLayer;
using ProfloPortalBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfloPortalBackend.BusinessLayer
{
    public class MemberService: IMemberService
    {
         
        private readonly IMemberRepository memberRepository;
        public MemberService(IMemberRepository memberInterface)
        {
            this.memberRepository = memberInterface;
        }

        public async Task<Member> GetMemberByUserIdAsync(string userId)
        {
            return await memberRepository.GetMemberByUserIdAsync(userId);
        }

        public async Task<Member> GetMemberByUserEmailIdAsync(string emailId)
        {
            return await memberRepository.GetMemberByUserEmailIdAsync(emailId);
        }

        public async Task<Member> CreateMemberIfNotExistsAsync(Member member)
        {
           return await memberRepository.CreateMemberIfNotExistsAsync(member);
        }

        public Task<Member> GetMemberAsync(string memberId)
        {
            return memberRepository.GetMemberAsync(memberId);
        }

        public Task<Member> UpdateMemberAsync(Member member)
        {
            return memberRepository.UpdateMemberAsync(member);
        }
    }
}
