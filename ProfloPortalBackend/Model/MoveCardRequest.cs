namespace ProfloPortalBackend.Model
{
    // MoveCardRequest Entities
    public class MoveCardRequest
    {
        public string CardId { get; set; }
        public string FromListId { get; set; }
        public string ToListId { get; set; }
        public string BoardId { get; set; }
        public int FromCardPosition { get; set; }
        public int ToCardPosition { get; set; }
    }
}