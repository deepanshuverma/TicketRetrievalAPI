using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketRetrievalAPI.Abstractions.Models;
using TicketRetrievalAPI.Abstractions.Models.Database;
using TicketRetrievalAPI.DataAccessLayer;

namespace TicketRetrievalAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InternalController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public InternalController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<TicketIssuedCountResponse> GetCountOfAllIssuedTcikets()
        {
            var count = await _context.Tickets.CountAsync();
            var respone = new TicketIssuedCountResponse()
            {
                count = count
            };
            return respone;
        }

        [HttpPost]
        public async Task<InValidateTicketResponse> InValidateTicket(ValidateTicketRequest request)
        {
            var response = new InValidateTicketResponse();
            var rowsAffected = _context.Database.
                ExecuteSqlInterpolated($"UPDATE Tickets SET IsValid = false WHERE id = {request.Id}");
            if(rowsAffected > 0)
            {
                response.IsSuccess = true;
            } else
            {
                response.IsSuccess = false;
                response.Error = $"{request.Id} is Invalid";
            }

            
            return response;
        }


        private async Task<bool> TicketsExists(Guid id) => await _context.Tickets.AnyAsync(e => e.Id == id);
    }
}
