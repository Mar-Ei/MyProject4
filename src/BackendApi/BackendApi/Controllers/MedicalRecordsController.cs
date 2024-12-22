using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordRepository _medicalRecordsService;

        public MedicalRecordsController(IMedicalRecordRepository medicalRecordsService)
        {
            _medicalRecordsService = medicalRecordsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _medicalRecordsService.FindAll();  // Используем FindAll
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _medicalRecordsService.GetByIdAsync(id);  // Используем GetByIdAsync
            if (record == null)
            {
                return NotFound("Медицинская запись не найдена.");
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] MedicalRecord record)
        {
            if (record == null)
            {
                return BadRequest("Некорректные данные для записи.");
            }

            await _medicalRecordsService.Create(record);
            return Ok("Медицинская запись успешно добавлена.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MedicalRecord record)
        {
            var existingRecord = await _medicalRecordsService.GetByIdAsync(id);
            if (existingRecord == null)
            {
                return NotFound("Медицинская запись не найдена.");
            }

            record.RecordId = id;
            await _medicalRecordsService.Update(record);
            return Ok("Медицинская запись успешно обновлена.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingRecord = await _medicalRecordsService.GetByIdAsync(id);
            if (existingRecord == null)
            {
                return NotFound("Медицинская запись не найдена.");
            }

            await _medicalRecordsService.Delete(existingRecord);
            return Ok("Медицинская запись успешно удалена.");
        }
    }
}
