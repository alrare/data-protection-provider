using Microsoft.AspNetCore.Mvc;
using DataProtectionProvider.Data;
using DataProtectionProvider.Model;
using Microsoft.AspNetCore.DataProtection;

namespace DataProtectionPrivider.Controllers
{
    [Route("api")]
    [ApiController]
    public class PersonalDataController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IDataProtectionProvider _provider;
        private readonly IDataProtector _protector;


        public PersonalDataController(DataContext context, IDataProtectionProvider provider, IDataProtector protector)
        {
            _context = context;
            _provider = provider.CreateProtector("PersonalData");
            _protector = protector;
        }

        [HttpGet("GetAllPersonalData")]
        public async Task<ActionResult<List<PersonalData>>> Get()
        {
            return Ok(await _context.PersonalData.ToListAsync());
        }

        [HttpGet("GetPersonalDataById/{id}")]
        public async Task<ActionResult<List<PersonalData>>> Get(int id)
        {
            var prod = await _context.PersonalData.FindAsync(id);
            if (prod == null)
                return BadRequest("Personal data not found.");
            return Ok(prod);
        }

        [HttpPost("AddPersonalData")]
        public async Task<ActionResult<PersonalData>> Add(PersonalData personalData)
        {

            //Encriptar para guardar en la bd
            string encriptedEmail = _protector.Protect(personalData.Email);
            string encriptedPhone = _protector.Protect(personalData.Phone);

            _context.PersonalData.Add(personalData);
            await _context.SaveChangesAsync();

            return Ok(await _context.PersonalData.ToListAsync());
        }

        [HttpPut("UpdatePersonalData")]
        public async Task<ActionResult<PersonalData>> UpdateProduct(PersonalData request)
        {
            var dbPer = await _context.PersonalData.FindAsync(request.Id);
            if (dbPer == null)
                return BadRequest("Personal data not found.");

            dbPer.Id = request.Id;
            dbPer.FirtsName = request.FirtsName;
            dbPer.LastName = request.LastName;
            dbPer.Email = request.Email;
            dbPer.Phone = request.Phone;

            await _context.SaveChangesAsync();

            return Ok(await _context.PersonalData.ToListAsync());
        }

        [HttpDelete("DeletePersonalData/{id}")]
        public async Task<ActionResult> DeletePersonalData(int id)
        {
            var dbPer = await _context.PersonalData.FindAsync(id);
            if (dbPer == null)
                return BadRequest("Personal data not found.");

            _context.PersonalData.Remove(dbPer);
            await _context.SaveChangesAsync();

            return Ok(await _context.PersonalData.ToListAsync());
        }
    }
}