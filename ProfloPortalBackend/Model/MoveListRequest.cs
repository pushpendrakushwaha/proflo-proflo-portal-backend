namespace ProfloPortalBackend.Model
{
    // MoveListRequest Entities
    public class MoveListRequest
    {
        public string ListId { get; set; }
        public string BoardId { get; set; }
        public int FromListPosition { get; set; }
        public int ToListPosition { get; set; }
    }
}