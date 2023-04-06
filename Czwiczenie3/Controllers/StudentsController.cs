using Czwiczenie3.DAL;
using Czwiczenie3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Czwiczenie3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentsRepository _studentsRepository;

    public StudentsController(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    [HttpGet]
    [Route("/students")]
    public IActionResult GetStudents()
    {
        return Ok(_studentsRepository.GetStudents());
    }

    [HttpGet]
    [Route("/students/{index}")]
    public IActionResult GetStudents(string index)
    {
        Student resp;
        try
        {
            resp = _studentsRepository.GetStudentsById(index);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return Ok(resp);
    }

    [HttpPut]
    [Route("/students/{index}")]
    public IActionResult ModifyStudents(StudentModyf student, string index)
    {
        Student resp;
        try
        {
            resp = _studentsRepository.ModifyStudent(student, index);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }

        return Ok(resp);
    }

    [HttpPost]
    [Route("/students")]
    public IActionResult AddStudents(Student student)
    {
        Student resp;
        try
        {
            resp = _studentsRepository.AddStudent(student);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(resp);
    }

    [HttpDelete]
    [Route("/students/{index}")]
    public IActionResult DeleteStudents(string index)
    {
        _studentsRepository.DeleteStudent(index);
        return NoContent();
    }
}