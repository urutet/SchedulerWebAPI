using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.University.Faculty;
using Scheduler.DomainModel.Model.University;
using Scheduler.Models.Errors;
using Scheduler.Models.University;
using Scheduler.Services.University;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacultiesController : Controller
{
    private IFacultyService _facultyService;
    private IFacultyCreator _facultyCreator;
    
    public FacultiesController(IFacultyService facultyService, IFacultyCreator facultyCreator)
    {
        _facultyService = facultyService;
        _facultyCreator = facultyCreator;
    }
    
    [HttpGet("faculties")]
    public async Task<IReadOnlyCollection<Faculty>> GetFaculties()
    {
        var faculties = await _facultyService.GetFaculties();
        return faculties;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFaculty createFaculty)
    {
        var faculty = _facultyCreator.CreateFrom(createFaculty);
        await _facultyService.AddFaculty(faculty);

        return Ok();
    }
    
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteFaculty(string id)
    {
        var department = await _facultyService.GetFaculty(id);
        if (department is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _facultyService.DeleteFaculty(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditFaculty(string id, [FromBody] CreateFaculty createFaculty)
    {
        var faculty = _facultyCreator.CreateFrom(createFaculty);
        
        var editFaculty = await _facultyService.GetFaculty(id);
        if (editFaculty is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _facultyService.UpdateFaculty(editFaculty.Id, faculty);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
}