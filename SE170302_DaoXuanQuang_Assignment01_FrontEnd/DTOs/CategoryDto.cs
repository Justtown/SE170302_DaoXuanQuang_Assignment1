using System.Text.Json.Serialization;

namespace SE170302_DaoXuanQuang_Assignment01_FrontEnd.DTOs
{
    public class CategoryDto
    {
        public short CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        [JsonPropertyName("categoryDesciption")]
        public string CategoryDesciption { get; set; } = null!;

        public short? ParentCategoryId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ODataResponse<T>
    {
        public List<T> Value { get; set; } = new List<T>();
    }
}