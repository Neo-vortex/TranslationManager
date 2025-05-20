using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace TranslationManager.Controllers;

[Route("api/translations")]
[ApiController]
public class TranslationsApiController(ApplicationDbContext context, IDistributedCache cache) : ControllerBase
{

    [HttpGet("{key}/{culture}")]
    public async Task<IActionResult> GetTranslation(string key, string culture, [FromQuery] string parameters)
    {
        var parameterArray = string.IsNullOrEmpty(parameters)
            ? []
            : parameters.Split(',', StringSplitOptions.TrimEntries);

        var cacheKey = $"translation_{key}_{culture}_{string.Join("_", parameterArray)}";

        var cachedValue = await cache.GetStringAsync(cacheKey);
        if (cachedValue != null)
        {
            return Ok(cachedValue);
        }

        var translation = await context.Translations
            .Include(t => t.Values)
            .FirstOrDefaultAsync(t => t.Key == key);

        if (translation == null)
        {
            return NotFound($"Translation key '{key}' not found");
        }

        var value = translation.Values.FirstOrDefault(v => v.Culture == culture);
        if (value == null)
        {
            return NotFound($"Translation for culture '{culture}' not found");
        }

        string finalText;
        try
        {
            finalText = parameterArray.Length > 0
                ? string.Format(value.Value, parameterArray)
                : value.Value;
        }
        catch (FormatException ex)
        {
            return BadRequest($"Error formatting translation: {ex.Message}");
        }

        await cache.SetStringAsync(cacheKey, finalText, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });

        return Ok(finalText);
    }


}