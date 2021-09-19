using clerk_data_web.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace clerk_data_web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _BaseUrl;

        public string MemberData { get; private set; }
        [BindProperty]
        public string FileUrl { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory, IOptionsSnapshot<ServiceOptions> options)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            if (options.Value == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            _BaseUrl = options.Value.BaseUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            _logger.LogInformation($"FileUrl: {FileUrl}");
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_BaseUrl}memberdata?xmlUrl={FileUrl}");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("POST succeeded.");
            }
            else
            {
                _logger.LogError("POST failed.");
            }
            return RedirectToPage("./Index");
        }

        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_BaseUrl}memberdata/");
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                MemberData = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
