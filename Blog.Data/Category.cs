using Newtonsoft.Json;
using System.Collections.Generic;

namespace Blog.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
