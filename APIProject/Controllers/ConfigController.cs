using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace APIProject.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ConfigController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsSnapshot<AttachmentOptions> _attachmentOptions;
        public ConfigController(IConfiguration configuration,IOptionsSnapshot<AttachmentOptions>attachmentOptions)
        {
            _configuration = configuration;
            _attachmentOptions=attachmentOptions;
        }
        [HttpGet]
        [Route("")]
        public ActionResult GetConfig()
        {
            var Config = new
            {
                AllowedHosts = _configuration["AllowedHosts"],
                DefaultConnection = _configuration["ConnectionStrings:DefaultConnection"],
                DefaultLogLevel = _configuration["Logging:LogLevel:Default"],
                TesKey = _configuration["TestKey"],
                signingkey = _configuration["signingkey"],
                AttachmentOptions = _attachmentOptions.Value
            };
            return Ok(Config);
        }
    }
}
