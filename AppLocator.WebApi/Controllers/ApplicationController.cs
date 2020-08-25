using AppLocator.Application.Models;
using AppLocator.Application.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AppLocator.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ILogger<ApplicationController> _logger;

        public ApplicationController(IApplicationService applicationService,
            ILogger<ApplicationController> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            try
            {
                var result = await _applicationService.GetApplicationList();
                return new ObjectResult(result);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred", null);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplication(int id)
        {
            try
            {
                var result = await _applicationService.GetApplication(id);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred", null);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveApplication(ApplicationModel request)
        {
            try
            {
                var result = await _applicationService.SaveApplication(request);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred", null);
                throw;
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateApplication(int id, [FromBody] JsonPatchDocument<ApplicationModel> patch)
        {
            try
            {
                var result = await _applicationService.UpdateApplication(id, patch);
                return new ObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred", null);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            try
            {
                await _applicationService.DeleteApplication(id);

                return Ok();
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "An error occurred", null);
                throw;
            }
        }
    }
}
