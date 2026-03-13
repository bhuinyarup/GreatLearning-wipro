using Microsoft.AspNetCore.Mvc.RazorPages;

namespace W4Day1AssignmentsSolution.Pages.Items;

public class IndexModel : PageModel
{
    private readonly ItemStore _store;

    public IndexModel(ItemStore store)
    {
        _store = store;
    }

    public IReadOnlyCollection<string> Items { get; private set; } = Array.Empty<string>();

    public void OnGet()
    {
        Items = _store.GetAll();
    }
}
