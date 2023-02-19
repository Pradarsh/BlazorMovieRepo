using Movie.Common.DTO;
using Movie.Common.HttpClients;
using System.Text.Json;

namespace Movie.Common.Services;

public class MembershipService : IMembershipService
{
    private readonly MembershipHttpClient _http;

    public MembershipService(MembershipHttpClient http)
    {
        _http = http;
    }

    public async Task<List<DirectorDTO>> GetDirectorAsync()
    {
        try
        {
            using HttpResponseMessage response =
                await _http.Client.GetAsync($"director");

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<List<DirectorDTO>>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }

    public async Task<DirectorDTO> GetDirectorAsync(int? id)
    {
        try
        {
            if(id is null) throw new ArgumentNullException("id");

            using HttpResponseMessage response =
                await _http.Client.GetAsync($"director/{id}");

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<DirectorDTO>(
                    await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return result ?? new();
        }
        catch
        {
            throw;
        }
    }
}
