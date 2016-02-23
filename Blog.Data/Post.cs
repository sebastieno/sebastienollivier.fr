using System;

namespace Blog.Data
{
    public class Post
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ComputedDescription
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Description))
                {
                    return this.Description;
                }

                var description = this.Content.Split(new string[] { "<!-- more -->", "<!--more-->" }, StringSplitOptions.RemoveEmptyEntries);

                if (description.Length > 0)
                {
                    return description[0];
                }

                return string.Empty;
            }
        }

        public string Content { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
