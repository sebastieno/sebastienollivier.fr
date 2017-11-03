namespace Blog.Web.Models
{
#pragma warning disable IDE1006 // Shared with javascript
  public class IRequest
  {
    public object cookies { get; set; }
    public object headers { get; set; }
    public object host { get; set; }
  }
#pragma warning restore IDE1006 
}
