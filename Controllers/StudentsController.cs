using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // READ - danh sách
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students.ToListAsync();
        return View(students);
    }

    // READ - chi tiết
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null) return NotFound();

        return View(student);
    }

    // CREATE - hiển thị form
    public IActionResult Create()
    {
        return View();
    }

    // CREATE - lưu dữ liệu
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (!ModelState.IsValid) return View(student);

        _context.Add(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // UPDATE - hiển thị form
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        return View(student);
    }

    // UPDATE - lưu thay đổi
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();

        if (!ModelState.IsValid) return View(student);

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(student.Id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // DELETE - xác nhận
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null) return NotFound();

        return View(student);
    }

    // DELETE - xóa dữ liệu
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int id)
    {
        return _context.Students.Any(e => e.Id == id);
    }
}
