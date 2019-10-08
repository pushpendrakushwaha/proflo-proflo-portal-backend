using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.BusinessLayer
{
    public interface IEmailNotificationService
    {

        void SendEmail(Member member);
        string GenerateToken(Member member);
        Member VerifyAndDecodeToken(string token);

    }
}
