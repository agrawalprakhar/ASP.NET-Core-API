using Assignment1.Data;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class keys : Controller
    {
        private readonly KeyValueDbContext _dbContext;

        public keys(KeyValueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{key}")]
        public ActionResult<CustomKeyValuePair> Get(string key)
        {
            var pair = _dbContext.KeyValuePairs.FirstOrDefault(p => p.Key == key);
            if (pair != null)
            {
                return Ok(pair);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(CustomKeyValuePairDTO pairDTO)
        {
            // Map the DTO to your entity
            var pair = new CustomKeyValuePair
            {
                Key = pairDTO.Key,
                Value = pairDTO.Value
            };
            var existingPair = _dbContext.KeyValuePairs.FirstOrDefault(p => p.Key == pair.Key);
            if (existingPair != null)
            {
                return Conflict();
            }

            _dbContext.KeyValuePairs.Add(pair);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpPatch("{key}/{value}")]
        public ActionResult Patch(string key, string value)
        {
            var pair = _dbContext.KeyValuePairs.FirstOrDefault(p => p.Key == key);
            if (pair == null)
            {
                return NotFound();
            }

            pair.Value = value;
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(string key)
        {
            var pair = _dbContext.KeyValuePairs.FirstOrDefault(p => p.Key == key);
            if (pair != null)
            {
                _dbContext.KeyValuePairs.Remove(pair);
                _dbContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
