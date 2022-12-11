using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.University.Department;
using Scheduler.DomainModel.Model.University;
using Scheduler.Models.Errors;
using Scheduler.Models.University;
using Scheduler.Services.University;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private IDepartmentService _departmentService;
    private IDepartmentCreator _departmentCreator;
    
    public DepartmentsController(IDepartmentService departmentService, IDepartmentCreator departmentCreator)
    {
        _departmentService = departmentService;
        _departmentCreator = departmentCreator;
    }
    
    [HttpGet("departments")]
    public async Task<IReadOnlyCollection<Department>> GetDepartments()
    {
        var departments = await _departmentService.GetDepartments();

        return departments;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> Create([FromBody] CreateDepartment createDepartment)
    {
        var department = _departmentCreator.CreateFrom(createDepartment);
        await _departmentService.AddDepartment(department);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteDepartment(string id)
    {
        var department = await _departmentService.GetDepartmentById(id);
        if (department is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _departmentService.DeleteDepartment(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditDepartment(string id, [FromBody] CreateDepartment createDepartment)
    {
        var department = _departmentCreator.CreateFrom(createDepartment);
        var editDepartment = await _departmentService.GetDepartmentById(id);
        if (editDepartment is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _departmentService.UpdateDepartment(editDepartment.Id, department);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }

}