using ABC_Retail.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ABC_Retail.Pages
{
    public class UploadModel : PageModel
    {
        private readonly BlobStorageService _blobStorageService;

        public UploadModel(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [BindProperty]
        public IFormFile? File { get; set; }

        public bool UploadSuccess { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (File != null && File.Length > 0)
            {
                using var stream = File.OpenReadStream();
                await _blobStorageService.UploadFileAsync(File.FileName, stream);
                UploadSuccess = true;
            }
            return Page();
        }
    }
}
