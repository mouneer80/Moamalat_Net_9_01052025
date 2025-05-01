using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Moamalat.Handlers
{
    public class LocalizationOptions
    {
        public string LocalizationFolderPath { get; set; } = "wwwroot/localization";
    }
    public class LocalizationHandler
    {
        private readonly Dictionary<string, string> _translations = new();
        private readonly string? _localizationFolderPath;
        public LocalizationHandler(IOptions<LocalizationOptions> options)
        {
            _localizationFolderPath = options.Value.LocalizationFolderPath;
            

        }
        public async Task SetLanguageAsync(string language)
        {

            _translations.Clear();

            try
            {

                //var filePath = Path.Combine("wwwroot", "localization", $"{language}.json");
                var filePath = $"{_localizationFolderPath}/{language}.json"; // In wwwroot
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    FlattenJson(json, _translations);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading language file: {ex.Message}");
                throw;
            }
        }
        public void FlattenJson(string json, Dictionary<string, string> translations)
        {

            var jObject = JObject.Parse(json);
            FlattenJToken(jObject, translations, string.Empty);

        }
        private void FlattenJToken(JToken token, Dictionary<string, string> result, string prefix)
        {
            if (token is JValue value)
            {
                result[prefix] = value.ToString();
            }
            else if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    FlattenJToken(property.Value, result, string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}");
                }
            }
            else if (token is JArray array)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    FlattenJToken(array[i], result, $"{prefix}[{i}]");
                }
            }
        }

        public string GetTranslation(string key)
        {

            return _translations.TryGetValue(key, out var value) ? value : key;
        }
    }
}
