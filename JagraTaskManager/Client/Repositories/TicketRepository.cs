using JagraTaskManager.Shared.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JagraTaskManager.Client.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _rootUrl = "api/ticket";
        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public TicketRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<TicketForListDto> CreateTicket(TicketForCreationDto ticket)
        {
            var dataJson = JsonSerializer.Serialize(ticket);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_rootUrl}/create", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TicketForListDto>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to Create Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<TicketForListDto> GetTicket(string ticketId)
        {
            var response = await _httpClient.GetAsync($"{_rootUrl}?ticketId={ticketId}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TicketForListDto>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to get Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<List<TicketForListDto>> GetTicketsByOrganization(string orgId)
        {
            var response = await _httpClient.GetAsync($"{_rootUrl}/byorg?organizationId={orgId}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TicketForListDto>>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to get Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<List<TicketForListDto>> GetTicketsByTeam(string teamId)
        {
            var response = await _httpClient.GetAsync($"{_rootUrl}/byteam?teamId={teamId}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TicketForListDto>>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to get Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<List<TicketForListDto>> GetTicketsByUser(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<TicketForListDto>>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to get Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<List<TicketForListDto>> GetTicketsByCreator()
        {
            return await GetTicketsByUser($"{_rootUrl}/bycreator");
        }

        public async Task<List<TicketForListDto>> GetTicketsByAssignee()
        {
            return await GetTicketsByUser($"{_rootUrl}/byassignee");
        }

        public async Task<List<TicketForListDto>> GetTicketsByWatcher()
        {
            return await GetTicketsByUser($"{_rootUrl}/bywatcher");
        }

        public async Task<TicketForListDto> UpdateTicketTitle(TicketForListDto ticket)
        {
            var dataJson = JsonSerializer.Serialize(ticket);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_rootUrl}/updateTitle", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TicketForListDto>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to Modify Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<TicketForListDto> UpdateTicketDescription(TicketForListDto ticket)
        {
            var dataJson = JsonSerializer.Serialize(ticket);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_rootUrl}/updateDescription", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TicketForListDto>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to Modify Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }

        public async Task<TicketForListDto> UpdateTicketAssignee(TicketForListDto ticket)
        {
            var dataJson = JsonSerializer.Serialize(ticket);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_rootUrl}/updateAssignee", stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<TicketForListDto>(responseString, defaultJsonSerializerOptions);
            }
            else
            {
                throw new Exception($"Failed to Modify Ticket. {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}