using Microsoft.AspNetCore.Mvc;
using SRE.Program.WebAPI.BusinessLogics.Logistics.Entities;
using SRE.Program.WebAPI.BusinessLogics.Logistics.Services;

namespace SRE.Program.WebAPI.Controllers;

[Route("api/logistics")]
[ApiController]
public class LogisticsController : ControllerBase
{
    private ILogger<LogisticsController> _logger;

    private ILogisticService _logisticService;

    public LogisticsController(
        ILogger<LogisticsController> logger,
        ILogisticService logisticService)
    {
        this._logger = logger;
        this._logisticService = logisticService;
    }

    [HttpGet]
    [Route("query")]
    public ActionResult<ApiResponse> Query(long sno)
    {
        try
        {
            this._logger.LogInformation($"sno:{sno}");

            var result = this._logisticService.Query(sno);

            return new ApiResponse
            {
                status = "success",
                data = result
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                status = "error",
                data = null,
                error = new ErrorEntity
                {
                    code = 500,
                    message = ex.Message
                }
            };
        }
    }

    [HttpGet]
    [Route("fake")]
    public ActionResult<ApiResponse> Fake(int num)
    {
        try
        {
            this._logger.LogInformation($"num:{num}");

            var result = this._logisticService.Fake(num);

            return new ApiResponse
            {
                status = "success",
                data = result
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                status = "error",
                data = null,
                error = new ErrorEntity
                {
                    code = 500,
                    message = ex.Message
                }
            };
        }
    }
}
