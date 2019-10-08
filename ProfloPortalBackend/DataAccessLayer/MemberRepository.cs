using ProfloPortalBackend.Model;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace ProfloPortalBackend.DataAccessLayer
{
    public class MemberRepository: IMemberRepository
    {
        private readonly DBContext context;
        public MemberRepository(DBContext dBContext)
        {
            context = dBContext;
        }
        // Create Member if not available
        public async Task<Member> CreateMemberIfNotExistsAsync(Member member)
        {
            var filterDefinition = Builders<Member>.Filter.Eq(x => x.UserId, member.UserId);
            var searchUserIdResult = (await context.Members.FindAsync(filterDefinition)).ToList();
            Console.WriteLine(searchUserIdResult.ToString());
            if (searchUserIdResult.Count == 0)
            {
                await context.Members.InsertOneAsync(member);
            }
            return member;    
        }

        public async Task<Member> GetMemberByUserIdAsync(string userId)
        {
            var filterDefinition = Builders<Member>.Filter.Eq(x => x.UserId, userId);
            var members = await context.Members.FindAsync(filterDefinition);
            return members.FirstOrDefault();
        }

        public async Task<Member> GetMemberByUserEmailIdAsync(string emailId)
        {
            var filterDefinition = Builders<Member>.Filter.Eq(x => x.EmailId, emailId);
            var members = await context.Members.FindAsync(filterDefinition);
            return members.FirstOrDefault();
        }

        public async Task<Member> GetMemberAsync(string memberId)
        {
            var filterDefinition = Builders<Member>.Filter.Eq(memberId, memberId);
            var members = await context.Members.FindAsync(filterDefinition);
            return members.FirstOrDefault();
        }

        public async Task<Member> UpdateMemberAsync(Member member)
        {
            var filterDefinition = Builders<Member>.Filter.Eq(x => x.MemberId, member.MemberId);
            Console.WriteLine(member.ToString());
            await context.Members.ReplaceOneAsync(filterDefinition, member);
            return member;
        }
    }
}