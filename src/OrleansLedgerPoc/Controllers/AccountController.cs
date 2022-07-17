using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Orleans;
using OrleansLedgerPoc.Entities.Interfaces;
using OrleansLedgerPoc.Entities.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OrleansLedgerPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IClusterClient _client;
        private ITransferGrain _transferGrain;

        public AccountController(IClusterClient client)
        {
            _client = client;
            _transferGrain = client.GetGrain<ITransferGrain>(0);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(MovementRequest request)
        {
            try
            {
                var from = _client.GetGrain<IAccountGrain>(request.DebitPartyBankBranchAccount);
                var to = _client.GetGrain<IAccountGrain>(request.CreditPartyBankBranchAccount);
                return Ok(await _transferGrain.Transfer(from, to, request));
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.ToString());
            }
            
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(MovementRequest request)
        {
            var to = _client.GetGrain<IAccountGrain>(request.CreditPartyBankBranchAccount);
            return Ok(await _transferGrain.Deposit(to, request));
        }

        [HttpGet("balance/{bank}/{branch}/{account}")]
        public async Task<IActionResult> GetBalance(int bank, int branch, int account)
        {
            var acc = _client.GetGrain<IAccountGrain>($"{bank}_{branch}_{account}");
            return Ok(await acc.GetBalance());
        }
    }
}
