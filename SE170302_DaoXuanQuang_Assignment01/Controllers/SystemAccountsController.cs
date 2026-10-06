using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Entity;
using SE170302_DaoXuanQuang_Assignment01_BackEnd.Repositories;

namespace SE170302_DaoXuanQuang_Assignment01_BackEnd.Controllers
{
    public class SystemAccountsController : ODataController
    {
        private readonly ISystemAccountRepository _repo;

        public SystemAccountsController(ISystemAccountRepository repo)
        {
            _repo = repo;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accounts = await _repo.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] short key)
        {
            var account = await _repo.GetAccountByIdAsync(key);
            if (account == null) return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SystemAccount account)
        {
            if (account == null) return BadRequest("Account is null");
            await _repo.AddAccountAsync(account);
            return Created(account);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] short key, [FromBody] SystemAccount account)
        {
            if (account == null) return BadRequest("Account is null");
            if (key != account.AccountId) return BadRequest("Key mismatch");
            var existing = await _repo.GetAccountByIdAsync(key);
            if (existing == null) return NotFound();
            await _repo.UpdateAccountAsync(account);
            return Updated(account);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] short key)
        {
            await _repo.DeleteAccountAsync(key);
            return NoContent();
        }
    }
}