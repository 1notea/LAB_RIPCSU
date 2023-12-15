using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Zakharov;
using Zakharov.Models;

public class JournalController : Controller
{
    private readonly JournalService _journalService;

    public JournalController(JournalService journalService)
    {
        _journalService = journalService;
    }

    public IActionResult Index(string studentName)
    {
        IEnumerable<Journal> journals;

        if (string.IsNullOrEmpty(studentName))
        {
            journals = _journalService.GetAllJournals();
        }
        else
        {
            journals = _journalService.GetJournalsByStudentName(studentName);
        }

        return View(journals);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Journal journal)
    {
        if (ModelState.IsValid)
        {
            _journalService.CreateJournal(journal);
            return RedirectToAction("Index");
        }

        return View(journal);
    }

    public IActionResult Edit(int id)
    {
        var journal = _journalService.GetJournalById(id);
        if (journal == null)
        {
            return NotFound();
        }

        return View(journal);
    }

    [HttpPost]
    public IActionResult Edit(int id, Journal journal)
    {
        if (id != journal.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _journalService.UpdateJournal(journal);
            return RedirectToAction("Index");
        }

        return View(journal);
    }

    public IActionResult Delete(int id)
    {
        _journalService.DeleteJournal(id);
        return RedirectToAction("Index");
    }

}
