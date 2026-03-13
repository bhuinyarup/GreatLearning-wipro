using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace W4Day1AssignmentsSolution.Pages.Items;

public class AddModel : PageModel
{
    private readonly ItemStore _store;

    public AddModel(ItemStore store)
    {
        _store = store;
    }

    [BindProperty]
    [Required]
    [StringLength(100, MinimumLength = 2)]
    [Display(Name = "Item Name")]
    public string InputItem { get; set; } = string.Empty;

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _store.Add(InputItem);
        return RedirectToPage("/Items/Index");
    }
}
