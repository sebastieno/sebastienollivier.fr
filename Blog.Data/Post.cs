using System;
using System.Linq;

namespace Blog.Data
{
    public class Post
    {
        public int? Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string MarkDownContent { get; set; }

        public DateTime? PublicationDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        internal string InternalTags { get; set; }

        public string[] Tags
        {
            get
            {
                if (string.IsNullOrEmpty(this.InternalTags))
                {
                    return new string[0];
                }

                return this.InternalTags.Split(',').Select(t => t.Trim()).ToArray();
            }
            set
            {
                this.InternalTags = String.Join(',', value);
            }
        }
    }
}
