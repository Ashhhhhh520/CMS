namespace CMS.Nav.Model;

public class NavModel
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public string? Href { get; set; }

    public string Icon { get; set; }

    public string ParentIcon { get; set; }

    public string Title { get; set; }

    public string FullTitle { get; set; }

    public bool Hide { get; set; }

    public bool Active { get; set; }

    public string? Target { get; set; }

    public NavModel[]? Children { get; set; }

}