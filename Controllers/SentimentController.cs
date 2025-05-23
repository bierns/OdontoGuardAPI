using Microsoft.AspNetCore.Mvc;
using OdontoGuardAPI.Models;
using OdontoGuardAPI.Services;

namespace OdontoGuardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentimentController : ControllerBase
    {
        private readonly SentimentAnalysisService _sentimentService;

        public SentimentController()
        {
            _sentimentService = new SentimentAnalysisService();
        }

        [HttpPost]
        public ActionResult<SentimentPrediction> PostSentiment(SentimentModel sentimentModel)
        {
            var prediction = _sentimentService.PredictSentiment(sentimentModel);
            return Ok(prediction);
        }
    }
}
