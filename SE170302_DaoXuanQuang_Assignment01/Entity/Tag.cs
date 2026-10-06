using System;
using System.Collections.Generic;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;

public partial class Tag
{
    public int TagId { get; set; }

    public string? TagName { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<NewsArticle> NewsArticles { get; set; } = new List<NewsArticle>();
}
