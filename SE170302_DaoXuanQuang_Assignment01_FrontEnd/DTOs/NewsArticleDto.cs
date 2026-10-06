namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs
{
    public class NewsArticleDto
    {
        public string NewsArticleId { get; set; } = null!;
        public string? NewsTitle { get; set; }
        public string Headline { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
        public string? NewsContent { get; set; }
        public string? NewsSource { get; set; }
        public short? CategoryId { get; set; }
        public bool? NewsStatus { get; set; }
        public short? CreatedById { get; set; }
        public short? UpdatedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public CategoryDto? Category { get; set; }
        public List<TagDto> Tags { get; set; } = new();
        public SystemAccountDto? CreatedBy { get; set; }

        public List<int> TagIds { get; set; } = new();
    }
}