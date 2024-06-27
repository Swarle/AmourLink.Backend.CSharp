namespace AmourLink.Recommendation.DTO;

public class InfoDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public List<AnswerDto> Answers { get; set; } = [];
}