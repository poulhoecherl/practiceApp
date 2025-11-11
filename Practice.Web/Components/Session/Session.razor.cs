
using Microsoft.AspNetCore.Components;
using Practice.Services.DTOs;
using Practice.Services.Interfaces;

namespace Practice.Web.Components.Session
{
    public partial class Session
    {
        [Inject]
        public ISessionService? SessionService { get; set; }

        List<SessionDto> Sessions { get; set; } = new();
        
        protected override async Task<IEnumerable<SessionDto>> OnInitializedAsync()
        {
            if (SessionService == null)
            {
                throw new InvalidOperationException("SessionService is not initialized.");
            }

            Sessions = (List<SessionDto>)await SessionService.GetAllSessionsAsync();
            return Sessions;
        }
    }
}
